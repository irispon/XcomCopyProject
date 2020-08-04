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
        Task<JArray> request = CertificateRequest(ServerHelper.LOGIN, "iris", "t8529741");
     //   Debug.Log("백그라운드 테스트");
        JArray jArray = await request;
      //  Debug.Log("2번째" + jArray.ToString());
    }

    async Task<JArray> CertificateRequest(string uri,string id,string password)
    {
        string certification=string.Empty;
        JArray jObject = null;

        WWWForm wWForm = new WWWForm();
        wWForm.AddField("id", id);
        wWForm.AddField("password", password);

        UnityWebRequest uwr = UnityWebRequest.Post(uri, wWForm);

        uwr.SendWebRequest();
   
        while (!uwr.isDone)
        {
          await Task.Delay(100);
            //나중에 변경해야될 코드.(로그인이 안되는 경우)
        }
      
      

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            return null;
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            jObject = JArray.Parse(uwr.downloadHandler.text);
            Debug.Log(jObject);


        }

        return jObject;

    }

 
}
