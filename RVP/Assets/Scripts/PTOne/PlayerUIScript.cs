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
    }

    // Update is called once per frame
    void Update()
    {
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

    }
}
