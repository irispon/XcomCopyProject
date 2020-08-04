using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHelper 
{

    public static string SERVERPATH = "http://localhost:4444";
    public static string DATABASEACESS = SERVERPATH + "/" + PostEvent.database.ToString();
    public static string LOGIN = SERVERPATH + "/" + PostEvent.login;
}

public enum SocketEvent
{
    chat, database
}

public enum PostEvent
{
    certificate, database, login
}
public enum ServerEvent
{
    reconnect, connect, reconnecting, connectError, reconnectError, connectTimeOut, connection, disconnect
}
public enum ChatHelper
{
    Client,Message,Sprite
}