using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : SingletonObject<LoginManager>,CallBack
{
    string nextScean = "SocketTest";
    public void Call(string message, CallBack callback)
    {
        if (message.Equals("Sucess"))
        {
            LoadingManager.LoadScene(nextScean);
        }else if (message.Equals("Error"))
        {


        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
