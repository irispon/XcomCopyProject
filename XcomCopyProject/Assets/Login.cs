using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    [SerializeField]
    TMP_InputField id, password;

    // Start is called before the first frame update


   public async void Click()
    {
        Task<bool> request = CertificateRequest(ServerHelper.LOGINPATH(), "iris", "t529741");
        //   Debug.Log("백그라운드 테스트");
        bool login = await request;
       
        if (login)
        {
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

 
}
