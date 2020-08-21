using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameSender : MonoBehaviour
{
    // Start is called before the first frame update

    public void MoveUnit(string unitId,Vector3 position)
    {

        UnityWebRequest uwr = UnityWebRequest.Post(uri, form);
        Request.PostSend(ServerHelper.SERVERPATH);
    }

}
