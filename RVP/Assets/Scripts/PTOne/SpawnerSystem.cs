using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : MonoBehaviour
{
    //Play Area
    [Header("Play Area")]
    [SerializeField] GameObject playArea;
    [SerializeField] Vector2 playAreaSpawnOffset;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] Vector2 spawnAreaOffset;
    Vector2 spawnRegion;
    //Play Area End

    //Spawner Variables
    [Header("Spawner Variables")]

    [SerializeField] int minSpawnCount;
    [SerializeField] int spawnCount;
    [SerializeField] List<MinionClass> minionList;

    public void SpawnWave()
    {
        for(int i  = 0; i < spawnCount; i++)
        {
            Debug.Log("Spawn Wave");
            CalcRectTwo();
        }
    }
    
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playArea.transform.position, playArea.transform.localScale);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playArea.transform.position, playArea.transform.localScale + (Vector3)playAreaSpawnOffset);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(spawnRegion, 0.1f);   
    }

    private void CalcSpawnRect()
    {
        spawnAreaOffset.x = Random.Range(playArea.transform.position.x - playAreaSpawnOffset.x, playArea.transform.position.x + playAreaSpawnOffset.x);
        spawnAreaOffset.y = Random.Range(playArea.transform.position.y - playAreaSpawnOffset.y, playArea.transform.position.y + playAreaSpawnOffset.y);

        spawnArea.x = Random.Range(playArea.transform.position.x - playArea.transform.localScale.x / 2, playArea.transform.position.x + playArea.transform.localScale.x / 2);
        spawnArea.y = Random.Range(playArea.transform.position.y - playArea.transform.localScale.y / 2, playArea.transform.position.y + playArea.transform.localScale.y / 2);

        float x = playAreaSpawnOffset.x - spawnArea.x;
        float y = playAreaSpawnOffset.y - spawnArea.y;
        spawnRegion = new Vector2(playArea.transform.position.x - x, playArea.transform.position.y - y);
    }

    private void CalcRectTwo()
    {
        int a = (Random.Range(0, 2) * 2) - 1;
        spawnRegion.x = Random.Range(-playAreaSpawnOffset.x, -playArea.transform.localScale.x) * a;
        spawnRegion.y = Random.Range(-playAreaSpawnOffset.y, -playArea.transform.localScale.y) * a;

        float x = playArea.transform.position.x + spawnRegion.x;
        float y = playArea.transform.position.y + spawnRegion.y;
    }
}