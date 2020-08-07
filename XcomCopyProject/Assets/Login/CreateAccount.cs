using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CreateAccount : MonoBehaviour
{

    void Create()
    {
        WWWForm form = new WWWForm();
        Task<string> post = Request.PostRequest(ServerHelper.CREATEACCOUNT(), form);

    }

}
