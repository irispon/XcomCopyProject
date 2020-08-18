using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using xcopy;

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeFields]
    public CharacterStatus status;
    public Weapon weapon;
    public Transform righttHand;
    public Transform leftHand;
    public List<Ablity> ablities;

    Animator animator;

    CameraManager manager;
    UnitManager unitManager;



    [HideInInspector]
    public bool moving;
    [HideInInspector]
    public bool isSelect = false;
    [HideInInspector]
    public bool ablityMode = false;
    IEnumerator move;
    IEnumerator rotate;

    Actions actions;
    Collider[] targets;
    Collider target;
    TargetIndecator indecator;
    int index = 0;



    public void Awake()
    {
        animator = GetComponent<Animator>();
        actions = new Actions(animator);

    }
    public void Start()
    {
        manager = CameraManager.GetInstance();
        unitManager = UnitManager.GetInstance();
        if (weapon != null)
            animator.runtimeAnimatorController = AnimatorControler.GetInstance().animations[weapon.weapon.type];
        unitManager.AddUnit(this);
        animator.SetBool("Rest", true);
        animator.speed = 1;
        indecator = TargetIndecator.GetInstance();
    }
    public bool IsAttackMode()
    {
        return unitManager.attackMode;
    }
    public void Select()
    {
        Debug.Log(name);
        manager.Foucusing(gameObject);
        Move.GetInstance().GetTurn(this);
        isSelect = true;

    }
    public void AttackMode()
    {
        if (moving == false)
        {
            manager.On(gameObject);
            unitManager.SelectUnit(this);

        }

    }
    public void DiSelect()
    {
        manager.Off();
        isSelect = false;
        ablityMode = false;
       indecator.Diselct();

    }
    public void MoveTo(Vector3 position)
    {

        if (moving == false)
        {
            if (move != null)
            {
                StopCoroutine(move);
            }
            move = MoveCorutine(position);
            StartCoroutine(move);
        }


    }
    IEnumerator MoveCorutine(Vector3 position)
    {
        Vector3 direction;
        Quaternion quaternion = default;
        moving = true;
        while (true)
        {
            if (Vector3.Distance(transform.position, position) > 0.1)
            {

                direction = position - transform.position;
                quaternion = Quaternion.LookRotation(direction);
                actions.Run();
                //   currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
                transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 2);
                transform.rotation = quaternion;

            }
            else
            {

                actions.Stay();
                transform.position = position;
                moving = false;
                break;
            }

            yield return new WaitForFixedUpdate();
        }
        if (quaternion != null)
            transform.rotation = Quaternion.Euler(quaternion.eulerAngles-new Vector3(quaternion.eulerAngles.x,0,0));

    }
    IEnumerator QuaternionCorutine(Quaternion quaternion)
    {
        while (transform.rotation!=quaternion)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * 2);
            yield return new WaitForFixedUpdate();
        }
  
    }

    public void Update()
    {
        if (isSelect && unitManager.attackMode && status.attackPoint > 0)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                ablityMode = true;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Aiming"))
                {
                    targets = Physics.OverlapSphere(transform.position, 100,LayerMaskUtill.Single("Enemy"));
                    index = 0;
                    Debug.Log("타게팅");
                    if (targets.Length > 0)
                    {
                        target = targets[0];
                        Debug.Log("targetName" + target);
                        Vector3 direction = target.transform.position - transform.position;
                        Quaternion quaternion = Quaternion.LookRotation(direction);
                        indecator.Set(target.transform, direction);
                        if (rotate != null)
                            StopCoroutine(rotate);
                        rotate = QuaternionCorutine(quaternion);
                        StartCoroutine(rotate);
                    }
                    else
                    {
                        target = null;

                    }

                        actions.Aiming();
         


                    


                }
                else
                {
                    Debug.Log("유니찬 공격");

                    actions.Attack();
                    ablityMode = false;
                    status.attackPoint -= 1;
                    indecator.Diselct();


                }


            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {

                actions.Stay();
                ablityMode = false;
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && animator.GetCurrentAnimatorStateInfo(0).IsName("Aiming"))
            {
                if (targets.Length > 0)
                {
                    index++;

                    if (index >= targets.Length)
                    {
                        index = 0;
                    }
                    target = targets[index];
                    Debug.Log("targetName" + target);
                    Vector3 direction = target.transform.position - transform.position;
                    Quaternion quaternion = Quaternion.LookRotation(direction);
                    indecator.Set(target.transform, direction);
                    if (rotate != null)
                        StopCoroutine(rotate);
                    rotate = QuaternionCorutine(quaternion);
                    StartCoroutine(rotate);

                }


            }


        }
        else if (isSelect == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Aiming"))
        {
            indecator.Diselct();
            actions.Stay();
        }


    }


    public void GetDamage(int damage)
    {
        status.hp -= damage;
    }
    public void OnMouseUpAsButton()
    {

    }

    public void OnMouseEnter()
    {

        Debug.Log("강조선 표시");

    }
    public void OnMouseExit()
    {
        Debug.Log("강조선 끄기");
    }

}
