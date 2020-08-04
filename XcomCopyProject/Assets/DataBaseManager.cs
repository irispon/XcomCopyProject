using Newtonsoft.Json.Linq;
using socket.io;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataBaseManager : SingletonObject<DataBaseManager>
{
    // Start is called before the first frame update

    public override void Init()
    {
        DontDestroyOnLoad(this);

    }

    public void GetData(string sql,Action<JArray> action)
    {

        StartCoroutine(GetRequest(sql, action));
    }

    IEnumerator GetRequest(string sql, Action<JArray> action=null)
    {
       
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("sql", sql);
        UnityWebRequest uwr = UnityWebRequest.Post(ServerHelper.DATABASEPATH(), wWForm);


        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            JArray jObject = JArray.Parse(uwr.downloadHandler.text);
            Debug.Log(jObject);
            if (action != null)
            {
                action(jObject);
            }
          

        }
    }

}
