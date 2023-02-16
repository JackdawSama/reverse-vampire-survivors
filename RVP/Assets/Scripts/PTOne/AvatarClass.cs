using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClass
{
    //Player Variables
    public int playerLevel;
    public bool isAlive;

    public int soulsCollected;
    public int soulsSaved;
    //Player Variables end

    //EXP Variables
    int baseExp;
    public int currentExp;
    public int expToNextLevel;
    //EXP Variables end

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
        currentHP = maxHP;
        maxDamage = baseDamage;
        attackSpeed = baseAttackSpeed;
        currentCorruption = 0;
        soulsCollected = 0;
        playerLevel = 1;
    }

    public void PlayerDeath()
    {
        //Sets the avatar to dead
        isAlive = false;
    }

    public void PlayerCorrupted()
    {
        //Deals with post corruption stats
        soulsSaved = 3/10 * soulsCollected;
    }

    public void PlayerReset(bool _isAlive, int _playerLevel, int _baseHP, int _baseDamage, int _baseAttackSpeed, float _corruptionThreshold)
    {
        //Resets the avatar stats
        isAlive = _isAlive;
        playerLevel = _playerLevel;
        baseHP = _baseHP;
        baseDamage = _baseDamage;
        baseAttackSpeed = _baseAttackSpeed;
        corruptionThreshold = _corruptionThreshold;
        
    }
    
    public void Damage()
    {
        //Calculates the damage of the avatar
        currentDamage = Random.Range(maxDamage, maxDamage + 4);
    }

    public void LevelUp()
    {
        //Updates Level and avatar stats
        playerLevel++;
        HealthUp();
        ExpUp();
        DamageUp();
        AttackSpeedUp();
    }

    private void HealthUp()
    {
        maxHP = baseHP + (playerLevel * 5);
    }
    private void ExpUp()
    {
        expToNextLevel = baseExp + (playerLevel * 10);
    }
    private void DamageUp()
    {
        maxDamage = baseDamage + (playerLevel + 2);
    }

    private void AttackSpeedUp()
    {
        attackSpeed = baseAttackSpeed + (playerLevel + 1);
    }

}
