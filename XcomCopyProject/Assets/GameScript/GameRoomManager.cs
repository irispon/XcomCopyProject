using socket.io;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using xcopy;

public class GameRoomManager:SingletonObject<GameRoomManager>
{
    Socket inGame;

    static public string roomName="testRoom";
    UnitManager units;
    PlayerCache cache;

    public void Start()
    {
        cache = PlayerCache.GetInstance();
        ConnectRoom(roomName);//테스트용
    }
    public async void Match()
    {
        //   WaitDialog.
   //     WaitDialog.GetInstance().Wait("서칭중", "매칭이 진행되고 있습니다.");
    //    roomName =   await MatchingRequest();
    
    }



    private Task<string> MatchingRequest()
    {
      WWWForm form = new WWWForm();
      form.AddField("id", cache.id);
      form.AddField("token", cache.token);
      Task<string> match=  Request.AsyncPostRequest(ServerHelper.SERVERPATH + "/" + SocketEvent.room.ToString() + "/"+GameServerCommand.Match,form);

        return match;

    }
    public void ConnectRoom(string roomName)
    {

        if (inGame != null)
        {
            inGame.Emit(ServerEvent.disconnect.ToString());
            Destroy(inGame.gameObject);
        }
        inGame = Socket.Connect(ServerHelper.SERVERPATH + "/" + SocketEvent.room.ToString()+"/"+roomName);
        inGame.On("connect", () => {
            Debug.Log("서버 접속" + inGame.IsConnected);
           inGame.On(GameServerCommand.Move.ToString(), (string boardSet)=> {
               //서버는. 모든 캐릭터 오브젝트의 좌표를 가지고 있다. 호출시 마다 캐릭터의 좌표를 갱신해서 보내준다.
               //[{name:xx,position:(11,11,11)},{name:xx,position:(11,11,11)}]
               //



           });
            inGame.On(GameServerCommand.Attack.ToString(), (string attack) => {
                //누가- 어떻게- 누구에게- 어디로 
                //{user:userName ,who:unitName, skill:xxx, to:unitName,position:(x,x,x,x)//선택 옵션.}


            });

            inGame.On(GameServerCommand.Turn.ToString(), (string turn) => {
                //{active: username } 유저의 턴 활성화. 만약 자신이 아니면 비활성화 시키면 될 듯.

            });

            inGame.On(GameServerCommand.Dead.ToString(), (string deadUnit) => {
                //{user:userName,active: unitName }

            });

        });

    }
    public void EmitAttack()
    {

    }

    public void EmitMove()
    {

    }
    public void EmitTurn()
    {

    }
}

enum GameServerCommand
{
    Move,Attack,Turn,Dead,Match
}