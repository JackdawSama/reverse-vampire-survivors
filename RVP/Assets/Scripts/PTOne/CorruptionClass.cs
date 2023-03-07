using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionClass
{
    //Corrupted variables
    int souls;
    int maxHp;
    int maxDamage;
    int exp;
    bool isAlive;

    //Corrupted Variables End

    public CorruptionClass(bool _isAlive, int _baseHP, int _baseDamage, int _souls)
    {
        isAlive = _isAlive;
        maxHp = _baseHP;
        maxDamage = _baseDamage;
        souls = _souls;
        exp = 100;
        
    }

    public void DeathState()
    {
        //Sets the state of the corrupted to dead
        isAlive = false; 
    }
}
