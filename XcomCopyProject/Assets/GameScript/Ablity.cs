using System;

[Serializable]
public class Ablity
{
    Character character;
    public AblityType type;
    public void SetChar(Character character)
    {
        this.character = character;
    }
    public void Action()
    {
        switch (type)
        {
            case AblityType.attack:
                break;
            case AblityType.buff:
                break;
            case AblityType.debuff:
                break;
        }
    }
}

public enum AblityType
{
    attack, buff, debuff
}