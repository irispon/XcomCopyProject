using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using xcopy;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Camera subCamera;
    CameraManager manager;
    UnitManager unitManager;
    public void Awake()
    {
        subCamera.enabled = false;
    }
    public void Start()
    {
        manager = CameraManager.GetInstance();
        unitManager = UnitManager.GetInstance();
        manager.AddTarget(gameObject);
    }
    public void Select()
    {

        
        manager.On(subCamera);
    }
    public void DiSelect()
    {
        manager.Off();
    }

    public void OnMouseUpAsButton()
    {
        unitManager.SelectUnit(this);
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
