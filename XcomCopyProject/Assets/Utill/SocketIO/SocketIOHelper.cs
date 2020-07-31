using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketIOHelper 
{
    public static string SERVERPATH = "http://localhost:4444";

}

public enum SocketEvent
{
    chat
}
public enum SystemEvent
{
    reconnect, connect, reconnecting, connectError, reconnectError, connectTimeOut
}