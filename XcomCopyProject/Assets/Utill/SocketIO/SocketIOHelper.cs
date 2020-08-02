using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketIOHelper 
{
    public static string SERVERPATH = "http://localhost:4444";

}

public enum SocketEvent
{
    chat, database
}
public enum ServerEvent
{
    reconnect, connect, reconnecting, connectError, reconnectError, connectTimeOut, connection, disconnect
}
public enum ChatHelper
{
    Client,Message,Sprite
}