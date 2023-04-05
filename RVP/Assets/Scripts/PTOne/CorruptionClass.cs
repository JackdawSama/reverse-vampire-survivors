using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionClass
{
    //Corrupted variables
    public int souls;
    public int maxHp;
    public int maxDamage;
    public int exp;
    bool isAlive;

    AvatarScript avatarRef;

    //Corrupted Variables End

    public CorruptionClass(AvatarScript avatar)
    {
        souls = avatar.avatar.soulsSaved;
        maxHp = avatar.avatar.maxHP;
        maxDamage = avatar.avatar.maxDamage;
        avatarRef = avatar;
    }

    public void InitStats()
    {
        CorruptionClass corrupt = new CorruptionClass(avatarRef);
    }

    public void DeathState(AvatarScript avatar)
    {
        //Sets the state of the corrupted to dead
        isAlive = false;
        avatar.avatar.Purge(souls);
    }
}
