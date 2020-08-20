using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : SingletonObject<DeckManager>
{
   List<Character> player;
   List<Character> other;

    public override void Init()
    {
        other = new List<Character>();
        player = new List<Character>();
    }
    public void SetPlayer(List<Character> deck)
    {

    }
    public void GetPlayer()
    {

        player.Clear();
 
    }
    public void GetOther()
    {
        other.Clear();
    }
}
