using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindow : SingletonObject<RegisterWindow>,ICallBack
{
    CanvasGroup group;
    [SerializeField]
    Image loading;
    [SerializeField]
    InputField email, password, duplicatePassword;
    DialogManager dialog;
    public override void Init()
    {
        group = GetComponent<CanvasGroup>();
        gameObject.SetActive(false);
        loading.gameObject.SetActive(false);
      


    }
    public void Start()
    {
        dialog = DialogManager.GetInstance();
    }
    //OpenButton
    public void Open()
    {
        gameObject.SetActive(true);
        group.interactable = true;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        group.interactable = true;
    }

    public async void Request()
    {
        Debug.Log("리퀘스트 클릭");
        if (password.text.Equals(duplicatePassword.text))
        {
            WWWForm wwform = new WWWForm();
        //    wwform.AddField("name", userName.text);
            wwform.AddField("password", password.text);
            wwform.AddField("email", email.text);

            group.interactable = false;
            loading.gameObject.SetActive(true);

            Task<string> post = global::Request.PostRequest(ServerHelper.CREATEACCOUNT(), wwform);
            string request = await post;

            loading.gameObject.SetActive(false);
            group.interactable = true;

            Back(request);
        }
        else
        {
            Back("notMatch");

        }

    }

    public void Back(string message)
    {
        if (message.Equals(RequestMessage.serverError.ToString()))
        {
            dialog.Call(message);

        }
        else if (message.Equals("true"))
        {
            dialog.Call("전송 성공","인증 링크가 전송되었습니다.", callBack:this);
        }
        else if(message.Equals("false"))
        {
            dialog.Call("전송 실패","이미 등록된 이메일입니다.");
        }
        else if(message.Equals("close"))
        {
            Close();
        }else if (message.Equals("notMatch"))
        {
            dialog.Call("패스워드 불일치", "패스워드가 불일치합니다.");
        }
        else if (message.Equals("exist"))
        {
            dialog.Call("중복된 아이디", "이미 아이디가 존재합니다.");
        }

    }
}
