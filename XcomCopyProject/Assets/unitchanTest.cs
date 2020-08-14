using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitchanTest : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo currentBaseState;
    Vector3 velocity;
	public bool useCurves = true;
	// Start is called before the first frame update
	static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", 1);
        animator.SetFloat("Direction", 1);
        animator.speed = 1.2f;
        currentBaseState= animator.GetCurrentAnimatorStateInfo(0);
        velocity = new Vector3(0, 0, 1);     
                                            
        velocity = transform.TransformDirection(velocity);
        transform.localPosition += velocity * Time.fixedDeltaTime;
        transform.Rotate(0, 0.5f, 0);

	}


}
