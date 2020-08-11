using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CameraManager : SingletonObject<CameraManager>
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Camera subCamera;
    CameraContorler mainContorler;
    CameraContorler subContorler;
     Queue<GameObject> targets;
    Queue<GameObject> enemTargets;
    bool keyState= true;
    // Start is called before the first frame update
    public override void Init()
    {
     //   subCamera =null;
        targets = new Queue<GameObject>();

    }
    public void Start()
    {
        mainContorler = mainCamera.GetComponent<CameraContorler>();
        subContorler = subCamera.GetComponent<CameraContorler>();
    }

    public void On(GameObject target)
    {
       
            mainCamera.enabled = false;
            subCamera.enabled = true;
            subContorler.SetFocus(target);
        



    }
    public void Off()
    {
       
        subCamera.enabled = false;
        mainCamera.enabled = true;
        
        
    }
    public void AddTarget(GameObject target)
    {
        targets.Enqueue(target);
    }
    public void FixedUpdate()
    {
        if (Input.GetButton("tab"))
        {
      
            if(targets.Count > 0 && keyState)
            {

                if (mainCamera.enabled == true )
                {

                    GameObject target = targets.Dequeue();
                    mainContorler.SetFocus(target);
                    targets.Enqueue(target);
                  //  Debug.Log("tab");


                }
                else if (subCamera.enabled==true)
                {
                    GameObject target = targets.Dequeue();
                    subContorler.SetFocus(target);
                    targets.Enqueue(target);
                    Debug.Log("subtab");
                }
            }

            keyState = false;
        }
        else
        {
            keyState = true;
        }

    }

    
}
