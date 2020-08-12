using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CameraManager : SingletonObject<CameraManager>
{
    [SerializeField]
    public Camera mainCamera;
    [SerializeField]
    public Camera subCamera;
    CameraContorler mainContorler;
    CameraContorler subContorler;

    bool keyState= true;
    // Start is called before the first frame update
    public override void Init()
    {
     //   subCamera =null;


    }
    public void Start()
    {
        mainContorler = mainCamera.GetComponent<CameraContorler>();
        subContorler = subCamera.GetComponent<CameraContorler>();
    }


    public void On(GameObject target)
    {
        if (mainCamera.enabled == true)
        {
            mainCamera.enabled = false;
            subCamera.enabled = true;
        }
        else
        {
      
            subCamera.enabled = false;
        }

            Foucusing(target);


    }
    public void Off()
    {
        if(subCamera.enabled==true)
        mainContorler.CamToCam(subCamera);

    }
    public void Foucusing(GameObject target)
    {
        if (mainCamera.enabled == true)
        {
            subCamera.enabled = false;
            subContorler.CamToCam(mainCamera, true);
        }
        else
        {
            subCamera.enabled = true;
            subContorler.SetFocus(target);
        }
        mainContorler.SetFocus(target);

    }

    public void FixedUpdate()
    {
 

    }

    
}
