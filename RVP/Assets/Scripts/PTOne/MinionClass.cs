using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionClass
{
    //Reference Variables
    int avatarLevel;
    //Reference Variables end

    //Minion Variables
    public bool isAlive = false;
    //Minion Variables end

    //HP Variables
    int baseHP;
    public int currentHP;
    public int maxHP;
    //HP Variables end

    //EXP Variables
    int baseExp;
    public int currentExp;
    //EXP Variables end

    //Attack Variables
    int baseDamage;
    public int currentDamage;
    public int maxDamage;
    //Attack Variables end

    public MinionClass(int _avatarLevel, int _baseHP, int _baseDamage, int _baseExp)
    {
        //Constructor for Minion Class
        //Sets the base stats of the minion and then adjusts them based on the player level
        //must take player level as one of its parameters

        avatarLevel = _avatarLevel;
        baseHP = _baseHP;
        baseDamage = _baseDamage;
        baseExp = _baseExp;
    }

    public void InitStats()
    {
        //Sets the INITIAL stats of the minion
        maxHP = SetBaseHP(baseHP);
        currentHP = maxHP;
        maxDamage = SetBaseDamage(baseDamage);
        currentExp = SetBaseEXP(baseExp);
    }

    int SetBaseHP(int value)
    {
        value = value + (avatarLevel + 5);
        return value;
    }

    int SetBaseEXP(int value)
    {
        value = value + (avatarLevel * 2);
        return value;

    }

    int SetBaseDamage(int value)
    {
        value = value + (avatarLevel + 1);
        return value;
    }
}
