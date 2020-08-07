using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : SingletonObject<DialogManager>,ICallBackHandler
{
    [SerializeField]
    Text title, context,button;
    [SerializeField]
    Image logo;
    ICallBack caller;
    Action action;

    public void Call(string message, ICallBack callback=null)
    {
        caller = callback;
        gameObject.SetActive(true);
        if (message.Equals(RequestMessage.validateError.ToString()))
        {
            title.text = "LoginError!";
            context.text = "비밀번호나 아이디 입력이 틀렸습니다.";
            Debug.Log("LoginError!");
            action = () => Close();
        }
        else if (message.Equals(RequestMessage.serverError.ToString()))
        {
            title.text = "ConnectError!";
            context.text = "서버 연결에 실패했습니다.";
            Debug.Log("LoginError!");
            action =()=> Close();


        }
        else if(message.Equals(RequestMessage.success.ToString()))
        {
            title.text = "LoginSuccess!";
            context.text = "아이디 로그인에 성공하셨습니다.";
            Debug.Log("로그인 성공");
            action = () => Next();
        }
       

    }
    public void Call(string title,string context, Action action=null,Image icon= null,ICallBack callBack=null)
    {
        caller = callBack;
        gameObject.SetActive(true);
        this.title.text = title;
        this.context.text = context;
        if (icon != null)
        {
            logo.sprite = icon.sprite;
        }
        if (action != null)
        {
            this.action = () => action();
        }
        else
        {
            this.action = () => Close();
        }
  

    }

    public void Confirm()
    {

        action();

    
    }
    public void Close()
    {
     //   Debug.Log("테스트");
        if(caller!=null)
        caller.Back("close");
        gameObject.SetActive(false);
    }
    void Next()
    {
        Debug.Log("다음 씬으로");
        LoadingManager.LoadScene("SocketTest");
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
