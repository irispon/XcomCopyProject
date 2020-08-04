using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Post
{


    // Start is called before the first frame update
    public async Task<string> PostRequest(string uri, string id, string password)
    {
        string certification = string.Empty;

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

            return "";
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            if (uwr.downloadHandler.text.Equals("true"))
            {
                return uwr.downloadHandler.text;
            }



        }

        return "";

    }
}
