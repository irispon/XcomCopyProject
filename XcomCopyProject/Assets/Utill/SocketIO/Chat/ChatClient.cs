using socket.io;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ChatClient : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI inputText;
    Socket chat;
    // Start is called before the first frame update
    void Start()
    {
        chat = Socket.Connect(SocketIOHelper.SERVERPATH + "/"+SocketEvent.chat.ToString());
        chat.On("connect", () => {
            Debug.Log("채팅 서버 접속");

        });
    }
    public void SendMessage()
    {
        if (chat != null)
        {
            JObject chatMessage = new JObject();
            chatMessage["UserName"] = "name";
            chatMessage["Message"] = inputText.text;

            chat.Emit(SocketEvent.chat.ToString(), inputText.text);
        }

    }
  

}

