using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileChace : SingletonObject<ProfileChace>
{
    public List<SpriteInfo> profiles;

    public override void Init()
    {
        profiles = new List<SpriteInfo>();
    }
 
}
public struct SpriteInfo
{
    public Sprite sprite;
    public string path;

}