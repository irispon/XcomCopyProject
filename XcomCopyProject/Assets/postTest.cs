using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class postTest : MonoBehaviour, ICallBack
{
    Task<string> task;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void test()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", "iris");
        form.AddField("token", "test");
        task = Request.PostRequest(ServerHelper.LOCALPATH + "/getinfo", form, this);

    }

    public void Back(string message)
    {

        //  Debug.Log(task.Result);
        //  JObject jObject = JObject.Parse(task.Result);


        test(task,message);
    }

    async void test(Task<string> task,string message){

        string request = await task;
        JObject jObject = JObject.Parse(request);
        Debug.Log(message + " " + task.IsCompleted);
        Debug.Log("" + task);
        Debug.Log("결과: "+request);
        Debug.Log("파싱: " + jObject);
    }
}
