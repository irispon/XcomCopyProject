using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProfileWindow : MonoBehaviour
{
    [SerializeField]
    ProfileImage profileImage;
    [SerializeField]
    GameObject content;
    Task<string> task;
    // Start is called before the first frame update

    public void Start()
    {

        task = Request.Get(ServerHelper.SERVERPATH + "/" + GetEvent.profiles);
    }
    //버튼
    public async void GetProfiles()
    {
      if(task != null)
        {
            string result = await task;
            Debug.Log(result);
            JArray jArray = JArray.Parse(result);
            foreach (JObject resource in jArray)
            {
                Sprite sprite = null;
                string resourcePath = resource["Resource"].ToString();
                try
                {
                    sprite = Resources.Load<Sprite>(resource["Resource"].ToString());
               
                }
                catch (Exception e)
                {
                    Debug.Log("리소스 에러");
                }
                if (sprite != null)
                {
                    ProfileImage image = Instantiate(profileImage);
                    SizeFitter.FittingContent(image.gameObject, content);
                    image.Set(resourcePath, sprite);
                }


            }
            task = null;


        }


    }
}
