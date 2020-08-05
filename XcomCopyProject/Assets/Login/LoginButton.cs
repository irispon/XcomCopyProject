using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : SingletonObject<LoginButton>,ICallBack
{
    // Start is called before the first frame update
    [SerializeField]
    LoginWindow loginPanel;
    [SerializeField]
    CanvasGroup group;


    public void Click()
    {
        loginPanel.Call("open", this);
    }

    public void Back(string message)
    {
        if (message.Equals("open"))
        {
            group.interactable = false;
        }
        else if (message.Equals("close"))
        {
            group.interactable = true;
        }
    }
}
