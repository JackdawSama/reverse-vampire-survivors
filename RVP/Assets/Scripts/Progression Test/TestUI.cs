using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestUI : MonoBehaviour
{
    //PLAYER VARIABLES
    [SerializeField]bool automate = false;
    [SerializeField]Button automateButton;
    bool levelSlider;
    [SerializeField]Button lvlSliderButton;
    [SerializeField]Button lvlUpEnemyButton;
    [SerializeField]Slider lvlSlider;
    [SerializeField]Slider expSlider;
    [SerializeField]Slider charHealthSlider; 
    public float spawnCooldown;
    [SerializeField]float baseSpawnCooldown;
    [SerializeField]float spawnCDinterval;
    float timer = 0;
    int enemySpawn = 0;
    int souls = 0;
    int enemiesKilled = 0;
    [SerializeField] float baseCostEnemyUpgrade = 15;
    float enemyUpgradeCost;
    [SerializeField] float currentEnemyLvl = 1;
    [SerializeField] float maxEnemyLvl;
    [SerializeField] float enemyBaseHealth = 25;
    [SerializeField] float enemyHealth;
    [SerializeField] float enemyBaseDamage = 5;
    [SerializeField] float enemyDmg;
    [SerializeField] float enemyBaseExp = 5;
    [SerializeField] float enemyExp; 
    //END PLAYER VARIABLES

    class Enemy
    {
        //ENEMY VARIABLES
        public float enemyLevel;
        public float enemyHealth;
        public float enemyDamage;
        public float enemyExp;

        public Enemy(float lvl, float baseHP, float baseDmg, float baseExp)
        {
            enemyLevel = lvl;
            enemyHealth = baseHP;
            enemyDamage = baseDmg;
            enemyExp = baseExp;
        }
        //END ENEMY VARIABLES
    }

    class Char
    {
        //CHARACTER VARIABLES
        public bool charIsAlive = true;
        public int charLevel = 1;
        public float charExp = 0;
        public float charBaseExp = 50;
        public float charMaxExp;
        public float totalExp = 0;
        public float charHealth;
        public float charMaxHealth = 125;
        public float healthPtage;
        public float charAttackSpeed = 1.2f;
        public float attackTimer = 0;
        public float charDamage;
        public float charBaseDamage = 15;

        public Char(int value)
        {
            charMaxExp = charBaseExp;
            charHealth = charMaxHealth;
            charDamage = charBaseDamage;
            int data = value;
        }
        //END CHARACTER VARIABLES
    }

    Char character = new Char (0);
    List<Enemy> enemy;


    //OTHER VARIABLES
    bool enemyUpgradeAvail = false;
    float globalTimer;
    int dieRoll;
    int n = 0;
    float healthPotionVal;
    [SerializeField]int healthPotionBase = 20;
    [SerializeField]TextMeshProUGUI soulsText;
    //[SerializeField]TextMeshProUGUI healthText;
    [SerializeField]TextMeshProUGUI enemiesKilledText;
    [SerializeField]TextMeshProUGUI lvlUpEnemyText;
    [SerializeField]TextMeshProUGUI globalTimerText;
    [SerializeField]TextMeshProUGUI enemiesSpawnedText;
    [SerializeField]TextMeshProUGUI characterLvlText;
    [SerializeField]TextMeshProUGUI characterDamageText;
    string dataText;
    [SerializeField]TextMeshProUGUI upgradesText;
    [SerializeField]TextMeshProUGUI charAPSText;
    [SerializeField]TextMeshProUGUI enemyCurrentLvlText;
    [SerializeField]TextMeshProUGUI guiLogsText;
    [SerializeField]TextMeshProUGUI youDiedText;
    [SerializeField]List<string> guiLogsArray = new List<string>();
    //END OTHER VARIABLES

    //GAME START
    void Start()
    {
        //Caching Player Variables
        timer = 0;
        souls = 0;
        maxEnemyLvl = 1;
        automate = false;
        automateButton.interactable = false;
        levelSlider = false;
        lvlSliderButton.interactable = false;
        lvlSlider.interactable = false;
        lvlUpEnemyButton.interactable = false;
        expSlider.minValue = 0;
        expSlider.maxValue = character.charMaxExp;
        expSlider.value = character.charExp;

        //Caching Character Variables
        character = new Char(Random.Range(0, 2));
        character.charExp = 0;
        character.totalExp = 0;
        character.attackTimer = 0;
        charHealthSlider.minValue = 0;
        charHealthSlider.maxValue = character.charMaxHealth;
        charHealthSlider.value = character.charHealth;

        //Caching Enemy Variables
        enemy = new List<Enemy>();
        enemyHealth = enemyBaseHealth;
        enemyDmg = enemyBaseDamage;
        enemyExp = enemyBaseExp; 
        spawnCooldown = baseSpawnCooldown;
        spawnCDinterval = 0.2f;


        //Cahcing Mic. Variables
        n = 0;
        globalTimer = 0;
        healthPotionVal = healthPotionBase;
        enemyUpgradeCost = baseCostEnemyUpgrade;

        //Caching UI Variables
        soulsText.text = souls.ToString();
        //healthText.text = "Health: " + character.charHealth.ToString() + "/" +character.charMaxHealth.ToString();
        enemiesKilledText.text = "Enemies Killed: " + enemiesKilled.ToString();
        lvlUpEnemyText.text = "Upgrade Enemy Level: \n" + enemyUpgradeCost.ToString() + " Souls";
        enemiesSpawnedText.text = /*"Enemies Spawned: " +*/ enemySpawn.ToString();
        characterLvlText.text = "Character Lvl: " + character.charLevel.ToString();
        characterDamageText.text = "Character Damage: " + character.charDamage.ToString();
        charAPSText.text = "Character Attack Speed: " + character.charAttackSpeed.ToString();
        enemyCurrentLvlText.text = "Enemy Lvl: " + currentEnemyLvl.ToString();
        guiLogsText.text = " ";
        
        for(int i = 0; i < 5; i++)
        {
            guiLogsArray.Add("-");
        }


    }

    void Update()
    {
        //If character is alive and there are enemies then the battle function is run
        if(character.charIsAlive)
        {
            HealthPercentage();
            if(character.healthPtage <= 15)
            {
                FindHealthPotion();
            }

            if(enemy.Count > 0)
            {
                EnemyDie();
            }

            globalTimer += Time.deltaTime;
            UpdateUI();

            character.attackTimer += Time.deltaTime;
            if(character.attackTimer >= 1/character.charAttackSpeed)
            {
                if(enemy.Count > 0)
                {
                    Fight();
                    Battle();
                    CharDie();
                    if(!character.charIsAlive)
                    {
                        return;
                    }
                }
                character.attackTimer = 0;
            }

            timer += Time.deltaTime;
            if(automate && timer >= 1/spawnCooldown)
            {
                AutomatedSpawn();
                timer = 0;
            }

            if(enemiesKilled >= 8 && !automate)
            {
                //TODO: Add AUTOMATION BUTTON.
                automateButton.interactable = true;
                upgradesText.text = "Automation Available";
            }

            if(enemiesKilled >= enemyUpgradeCost)
            {
                //TODO: Add UPGRADE ENEMY BUTTON.
                if(souls >= enemyUpgradeCost)
                {
                    enemyUpgradeAvail = true;
                }
                if(enemyUpgradeAvail)
                {
                    lvlUpEnemyButton.interactable = true;
                    upgradesText.text = "Enemy Upgrade Available";
                }
            }

            if(enemiesKilled >= 50 && !levelSlider)
            {
                //TODO: Add UPGRADE SLIDER CONTROL.
                lvlSliderButton.interactable = true;
                upgradesText.text = "Level Slider Available";

            }


        }   
    }

    //PLAYER FUNCTIONS
    void UpdateUI()
    {
        //Updates UI
        if(!lvlSlider.interactable)
        {
            currentEnemyLvl = maxEnemyLvl;
            //Debug.Log("Enemy Lvl: " + currentEnemyLvl);
        }
        else
        {
            currentEnemyLvl = lvlSlider.value;
        }

        soulsText.text = souls.ToString();
        //healthText.text = "Health: " + character.charHealth.ToString() + "/" +character.charMaxHealth.ToString();
        enemiesKilledText.text = "Enemies Killed: " + enemiesKilled.ToString();
        lvlUpEnemyText.text = "Upgrade Enemy Lvl: \n" + Mathf.FloorToInt(enemyUpgradeCost).ToString() + " Souls";
        enemiesSpawnedText.text = /*"Enemies Spawned: " +*/ enemySpawn.ToString();
        characterLvlText.text = "Character Lvl: " + character.charLevel.ToString();
        characterDamageText.text = "Character Damage: " + character.charDamage.ToString();
        DisplayTime(globalTimer);

        charAPSText.text = "Character Attack Speed: " + character.charAttackSpeed.ToString();
        enemyCurrentLvlText.text = "Enemy Lvl: " + currentEnemyLvl.ToString();

        expSlider.maxValue = character.charMaxExp;
        expSlider.value = character.charExp;

        charHealthSlider.maxValue = character.charMaxHealth;
        charHealthSlider.value = character.charHealth;

        guiLogsText.text = "[" + globalTimerText.text + "] : " + guiLogsArray[0] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[1] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[2] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[3] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[4] + "\n";

    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        globalTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void SpawnButton()
    {
        //Spawn Character
        enemy.Add(new Enemy(currentEnemyLvl, enemyHealth, enemyDmg, enemyExp));
        enemySpawn++;
    }

    void AutomatedSpawn()
    {
        enemy.Add(new Enemy(currentEnemyLvl, enemyHealth, enemyDmg, enemyExp));
        enemySpawn++;
    }

    public void BuyAutomateUpgrade()
    {
        //Buy Automate Upgrade
        if(souls >= 8)
        {
            automate = true;
            souls -= 8;
            automateButton.interactable = false;
        }
        else{upgradesText.text = "Not enough souls";}
    }

    public void BuySliderUpgrade()
    {
        //Buy Slider Automate
        if(souls < 50)
        {
            upgradesText.text = "Not enough souls";
            return;
        }
        levelSlider = true;
        souls -= 50;
        lvlSlider.interactable = true;
        lvlSlider.value = currentEnemyLvl;
        lvlSliderButton.interactable = false;
    }

    public void BuyEnemyUpgrade()
    {
        //Buy Enemy Upgrade
        if(souls < enemyUpgradeCost)
        {
            upgradesText.text = "Not enough souls";
            return;
        }
        
        if(souls >= enemyUpgradeCost)
        {
            int enemyCost = Mathf.FloorToInt(enemyUpgradeCost);
            souls -= enemyCost;
            EnemyLevelUp();
            lvlUpEnemyButton.interactable = false;
            enemyUpgradeAvail = false;
        }
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
            character.charMaxExp = character.charMaxExp + (10 * character.charLevel);
        }
    }

    void CharLevelUp()
    {
        //* CHARACTER Level Up. 
        //* Increases character lvl, character base dmg, increases attributes 
        character.charLevel++;
        character.charDamage = character.charDamage + character.charLevel;
        character.charDamage = Mathf.Floor(character.charDamage);
        character.charMaxHealth = character.charMaxHealth + character.charLevel;
        character.charAttackSpeed = character.charAttackSpeed + 0.2f;
        Debug.Log(character.charAttackSpeed);
    }

    void CharDie()
    {
        //Character death
        if(character.charHealth <= 0)
        {
            character.charHealth = 0;
            dataText = "Your Champion Died";
            youDiedText.text = "Your Champion Died";
            updateGUILogs(dataText);
            character.charIsAlive = false;
        }
    }
    //END OF CHARACTER FUNCTIONS

    //ENEMY FUNCTIONS
    void EnemyLevelUp()
    {
        currentEnemyLvl++;
        maxEnemyLvl++;
        spawnCooldown = baseSpawnCooldown + (spawnCDinterval * maxEnemyLvl);
        lvlSlider.maxValue = maxEnemyLvl;
        enemyUpgradeCost = baseCostEnemyUpgrade + maxEnemyLvl/1.2f;
        enemyHealth = maxEnemyLvl/2 * (enemyBaseHealth + currentEnemyLvl);
        enemyDmg = enemyBaseDamage + currentEnemyLvl;
        enemyExp = maxEnemyLvl/2 * (enemyBaseExp + currentEnemyLvl);
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
        }
    }
    //END OF ENEMY FUNCTIONS

    //OTHER FUNCTIONS
    void Fight()
    {
        enemy[n].enemyHealth -= character.charDamage;

        dieRoll = Random.Range(1,4);

        if (dieRoll == 1)
        {
            dataText = "Hack!";
            updateGUILogs(dataText);
            return;
        }
        if (dieRoll == 2)
        {
            dataText = "Slash";
            updateGUILogs(dataText);
            return;
        }
        if (dieRoll == 3)
        {
            dataText = "Bonk";
            updateGUILogs(dataText);
            return;
        }
    }
    void Battle()
    {
        //Die roll to decide doing damage or taking damage
        dieRoll = Random.Range(1,7);

        if (dieRoll == 3)
        {
            //* Enemy Attack
            character.charHealth -= enemy[n].enemyDamage;
            dataText = "Aaarrggghhh!";
            updateGUILogs(dataText);
            return;
        }
        if (dieRoll == 4)
        {
            //* Enemy Attack
            character.charHealth -= enemy[n].enemyDamage;
            dataText = "Ouch";
            updateGUILogs(dataText);
            return;
        }
        if (dieRoll == 5)
        {
            //* Enemy Attack
            character.charHealth -= enemy[n].enemyDamage;
            dataText = "Taking Damage!";
            updateGUILogs(dataText);
            return;
        }
        if (dieRoll == 6)
        {
            //* Enemy Attack
            character.charHealth -= enemy[n].enemyDamage;
            dataText = "Under Attack";
            updateGUILogs(dataText);
            return;
        }
    }

    void FindHealthPotion()
    {
        //Does a die roll for a heatlh potion
        dieRoll = Random.Range(1, 11);

        healthPotionVal = healthPotionBase + (0.5f * character.charLevel);
        healthPotionVal = Mathf.Floor(healthPotionVal);

        if(dieRoll == 1)
        {
            //* Health Potion
            dataText = "Not yet dead!";
            updateGUILogs(dataText);
            character.charHealth += healthPotionVal;
            return;
        }
    }

    void HealthPercentage()
    {
        //Information for how much health the character has
        character.healthPtage = (character.charHealth/character.charMaxHealth) * 100;
    }

    void updateGUILogs(string newLog)
    {
        //Updates the GUI logs
        guiLogsArray.RemoveAt(0);
        guiLogsArray.Add(newLog);
    }
    //END OF OTHER FUNCTIONS
}
