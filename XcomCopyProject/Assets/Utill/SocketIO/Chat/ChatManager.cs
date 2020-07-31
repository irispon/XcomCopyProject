
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : SingletonObject<ChatManager>
{

    public TextMeshProUGUI inputText;
    public GameObject contentPanel;
    public ChatObject chatObject;

    public void ReciveMessage(string message)
    {
        JObject chatMessage = JObject.Parse(message);
        try
        {
            ChatObject chat = Instantiate(chatObject);
            chat.outputText.text = chatMessage["Message"].ToString();
        }
        catch (Exception e)
        {
            Debug.Log("Chat에러"+e);

        }
    }
    public void SendMessage(ChatMessage message)
    {
       

    }
}
