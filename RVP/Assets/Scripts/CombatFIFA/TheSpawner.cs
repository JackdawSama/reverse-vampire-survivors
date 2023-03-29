using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSpawner : MonoBehaviour
{
    [Header("Spawner Variables")]
    public float spawnTimer;
    public float spawnCooldown;
    public int spawnCount;
    public List<GameObject> enemies = new List<GameObject>();

    [Header("Spawner Components")]
    public GameObject enemyPrefab;

    private void Start()
    {
        spawnTimer = 0f;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnCooldown)
        {
            // for(int i = 0; i < spawnCount; i++)
            // {
            //     SpawnEnemy();
            // }
            // spawnTimer = 0f;

            Debug.Log("WAVE SPAWNED");
            spawnTimer = 0f;
        }

    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemies.Add(enemy);
    }


}
