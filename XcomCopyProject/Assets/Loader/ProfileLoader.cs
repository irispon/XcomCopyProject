using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileLoader : Loader
{
    DataBaseManager manager = DataBaseManager.GetInstance();
    ProfileImageChache chache = ProfileImageChache.GetInstance();
    public override void Load()
    {
        done = false;
        try
        {

      
            manager.GetData("SELECT*FROM profileresource ", (JArray arry) =>
            {

                foreach (JObject resource in arry)
                {



                    string resourcePath = resource["Resource"].ToString();
                    Sprite sprite = Resources.Load<Sprite>(resource["Resource"].ToString());
                    chache.profileImage.Add(resourcePath, sprite);
                    
                }


                done = true;



            });
        }catch(Exception e)
        {

            Debug.Log(e);
        }
      


    }

}
