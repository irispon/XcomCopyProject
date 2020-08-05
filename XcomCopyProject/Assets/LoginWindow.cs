using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginWindow : MonoBehaviour, ICallBackHandler, ICallBack
{
    [SerializeField]
    InputField id, password;
    [SerializeField]
    Image loadingIcon;
    // Start is called before the first frame update



    public void Awake()
    {
        loadingIcon.gameObject.SetActive(false);
       
     
    }
    public void Start()
    {
        gameObject.SetActive(false); 
    }

    public void Call(string message, ICallBack callback)
    {
        if (message.Equals("open"))
        {
            gameObject.SetActive(true);
            callback.Back(message);
        }
    }

    public async void Login()
    {
        Task<bool> request = CertificateRequest(ServerHelper.LOGINPATH(), id.text, password.text);
        //   Debug.Log("백그라운드 테스트");
        bool login = await request;

        if (login)
        {
            SceneManager.LoadScene("SocketTest");
            Debug.Log("로그인 성공");

        }
        else
        {

            Debug.Log("비밀번호나 아이디 입력이 잘못되었습니다.");
        }
        //  Debug.Log("2번째" + jArray.ToString());
    }

    async Task<bool> CertificateRequest(string uri,string id,string password)
    {
        string certification=string.Empty;

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

            return false;
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            if (uwr.downloadHandler.text.Equals("true"))
            {
                return true;
            }
           


        }

        return false;

    }

    public void Back(string message)
    {
   
    }
}
