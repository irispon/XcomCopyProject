using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStatus weapon;
   
    public bool Shoot(Character target)
    {
        if (weapon.magazine > 0)
        {
        //    target.GetDamage(weapon.projectile.damage);
        // 발사 모션
            return true;
        }

        return false;


    }

}
[SerializeField]
public struct WeaponStatus
{
    public WeaponType type;
    public Projectile projectile;
    public int range;
    public int magazine;


}
public enum WeaponType
{
    Sword,Gun,Melee
}
