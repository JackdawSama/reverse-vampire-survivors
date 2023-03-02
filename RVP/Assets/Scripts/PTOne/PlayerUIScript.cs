using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    //References
    AvatarScript avatar;
   [SerializeField] GameObject enemyPrefab;
    //References End

    //Avatar Variables
    [SerializeField] int currentLevel;
    [SerializeField] int currentHP;
    [SerializeField] int maxHP;
    [SerializeField] int currentAttackSpeed;
    [SerializeField] int maxDamage;
    //Avatar Variables End

    //Avatar UI Variables

    //Avatar UI Variables End

    //Enemy Variables
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] List<Transform> spawnPointList;
    [SerializeField] int spawnCount;
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
    }
}
