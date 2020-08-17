using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public ActionState state;

    Animator animator;

    CameraManager manager;
    UnitManager unitManager;



    [HideInInspector]
    public bool moving;
    IEnumerator move;

    Actions actions;



    public void Awake()
    {
        animator = GetComponent<Animator>();
        actions = new Actions(animator);
        animator.runtimeAnimatorController = AnimatorControler.GetInstance().animations[weapon.weapon.type];
    }
    public void Start()
    {
        manager = CameraManager.GetInstance();
        unitManager = UnitManager.GetInstance();
        unitManager.AddUnit(this);
        animator.SetBool("Rest", true);
        animator.speed = 1;

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
        Quaternion quaternion;
        moving = true;
        while (true)
        {
            if (Vector3.Distance(transform.position, position) > 0.1)
            {
                animator.SetBool("Rest", false);
                direction = position - transform.position;
                quaternion = Quaternion.LookRotation(direction);
                animator.SetFloat("Speed", 1);
                animator.speed = 1.2f;
             //   currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
                transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 2);
                transform.rotation = quaternion;

            }
            else
            {
                animator.SetFloat("Speed", 0);
  
               // animator.speed = 0f;
                transform.position = position;
                moving = false;
                animator.SetBool("Rest", true);
                break;
            }

            yield return new WaitForFixedUpdate();
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
enum ActionState
{

}
