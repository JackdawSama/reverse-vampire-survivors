using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionClass
{
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

    public MinionClass()
    {
        //Constructor for Minion Class
        //Sets the base stats of the minion and then adjusts them based on the player level
        //must take player level as one of its parameters
    }
}
