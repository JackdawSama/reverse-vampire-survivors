using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSpawner : MonoBehaviour
{
    [Header("Spawner Variables")]
    public float spawnTimer;
    public float spawnCooldown;
    public int spawnCount;
    public float minSpawnRad;
    public float maxSpawnRad;
    public List<GameObject> enemies = new List<GameObject>();

    [Header("Spawner References")]
    public TheHero hero;

    [Header("Spawner Components")]
    public GameObject enemyPrefab;

    private void Start() 
    {
        spawnTimer = 0f;

        for(int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy();
        }

        // int point = Random.Range(0, enemies.Count);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnCooldown)
        {
            if(!hero)
            {
                return;
            }
            for(int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy;
       
        Vector2 spawnPos = SwarmSpawning();
        enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
        // enemies.Add(enemy);
    }

    private Vector2 SwarmSpawning()
    {
        Vector2 spawnPos;

        float angle = Random.Range(0f, 360f);

        spawnPos.x = hero.transform.position.x + (Random.Range(minSpawnRad, maxSpawnRad) * Mathf.Cos(angle / (180f / Mathf.PI)));
        spawnPos.y = hero.transform.position.y + (Random.Range(minSpawnRad, maxSpawnRad) * Mathf.Sin(angle / (180f / Mathf.PI)));

        return spawnPos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(hero.transform.position, maxSpawnRad);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(hero.transform.position, minSpawnRad);
    }
}
