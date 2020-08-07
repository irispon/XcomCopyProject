using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Post
{


    // Start is called before the first frame update
    public static async Task<string> PostRequest(string uri, WWWForm form,ICallBack callBack =null)
    {
     

        Debug.Log(uri);
      
  
        UnityWebRequest uwr = UnityWebRequest.Post(uri, form);

        uwr.SendWebRequest();

        while (!uwr.isDone)
        {
            await Task.Delay(100);
            //나중에 변경해야될 코드.(로그인이 안되는 경우)
        }



        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            if (callBack != null)
            callBack.Back(PostEvent.error.ToString());//나중에 enum으로 바꾸자
            return PostMessage.serverError.ToString();
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            if(callBack!=null)
            callBack.Back(PostEvent.success.ToString());
            return uwr.downloadHandler.text;
          
        }

 

    }
}
