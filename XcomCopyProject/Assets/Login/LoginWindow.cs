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
        Task<LoginMessage> request = CertificateRequest(ServerHelper.LOGINPATH(), id.text, password.text);
        loadingIcon.gameObject.SetActive(true);
        //   Debug.Log("백그라운드 테스트");
        LoginMessage login = await request;
        Debug.Log(login.ToString());
        DialogManager.GetInstance().Call(login.ToString(), this);
        group.alpha = 0.5f;
        group.interactable = false;

        //  Debug.Log("2번째" + jArray.ToString());
    }

    async Task<LoginMessage> CertificateRequest(string uri,string id,string password)
    {
     

        Debug.Log(uri);
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("id", id);
        wWForm.AddField("password", password);

        UnityWebRequest uwr = UnityWebRequest.Post(uri, wWForm);

        uwr.SendWebRequest();
   
        while (!uwr.isDone)
        {
          await Task.Delay(10);
            //나중에 변경해야될 코드.(로그인이 안되는 경우)
        }
      
      

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);

            return LoginMessage.serverError;
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            if (uwr.downloadHandler.text.Equals("true"))
            {
                return LoginMessage.success;
            }
            else if(uwr.downloadHandler.text.Equals("false"))
            {
                return LoginMessage.validateError;
            }
           


        }

        return LoginMessage.serverError;

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
