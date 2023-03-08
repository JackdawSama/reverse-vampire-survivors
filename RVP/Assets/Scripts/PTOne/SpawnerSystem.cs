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
    [SerializeField] Vector2 spawnRegion;
    float width;
    float height;
    //Play Area End

    //Spawner Variables
    [Header("Spawner Variables")]

    [SerializeField] int minSpawnCount;
    [SerializeField] int spawnCount;
    [SerializeField] List<MinionClass> minionList;

    //GameObject reference;

    void Start()
    {
        //reference = GameObject.Find("Play Area").GetComponent<SpriteRenderer>();

        width = GameObject.Find("Play Area").GetComponent<SpriteRenderer>().bounds.size.x;
        height = GameObject.Find("Play Area").GetComponent<SpriteRenderer>().bounds.size.y;

        Debug.Log(width + ", " + height);
    }

    public void SpawnWave()
    {
        for(int i  = 0; i < spawnCount; i++)
        {
            Debug.Log("Spawn Wave");
            //CalcRectTwo();
            //CalcSpawnRect();
            CalcRect();
        }
    }
    
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playArea.transform.position, playArea.transform.localScale);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playArea.transform.position, playArea.transform.localScale + (Vector3)playAreaSpawnOffset);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(spawnRegion, 0.1f);   
    }

    private void CalcSpawnRect()
    {
        spawnAreaOffset.x = Random.Range(-width/2 - playAreaSpawnOffset.x, width/2 + playAreaSpawnOffset.x);
        spawnAreaOffset.y = Random.Range(playArea.transform.position.y - playAreaSpawnOffset.y/2, playArea.transform.position.y + playAreaSpawnOffset.y/2);

        spawnArea.x = Random.Range(playArea.transform.position.x - playArea.transform.localScale.x / 2, playArea.transform.position.x + playArea.transform.localScale.x / 2);
        spawnArea.y = Random.Range(playArea.transform.position.y - playArea.transform.localScale.y / 2, playArea.transform.position.y + playArea.transform.localScale.y / 2);

        float x = playAreaSpawnOffset.x + spawnArea.x;
        float y = playAreaSpawnOffset.y + spawnArea.y;
        spawnRegion = new Vector2(playArea.transform.position.x + x, playArea.transform.position.y + y);
    }

    private void CalcRectTwo()
    {
        int a = (Random.Range(0, 2) * 2) - 1;                                                                   //random gen for -1 or 1
        spawnRegion.x = Random.Range(playAreaSpawnOffset.x, playArea.transform.localScale.x);       
        spawnRegion.y = Random.Range(playAreaSpawnOffset.y, playArea.transform.localScale.y);

        float x = playArea.transform.position.x + spawnRegion.x;
        float y = playArea.transform.position.y + spawnRegion.y;

        spawnRegion = new Vector2(x, y);
    }

    private void CalcRect()
    {
        int value = Random.Range(0,4);

        float x;
        float y;
        switch (value)
        {
            case(0):
            //Rectangle Top Spawn
            //Width ranges from -X to X of larger Rect
            //Height ranges from y to Y
            Debug.Log("Case: " + value);
            x = Random.Range(playArea.transform.position.x - (width/2 + playAreaSpawnOffset.x), playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y + height/2, playArea.transform.position.y + (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            Debug.Log("Point: " + spawnRegion);
            break;

            case(1):
            //Rectangle Right Spawn
            Debug.Log("Case: " + value);
            x = Random.Range(playArea.transform.position.x + width/2, playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y + (height/2 + playAreaSpawnOffset.y), playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            Debug.Log("Point: " + spawnRegion);
            break;

            case(2):
            //Rectangle Bottom Spawn
            Debug.Log("Case: " + value);
            x = Random.Range(playArea.transform.position.x - (width/2 + playAreaSpawnOffset.x), playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y - height/2, playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            Debug.Log("Point: " + spawnRegion);
            break;

            case(3):
            //Rectangle Left Spawn
            Debug.Log("Case: " + value);
            x = Random.Range(playArea.transform.position.x - (width/2 + playAreaSpawnOffset.x), playArea.transform.position.x - width/2);
            y = Random.Range(playArea.transform.position.y + (height/2 + playAreaSpawnOffset.y), playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            Debug.Log("Point: " + spawnRegion);
            break;

            default:
            Debug.Log("RANDOMGEN OUT OF REQUIRED BOUNDS");
            break;
        }
    }
}