using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameChange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    InputField changeName;

    public void Click()
    {
        PlayerCache.GetInstance().SetName(changeName.text);
    }
}
