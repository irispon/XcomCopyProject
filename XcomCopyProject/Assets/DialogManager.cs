using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : SingletonObject<DialogManager>,ICallBackHandler
{
    public void Call(string message, ICallBack callback)
    {
        if (message.Equals("LoginError"))
        {

            Debug.Log("LoginError!");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
