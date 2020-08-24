using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaitDialog:SingletonObject<WaitDialog>
{
    [SerializeField]
    Text title,context;
    public void Start()
    {
   
    }
    public void Wait(string title,string context)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        this.context.text = context;
        this.title.text = title;
    }

    public void Done()
    {
        gameObject.SetActive(false);
    }

}