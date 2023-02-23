using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : MonoBehaviour
{
    //Spawner References
    [SerializeField]GameObject minionPrefab;
    [SerializeField]List<Transform> spawnPointList;
    //Spawner References end

    //Spawner Timer Variables
    float spawnTimer;
    [SerializeField]float initSpawnCoolDown;
    float currentSpawnCoolDown;

    //Spawner Timer Variables end

    //Spawner Wave Variables
    [SerializeField]int  intWaveCount;
    int currentWaveCount;
    //Spawner Wave Variables end

    //Spawn Region Variables
    //TODO: Add spawn region variables
    //Spawn Region Variables end

    public void SpawnWave()
    {
        for(int i = 0; i < currentWaveCount; i++)
        {
            //Spawn Enemy Wave from a spawn point
            Debug.Log("Spawned Enemy");
        }
    }

    public void UpdateSpawner()
    {
        UpdateWaveNumbers();
        UpdateSpawnTimer();
    }

    private void UpdateWaveNumbers()
    {
        currentWaveCount = intWaveCount + 5;
    }

    private void UpdateSpawnTimer()
    {
        currentSpawnCoolDown = initSpawnCoolDown - 0.1f;
    }
}