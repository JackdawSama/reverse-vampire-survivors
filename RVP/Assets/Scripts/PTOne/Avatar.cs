using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : Subject
{
    //Avatar Variables
    [Header("Avatar Variables")]
    public bool isAlive;
    public bool isCorrupted;

    public int totalSouls;
    public int soulsCollected;
    //End

    //Level Up Variables
    [Header("Level Up Variables")]
    public int level;

    [SerializeField] float baseExp;
    [SerializeField] float currentExp;
    [SerializeField] float expToNextLevel;
    //End

    //HP Variables
    [Header("HP Variables")]
    [SerializeField] float baseHitPoints;
    [SerializeField] float currentHitPoints;
    [SerializeField] float maxHitPoints;
    //End

    //Damage Variables
    [Header("Damage Variables")]
    [SerializeField] float baseDamage;
    [SerializeField] float currentDamage;
    [SerializeField] float maxDamage;
    //End

    //Attack Speed Variables
    [Header("Attack Speed Variables")]
    [SerializeField] float baseAttackSpeed;
    [SerializeField] float currentAttackSpeed;
    //End

    //Corruption Variables
    [Header("Corruption Variables")]
    [SerializeField] float currentCorruption;
    [SerializeField] float baseCorruptionThreshold;
    [SerializeField] float corruptionThreshold;
    //End

    void Start()
    {
        // InitialiseAvatar();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            InitialiseAvatar();
        }
    }

    public void InitialiseAvatar()
    {
        //Set Hit Points
        maxHitPoints = baseHitPoints;
        currentHitPoints = maxHitPoints;

        //Set Damage
        SetDamage();

        //Set Attack Speed

        //Set Corruption
        //? Set Threshold
        currentCorruption = 0;

        //Set Souls
        soulsCollected = 0;

        //Set Avatar Level
        level = 1;

        //Set Avatar XP
        currentExp = 0;

        NotifyObservers(Actions.AvatarInitialise);
    }

    public void Die()
    {
        isAlive = false;
        //!Set OnNotify to pass values to respawn avatar with new values here
        DestroyImmediate(this);
    }


    public void Corrupted()
    {
        isCorrupted = true;
        passStats();
        //!Set variables to be passed to create corrupted avatar type
    }

    public int Liberated()
    {
        return soulsCollected;
    }

    public void passStats()
    {

    }

    public float SetDamage()
    {
        return currentDamage = Random.Range(maxDamage - 4, maxDamage);
    }

    public void Corrupt(float corruption)
    {
        currentCorruption += corruption;
    }

    public void GainExP(float exp)
    {
        currentExp += exp;
        if(currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        HealthUp();
        ExpUp();
        DamageUp();
        AttackSpeedUp();
    }

    private void HealthUp()
    {
        // maxHitPoints = baseHitPoints + (level * )
    }

    private void ExpUp()
    {

    }

    private void DamageUp()
    {

    }

    private void AttackSpeedUp()
    {

    }

    public void TakeDamage()
    {

    }
}
