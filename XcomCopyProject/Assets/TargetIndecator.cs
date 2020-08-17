using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndecator : SingletonObject<TargetIndecator>
{

    public Vector3 position;

    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    /// <summary>
    /// 유닛 입력
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    /// 
    public void Set(Transform target,Vector3 direction)
    {
        gameObject.SetActive(true);
        // Debug.Log(unit.name);
        transform.SetParent(target);
        transform.localPosition = new Vector3(0, 0, 0)+position;
        transform.rotation = Quaternion.LookRotation(direction);

    }
    public void Set(Transform target)
    {
        gameObject.SetActive(true);
        // Debug.Log(unit.name);
        transform.SetParent(target);
        transform.localPosition = new Vector3(0, 0, 0) + position;


    }
    public void Diselct()
    {
        gameObject.SetActive(false);

    }
   


    }

