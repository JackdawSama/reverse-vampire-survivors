using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClass
{
    //Player Variables
    public int playerLevel;
    public bool isAlive;
    //Player Variables end

    //HP Variables
    int baseHP;
    public int currentHP;
    public int maxHP;
    //HP Variables end

    //Attack Variables
    int baseDamage;
    public int currentDamage;
    public int maxDamage;

    int baseAttackSpeed;
    public int attackSpeed;
    //Attack Variables end

    //Corruption Variables
    public float currentCorruption;
    public float corruptionThreshold;
    //Corruption Variables end

    public AvatarClass(bool _isAlive, int _playerLevel, int _baseHP, int _baseDamage, int _baseAttackSpeed, float _corruptionThreshold)
    {
        isAlive = _isAlive;
        playerLevel = _playerLevel;
        baseHP = _baseHP;
        baseDamage = _baseDamage;
        baseAttackSpeed = _baseAttackSpeed;
        corruptionThreshold = _corruptionThreshold;
    }

    public void InitStats()
    {
        //Sets the INITIAL stats of the avatar
        maxHP = baseHP;
    }

    public void LevelUp()
    {
        //Updates Level and avatar stats
        playerLevel++;
    }

    private void healthUp()
    {

    }
    private void expUp()
    {

    }
}
