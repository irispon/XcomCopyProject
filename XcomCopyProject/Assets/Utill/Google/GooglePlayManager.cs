﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Text UI 사용
using UnityEngine.UI;
// 구글 플레이 연동
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
public class GooglePlayManager : MonoBehaviour
{
    bool bWait = false;
    public TextMeshProUGUI text;

    void Awake()
    {
        /*
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        */

        text.text = "no Login";
    }
    void Start()
    {

    }
    void Update()
    {

    }

    public void OnLogin()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool bSuccess) =>
            {
                if (bSuccess)
                {
                    Debug.Log("Success : " + Social.localUser.userName);
                    text.text = Social.localUser.userName;
                }
                else
                {
                    Debug.Log("Fall");
                    text.text = "Fail";
                }
            });
        }
    }

    public void OnLogOut()
    {
     //   ((PlayGamesPlatform)Social.Active).SignOut();
        text.text = "Logout";
    }
}

