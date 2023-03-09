using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : MonoBehaviour
{
    //Avatar Ref
    AvatarScript avatar;
    //Avatar Ref End

    //Play Area
    [Header("Play Area")]
    [SerializeField] GameObject playArea;
    [SerializeField] Vector2 playAreaSpawnOffset;
    [SerializeField] Vector2 spawnRegion;
    float width;
    float height;
    //Play Area End

    //Spawner Variables
    [Header("Spawner Variables")]

    [SerializeField] int minSpawnCount;
    [SerializeField] int spawnCount;
    [SerializeField] GameObject minion;
    [SerializeField] List<GameObject> minionList;

    [SerializeField] int corruptedListCount;
    [SerializeField] GameObject[] corruptedList;
    //Spawner Variables end

    //SpawnerTimer Variables
    [SerializeField] float spawnerTimer;
    [SerializeField] float spawnerTimerCooldown;
    [SerializeField] bool spawnClicked;
    //SpawnerTimer Variables End

    //Auto-Spawning Variables
    [SerializeField] float autpoSpawnerTimer;
    [SerializeField] bool autoSpawnerActive;
    [SerializeField] int autoSpawnerCost; 
    //Auto-Spawning Variables End

    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();

        width = GameObject.Find("Play Area").GetComponent<SpriteRenderer>().bounds.size.x;
        height = GameObject.Find("Play Area").GetComponent<SpriteRenderer>().bounds.size.y;

        corruptedList = new GameObject[corruptedListCount];

        spawnClicked = false;
    }

    void Update()
    {
        if(avatar.avatar.isAlive)
        {
            if(spawnClicked)
            {
                spawnerTimer+=Time.deltaTime;

                if(spawnerTimer >= spawnerTimerCooldown)
                {
                    spawnerTimer = 0;
                    spawnClicked = false;
                }
            }
        }
    }

    public void SpawnWave()
    {
        if(!spawnClicked)
        {
            spawnClicked = true;
            for(int i  = 0; i < spawnCount; i++)
            {
                Debug.Log("Spawn Wave");
                CalcRect();
                minionList.Add(Instantiate(minion, spawnRegion, transform.rotation));
            }
            return;
        }

        Debug.Log("Spawn CoolDown");
    }

    public void SpawnCorrupted()
    {

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
            //Width ranges from x to X of larger Rect
            //Height ranges from Y to -Y
            Debug.Log("Case: " + value);
            x = Random.Range(playArea.transform.position.x + width/2, playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y + (height/2 + playAreaSpawnOffset.y), playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            Debug.Log("Point: " + spawnRegion);
            break;

            case(2):
            //Rectangle Bottom Spawn
            //Width ranges from -X to X of larger Rect
            //Height ranges from -y to -Y
            Debug.Log("Case: " + value);
            x = Random.Range(playArea.transform.position.x - (width/2 + playAreaSpawnOffset.x), playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y - height/2, playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            Debug.Log("Point: " + spawnRegion);
            break;

            case(3):
            //Rectangle Left Spawn
            //Width ranges from -x to -X of larger Rect
            //Height ranges from Y to -Y
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