using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionClass
{
    //Reference Variables
    public int minionLevel;
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

    //Corruption Variables
    [SerializeField]float baseCorruptVal;
    public float corruptVal;
    //Corruption Variables end

    //Attack Variables
    int baseDamage;
    public int currentDamage;
    public int maxDamage;
    //Attack Variables end

    public MinionClass(int _level, int _baseHP, int _baseDamage, int _baseExp, float _corruption)
    {
        //Constructor for Minion Class
        //Sets the base stats of the minion and then adjusts them based on the player level
        //must take player level as one of its parameters

        minionLevel = _level;
        baseHP = _baseHP;
        baseDamage = _baseDamage;
        Debug.Log("Base Damage: " + baseDamage);
        baseExp = _baseExp;
        baseCorruptVal = _corruption;
    }

    public void InitStats()
    {
        //Sets the INITIAL stats of the minion
        maxHP = SetBaseHP(baseHP);
        currentHP = maxHP;
        maxDamage = SetBaseDamage(baseDamage);
        Debug.Log("Max Damage: " + maxDamage);
        currentExp = SetBaseEXP(baseExp);
        corruptVal = SetCorruption(baseCorruptVal);
    }

    int SetBaseHP(int value)
    {
        value = value + (minionLevel + 5);
        return value;
    }

    int SetBaseEXP(int value)
    {
        value = value + (minionLevel * 2);
        return value;

    }

    int SetBaseDamage(int value)
    {
        value = value + (minionLevel + 1);
        return value;
    }

    float SetCorruption(float value)
    {
        // value = value - baseCorruptVal/avatarLevel;
        return value;
    }
}
