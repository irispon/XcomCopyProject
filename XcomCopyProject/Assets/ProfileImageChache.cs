using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileImageChache : SingletonObject<ProfileImageChache>
{
   public Dictionary<string, Sprite> profileImage;
    // Start is called before the first frame update
    public override void Init()
    {
        profileImage = new Dictionary<string, Sprite>();
        DontDestroyOnLoad(this);
    }


}
