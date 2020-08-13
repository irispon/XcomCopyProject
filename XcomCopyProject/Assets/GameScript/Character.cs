using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using xcopy;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update

    CameraManager manager;
    UnitManager unitManager;
    public CharacterStatus status;
    [HideInInspector]
    public bool attackMode;

    public void Start()
    {
        manager = CameraManager.GetInstance();
        unitManager = UnitManager.GetInstance();
        unitManager.AddUnit(this);



    }
    public void Select()
    {
        Debug.Log(name);
        manager.Foucusing(gameObject);
        Move.GetInstance().GetTurn(this);

    }
    public void AttackMode()
    {
        manager.On(gameObject);
        unitManager.SelectUnit(this);
        attackMode = true;
    }
    public void DiSelect()
    {
        manager.Off();
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
