using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CameraManager : SingletonObject<CameraManager>
{
    [SerializeField]
    Camera mainCamera;
    CameraContorler mainContorler;
    Camera subCamera;
    Queue<GameObject> targets;
    bool keyState= true;
    // Start is called before the first frame update
    public override void Init()
    {
        subCamera =null;
        targets = new Queue<GameObject>();

    }
    public void Start()
    {
        mainContorler = mainCamera.GetComponent<CameraContorler>();
    }

    public void On(Camera camera)
    {
        if (subCamera == null)
        {
            mainCamera.enabled = false;
            subCamera = camera;
            subCamera.enabled = true;
        
        }


    }
    public void Off()
    {
       
        subCamera.enabled = false;
        mainCamera.enabled = true;
        subCamera = null;
        
    }
    public void AddTarget(GameObject target)
    {
        targets.Enqueue(target);
    }
    public void FixedUpdate()
    {
        if (Input.GetButton("tab"))
        {
      
            if (targets.Count > 0 && mainCamera.enabled == true&& keyState)
            {

                GameObject target = targets.Dequeue();
                mainContorler.SetFocus(target);
                targets.Enqueue(target);
                Debug.Log("tab");


            }
            keyState = false;
        }
        else
        {
            keyState = true;
        }
    }

    
}
