using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClass
{
    //Player Variables
    public int playerLevel;
    public bool isAlive;
    public bool isCorrupted;

    public int totalSouls;
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
    public float baseCorruptionThreshold;
    //Corruption Variables end

    public AvatarClass(bool _isAlive, int _playerLevel, int _baseHP, int _baseDamage, int _baseAttackSpeed, int _baseExp, float _corruptionThreshold)
    {
        isAlive = _isAlive;
        playerLevel = _playerLevel;
        baseHP = _baseHP;
        baseDamage = _baseDamage;
        baseAttackSpeed = _baseAttackSpeed;
        baseCorruptionThreshold = _corruptionThreshold;
        baseExp = _baseExp;
    }

    public void InitStats()
    {
        //Sets the INITIAL stats of the avatar
        maxHP = baseHP;
        currentHP = maxHP;

        maxDamage = baseDamage;
        attackSpeed = baseAttackSpeed;

        SetCorruptionThreshold();
        currentCorruption = 0;
        
        totalSouls=0;
        soulsCollected = 0;
        
        playerLevel = 1;
        currentExp = 0;

        ExpUp();
    }

    public void PlayerDeath()
    {
        //Sets the avatar to dead
        isAlive = false;
        soulsSaved = soulsCollected;

    }

    public void Corrupt(float corruption)
    {
        //Deals with corruption
        currentCorruption += corruption;
    }

    public void Corrupted()
    {
        //Alters the Avatar stats and updates stats for a corrupted Avatar
        isCorrupted = true;
        soulsSaved = 3/10 * soulsCollected;
        maxHP = 4/5 * maxHP;
        maxDamage = 1/2 * maxDamage;
        attackSpeed = 2/3 * attackSpeed;

        Debug.Log("Corrupted");
    }

    public int Liberated()
    {
        //Deals with post corruption stats
        return soulsSaved;
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
    
    public int Damage()
    {
        //Calculates the damage of the avatar
        return currentDamage = Random.Range(maxDamage, maxDamage + 4);
    }

    public void GainEXP(int expDrop)
    {
        //Gains EXP from killing a minion
        currentExp += expDrop;
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        //Updates Level and avatar stats
        playerLevel++;
        totalSouls = soulsCollected;
        soulsCollected = 0;
        HealthUp();
        ExpUp();
        DamageUp();
        AttackSpeedUp();

    }

    public void RespawnNewHero()
    {
        //Function to respawn a new hero based on the old Avatar's stats
        isAlive = true;
        if(playerLevel > 3)
        {
            playerLevel = playerLevel - 2;
        }
        else if(playerLevel <= 3)
        {
            playerLevel = 1;
        }
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
        currentExp = 0;
        expToNextLevel = baseExp + (playerLevel * 10);
    }
    private void DamageUp()
    {
        maxDamage = baseDamage + (playerLevel + 2);
    }

    private void AttackSpeedUp()
    {
        attackSpeed = baseAttackSpeed/(playerLevel + 1);
    }

    public void TakeDamage(int damage)
    {
        //Avatar that deal damage to the avatar
        currentHP -= damage;
    }

    public void SetCorruptionThreshold()
    {
        //Function to set the corruption threshold of the avatar
        float margin = 0.2f;
        
        int a = (Random.Range(0, 2) * 2) -1;

        corruptionThreshold = Random.Range(baseCorruptionThreshold, baseCorruptionThreshold + (a * margin));
    }

}
