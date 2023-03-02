using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    //References
    AvatarScript avatar;
   [SerializeField] GameObject enemyPrefab;
    //References End

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
            enemyList.Add(Instantiate(enemyPrefab, spawnPointList[i].position, Quaternion.identity));
        }
    }
}
