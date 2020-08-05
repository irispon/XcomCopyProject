using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginButton : MonoBehaviour,ICallBack
{
    // Start is called before the first frame update
    [SerializeField]
    LoginWindow loginPanel;


    void PopUP()
    {
        loginPanel.Call("open", this);
    }

    public void Back(string message)
    {
        if (message.Equals("open"))
        {
            gameObject.SetActive(false);
        }
    }
}
