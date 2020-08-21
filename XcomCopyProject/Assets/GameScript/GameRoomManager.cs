using socket.io;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomManager:SingletonObject<GameRoomManager>
{
    Socket chat;

    static public string roomName;


    public void Connect(string roomName)
    {

        if (chat != null)
        {
            chat.Emit(ServerEvent.disconnect.ToString());
            Destroy(chat.gameObject);
        }
        chat = Socket.Connect(ServerHelper.SERVERPATH + "/" + SocketEvent.room.ToString()+"/"+roomName);
        chat.On("connect", () => {
            Debug.Log("채팅 서버 접속" + chat.IsConnected);
           chat.On(GameServerCommand.Move.ToString(), ()=> { 
            
           
           
           
           });

        });

    }

}

enum GameServerCommand
{
    Move,Attack
}