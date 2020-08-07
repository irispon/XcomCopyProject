using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImage : MonoBehaviour
{
    public Image iamge;
    [HideInInspector]
    public string profilePath;
    [HideInInspector]
    public Sprite sprite;
   

    public void Set(string profilePath,Sprite sprite)
    {
        this.profilePath = profilePath;
        this.sprite = sprite;
        iamge.sprite = sprite;

    }

    public void Click()
    {
        PlayerCache.GetInstance().SetProfile(profilePath, sprite);
    }

}
