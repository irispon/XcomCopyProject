using System;
using System.Collections;
using System.Collections.Generic;
using socket.io;

using UnityEngine;
using WebSocketSharp.Server;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string scene;
    [SerializeField]
    private List<Loader> loaders;
    // Start is called before the first frame update
    void Start()
    {

        List<ILoader> iloaders = new List<ILoader>(loaders);
        LoadingManager.LoadScene(scene, loaders: iloaders);

    }


}
