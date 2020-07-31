

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;
using Newtonsoft.Json.Linq;

public class ChatManager : SingletonObject<ChatManager>
{

    public TMP_InputField inputText;
    public GameObject contentPanel;
    public ChatObject chatObject;

    public void ReciveMessage(string message)
    {

        JObject chatMessage = JObject.Parse(message);


        try
        {
            ChatObject chat = Instantiate(chatObject);
            chat.outputText.text = chatMessage[ChatHelper.Message.ToString()].ToString();
            chat.client.text = chatMessage[ChatHelper.Client.ToString()].ToString();
            SizeFitter.FittingContent(chat.gameObject, contentPanel);
        }
        catch (Exception e)
        {
            Debug.Log("Chat에러"+e);

        }
    }

}
