using socket.io;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager :SingletonObject<SocketManager>
{
    Dictionary<SocketEvent, Socket> sockets;

    public override void Init()
    {
        sockets = new Dictionary<SocketEvent, Socket>();
    }
    public Socket GetSocket(SocketEvent socketEvent)
    {
        Socket socket;


        if (sockets.ContainsKey(socketEvent))
        {

            return sockets[socketEvent];
        }
        else
        {
            socket = Socket.Connect(SocketIOHelper.SERVERPATH + "/" + socketEvent.ToString());
            sockets.Add(socketEvent, socket);
        }
   
     
        return socket;
    }
 

}
