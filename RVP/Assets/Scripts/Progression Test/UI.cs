using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    //PLAYER VARIABLES
    bool automate = false;
    float spawnCooldown;
    float baseSpawnCD;
    float timer = 0;
    int enemySpawn = 0;
    int souls = 0;
    int enemiesKilled = 0;
    //END PLAYER VARIABLES

    class Enemy
    {
        //ENEMY VARIABLES
        public int enemyLevel = 1;
        public int enemyMaxLevel = 1;
        public float enemyHealth = 25;
        public float enemyDamage = 10;
        public float enemyDamageRange = 5;
        public float enemyDmg;
        public float enemyExp = 10;
        public float baseEnemyExp = 10;
        //END ENEMY VARIABLES
    }

    class Char
    {
        //CHARACTER VARIABLES
        public bool charIsAlive = true;
        string[] PrimaryAttribute = {"Strength", "Agility"};
        public string charPrimaryAttribute;
        public int charLevel = 1;
        public float charExp = 0;
        public float charMaxExp = 100;
        public float totalExp = 0;
        public float charStrength;
        public float charHealth = 100;
        public float charMaxHealth = 100;
        public float healthPtage;
        public float charAgility;
        public float charAttackSpeed = 1.0f;
        public float attackTimer = 0;
        public float charDamage = 12;
        public float charDamageRange = 6;
        public float charDmg;

        public Char()
        {
            int value = Random.Range(0, PrimaryAttribute.Length);
            charPrimaryAttribute = PrimaryAttribute[value];
            if(charPrimaryAttribute == "Strength")
            {
                charStrength = 1.5f;
                charAgility = 1;
                return;
            }

            charStrength = 1;
            charAgility = 1.5f;
            return;
        }
        //END CHARACTER VARIABLES
    }

    Char character = new Char();
    List<Enemy> enemy = new List<Enemy>();


    //OTHER VARIABLES
    int dieRoll;
    int n = 0;
    int healthPotionVal;
    int healthPotionBase = 25;
    //END OTHER VARIABLES

    //GAME START
    void Start()
    {
        //Caching Player Variables
        timer = 0;
        souls = 0;
        automate = false;

        //Caching Character Variables
        character.charExp = 0;
        character.totalExp = 0;
        character.attackTimer = 0;


        //Cahcing Mic. Variables
        n = 0;
        healthPotionVal = healthPotionBase;

    }

    void Update()
    {
        //If character is alive and there are enemies then the battle function is run
        if(character.charIsAlive)
        {
            character.attackTimer += Time.deltaTime;
            if(character.attackTimer >= 1/character.charAttackSpeed)
            {
                Debug.Log("Attack");
                if(enemy.Count > 0)
                {
                    Battle();
                }
                character.attackTimer = 0;
            }

            HealthPercentage();
        
            if(character.healthPtage <= 10)
            {
                FindHealthPotion();
            }
        }

        if(enemiesKilled >= 50)
        {
            //TODO: Add UPGRADE BUTTON. Set Button to active.
        }

    }

    //PLAYER FUNCTIONS
    void SpawnButton()
    {
        //Spawn Character
        enemy.Add(new Enemy());
        enemySpawn++;
    }

    void AutomateButton()
    {
        //Automate spawning

        if(automate && timer >= spawnCooldown)
        {
            SpawnButton();
            timer = 0;
        }
    }

    void BuyAutomateUpgrade()
    {
        //Buy Automate Upgrade
        automate = true;
    }
    //END OF PLAYER FUNCTIONS

    //CHARACTER FUNCTIONS
    void EXPGain()
    {
        //* EXPGain for Character
        character.charExp += enemy[n].enemyExp;
        character.totalExp += character.charExp;
        if (character.charExp >= character.charMaxExp)
        {
            CharLevelUp();
            character.charExp = 0;
            character.charMaxExp = character.charMaxExp * 1.15f;
        }
    }

    void CharLevelUp()
    {
        //* CHARACTER Level Up. 
        //* Increases character lvl, character base dmg, increases attributes 
        character.charLevel++;
        character.charDamage++;
        if(character.charPrimaryAttribute == "Strength")
        {
            character.charStrength = character.charStrength * 1.2f;
            character.charMaxHealth = character.charMaxHealth * 1.2f;
            character.charMaxHealth = Mathf.Floor(character.charMaxHealth);
            character.charAgility = character.charAgility * 1.1f;
            character.charAttackSpeed = character.charAttackSpeed * 1.1f;
            return;
        }

        character.charStrength = character.charStrength * 1.1f;
        character.charMaxHealth = character.charMaxHealth * 1.1f;
        character.charMaxHealth = Mathf.Floor(character.charMaxHealth);
        character.charAgility = character.charAgility * 1.2f;
        character.charAttackSpeed = character.charAttackSpeed * 1.2f;
    }

    void CharDie()
    {
        //Character death
        if(character.charHealth <= 0)
        {
            character.charHealth = 0;
            character.charIsAlive = false;
        }
    }
    //END OF CHARACTER FUNCTIONS

    //ENEMY FUNCTIONS
    void EXPDropGain()
    {
        //* EXPGain for Enemy
        enemy[n].enemyExp = enemy[n].baseEnemyExp * Mathf.Pow(1.12f, enemy[n].enemyLevel - 1);
    }

    void EnemyDie()
    {
        //Enemy death
        if(enemy[n].enemyHealth <= 0)
        {
            souls++;
            enemiesKilled++;
            enemy[n].enemyHealth = 0;
            EXPGain();
            enemy.RemoveAt(n);
            n++;
        }
    }
    //END OF ENEMY FUNCTIONS

    //OTHER FUNCTIONS
    void Battle()
    {
        //Die roll to decide doing damage or taking damage
        dieRoll = Random.Range(1,7);

        if (dieRoll == 1)
        {
            //* Character Attack
            enemy[n].enemyHealth -= character.charDamage;
            Debug.Log("Character dealt Damage : " + character.charDamage + " to enemy");
            return;
        }
        if (dieRoll == 2)
        {
            //* Character Attack
            enemy[n].enemyHealth -= character.charDamage;
            Debug.Log("Character dealt Damage : " + character.charDamage + " to enemy");
            return;
        }
        if (dieRoll == 3)
        {
            //* Character Attack
            enemy[n].enemyHealth -= character.charDamage;
            Debug.Log("Character dealt Damage : " + character.charDamage + " to enemy");
            return;
        }
        if (dieRoll == 4)
        {
            //* Enemy Attack
            character.charHealth -= enemy[n].enemyDamage;
            Debug.Log("Enemy dealt Damage : " + enemy[n].enemyDamage + " to character");
            return;
        }
        if (dieRoll == 5)
        {
            //* Enemy Attack
            character.charHealth -= enemy[n].enemyDamage;
            Debug.Log("Enemy dealt Damage : " + enemy[n].enemyDamage + " to character");
            return;
        }
        if (dieRoll == 6)
        {
            //* Enemy Attack
            character.charHealth -= enemy[n].enemyDamage;
            Debug.Log("Enemy dealt Damage : " + enemy[n].enemyDamage + " to character");
            return;
        }
    }

    void FindHealthPotion()
    {
        //Does a die roll for a heatlh potion
        dieRoll = Random.Range(1, 7);

        if(dieRoll == 1)
        {
            //* Health Potion
            character.charHealth += healthPotionVal;
            return;
        }
        if(dieRoll == 3)
        {
            //* Health Potion
            character.charHealth += healthPotionVal;
            return;
        }
    }

    void KillingEfficiency()
    {
        //Information for how efficient the player is farming
        float efficiency = (enemiesKilled/enemySpawn) * 100;
    }

    void HealthPercentage()
    {
        //Information for how much health the character has
        float healthPercentage = (character.charHealth/character.charMaxHealth) * 100;
        character.healthPtage = healthPercentage;
    }
    //END OF OTHER FUNCTIONS
}
