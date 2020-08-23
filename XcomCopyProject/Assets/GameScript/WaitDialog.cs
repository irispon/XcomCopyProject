using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaitDialog:SingletonObject<WaitDialog>
{
    [SerializeField]
    Text title,context;
    public void Wait(string title,string context)
    {
        gameObject.SetActive(true);
        this.context.text = context;
        this.title.text = title;
    }

    public void Done()
    {
        gameObject.SetActive(false);
    }

}