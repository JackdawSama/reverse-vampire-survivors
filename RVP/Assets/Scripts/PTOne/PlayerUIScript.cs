using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    //References
    AvatarScript avatar;
   [SerializeField] GameObject enemyPrefab;
   [SerializeField] List<AvatarScript> corruptedAvatarList;
    //References End

    //Avatar Variables
    [SerializeField] int currentLevel;
    [SerializeField] int lastLevel;
    [SerializeField] int currentHP;
    [SerializeField] int maxHP;
    [SerializeField] int currentAttackSpeed;
    [SerializeField] int maxDamage;
    [SerializeField] int currentSouls;
    [SerializeField] int currentExp;
    [SerializeField] int expToNextLevel;
    //Avatar Variables End

    //Avatar UI Variables
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI attackSpeedText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI soulsText;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider expBar;

    [SerializeField]List<string> guiLogsArray = new List<string>();
    [SerializeField] TextMeshProUGUI guiLogsText;
    [SerializeField] float globalTimer;
    [SerializeField] TextMeshProUGUI globalTimerText;

    [SerializeField] float lasCorruption;
    //Avatar UI Variables End

    //Enemy Variables
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] List<Transform> spawnPointList;
    [SerializeField] int spawnCount;
    public int minionLevel;
    //End Enemy Variables


    int totalSouls;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        lastLevel = currentLevel;


        for(int i = 0; i < 5; i++)
        {
            guiLogsArray.Add("-");
        }
    }

    // Update is called once per frame
    void Update()
    {
        globalTimer += Time.deltaTime;

        UpdateGUI();

        updateAvatarStats();
        updateAvatarUI();
        
    }

    public void SpawnEnemyWave()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            int j = i % 4;
            enemyList.Add(Instantiate(enemyPrefab, spawnPointList[j].position, Quaternion.identity));
        }
    }

    void updateAvatarStats()
    {
        currentLevel = avatar.avatar.playerLevel;
        currentHP = avatar.avatar.currentHP;
        maxHP = avatar.avatar.maxHP;
        currentAttackSpeed = avatar.avatar.attackSpeed;
        maxDamage = avatar.avatar.maxDamage;
        currentSouls = avatar.avatar.soulsCollected;
        currentExp = avatar.avatar.currentExp;
        expToNextLevel = avatar.avatar.expToNextLevel;
    }

    void updateAvatarUI()
    {
        levelText.text = "Level: " + currentLevel;
        attackSpeedText.text = "Attack Speed: " + currentAttackSpeed;
        damageText.text = "Damage: " + maxDamage;
        soulsText.text = "Souls: " + currentSouls;

        healthBar.value = currentHP;
        healthBar.maxValue = maxHP;

        expBar.value = currentExp;
        expBar.maxValue = expToNextLevel;
    }

    public void UpdateMinion()
    {
        minionLevel++;
    }

    public void SpawnCorrupted()
    {
        Debug.Log("SPAWN CORRUPTED");
    }

    public void CheckLevel()
    {
        if(currentLevel > lastLevel)
        {
            updateGUILogs("Avatar Level Up! Level : " + currentLevel);
            lastLevel = currentLevel;
        }
    }

    public void CheckCorruption()
    {
        if(avatar.avatar.corruptionThreshold - avatar.avatar.currentCorruption <= 1.5f)
        {
            updateGUILogs("Avatar nearing corruption");
            //lasCorruption = avatar.avatar.currentCorruption;
        }
    }

    void UpdateGUI()
    {
        DisplayTime(globalTimer);

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

    void updateGUILogs(string newLog)
    {
        //Updates the GUI logs
        guiLogsArray.RemoveAt(0);
        guiLogsArray.Add(newLog);
    }
}
