using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCache : SingletonObject<PlayerCache>,ICallBack
{
    public string id;
    public string token;
    public string userName="NoName";
    public string profile;
    Task<string> userData;


    public override void Init()
    {
        DontDestroyOnLoad(this);
    }
    /// <summary>
    /// token을 받아오는 메서드입니다. 설정이 아닙니다.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    public void Set(string id,string token)
    {
        this.id = id;
        this.token = token;
        //여기서 플레이어 정보 요청 id,token
      //  WWWForm from = new WWWForm();
      //  userData = Post.PostRequest("", from,this);
    
    }
    public void UpdateInfo(string profile,string userName)
    {
       // player 프로파일 유저네임 서버에 업데이트

    }
    private void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("id", id);
        //     Task<string> task=  Post.PostRequest(ServerHelper.SERVERPATH + "/" + "disconnect", form);
        Debug.Log("어플리케이션 종료");
    
    }
    public void Back(string message)
    {
        if (message.Equals(PostEvent.error.ToString()))
        {
            Debug.Log("재요청 내지는 게임 서버 오류로 게임 종료");
        }
        else if(message.Equals(PostEvent.success.ToString()))
        {

            Debug.Log("아이디 갱신");
        }
    }

}
