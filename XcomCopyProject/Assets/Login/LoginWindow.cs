using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginWindow : SingletonObject<LoginWindow>, ICallBackHandler, ICallBack
{
    [SerializeField]
    InputField id, password;
    [SerializeField]
    Image loadingIcon;
    ICallBack caller;
    CanvasGroup group;
    // Start is called before the first frame update



    public override void Init()
    {
        loadingIcon.gameObject.SetActive(false);
        gameObject.SetActive(false);
        group = GetComponent<CanvasGroup>();


    }
    public void Start()
    {
     
    }

    public void Call(string message, ICallBack callback)
    {
        if (message.Equals("open"))
        {
            caller = callback;
            gameObject.SetActive(true);
            callback.Back(message);
        }
    }
    public void Close()
    {
        if(caller!=null)
        caller.Back("close");//별로 좋진 않은 거 같음.
        gameObject.SetActive(false);
        Debug.Log("closeLoginWindow");
    }

    public async void Login()
    {
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("id", id.text);
        wWForm.AddField("password", password.text);

        Task<string> request = Request.PostRequest(ServerHelper.LOGINPATH(), wWForm);
        loadingIcon.gameObject.SetActive(true);
        string login = await request;
        Debug.Log(login);
        JObject json = JObject.Parse(login);
        Debug.Log(json+" "+ json["id"]);
        if (json["id"].ToString().Equals("1"))
        {
            login = RequestMessage.success.ToString();
        }
        else if(json["id"].ToString().Equals("0"))
        {
            login = RequestMessage.validateError.ToString();
        }
        DialogManager.GetInstance().Call(login, this);
        group.alpha = 0.5f;
        group.interactable = false;

        PlayerCache.GetInstance().Set(id.text, json["token"].ToString());
        //  Debug.Log("2번째" + jArray.ToString());
    }


    public void Back(string message)
    {
        if (message.Equals("close"))
        {
            group.alpha = 1;
            loadingIcon.gameObject.SetActive(false);
            group.interactable = true;
        }
    }
}
