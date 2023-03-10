using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerSystem : MonoBehaviour
{
    //References
    AvatarScript avatar;

    PlayerUIScript uiScript;
    //References End

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
    public List<GameObject> minionList;

    [SerializeField] GameObject corrupted;
    [SerializeField] List<GameObject> corruptedList;
    [SerializeField] List<CorruptionClass> corruptedRef;

    [SerializeField] GameObject automateButton;
    //Spawner Variables end

    //SpawnerTimer Variables
    [SerializeField] float spawnerTimer;
    [SerializeField] float spawnerTimerCooldown;
    [SerializeField] bool spawnClicked;
    //SpawnerTimer Variables End

    //Auto-Spawning Variables
    [SerializeField] float autoSpawnerTimer;
    [SerializeField] float autoSpawnerCoolDown;
    [SerializeField] bool autoSpawnerActive;
    [SerializeField] int autoSpawnerCost; 
    //Auto-Spawning Variables End

    //Corrupted Spawn Variables
    [SerializeField] GameObject corruptedSpawnButton;
    //Corrupted SPawn End Variables

    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        uiScript = GameObject.Find("Canvas").GetComponent<PlayerUIScript>();

        width = GameObject.Find("Play Area").GetComponent<SpriteRenderer>().bounds.size.x;
        height = GameObject.Find("Play Area").GetComponent<SpriteRenderer>().bounds.size.y;

        corruptedList = new List<GameObject>();
        corruptedRef = new List<CorruptionClass>();

        automateButton.SetActive(false);
        autoSpawnerActive = false;

        corruptedSpawnButton.SetActive(false);

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

            if(autoSpawnerActive)
            {
                automateButton.SetActive(false);
                autoSpawnerTimer += Time.deltaTime;

                if(autoSpawnerTimer >= autoSpawnerCoolDown)
                {
                    SpawnWaveTwo();
                    autoSpawnerTimer = 0;
                }
            }
        }

        EnableAutomateButton();
        EnableAutomate();

        if(avatar.avatar.isCorrupted)
        {
            //Adds corrupted to a reference list
            avatar.avatar.isAlive = false;
            corruptedSpawnButton.SetActive(true);
            corruptedRef.Add(new CorruptionClass(avatar));
            corruptedSpawnButton.GetComponentInChildren<TextMesh>().text = "Spawn Corrupted(" + corruptedRef.Count + ")";
            avatar.avatar.isCorrupted = false;
        }
    }

    public void SpawnWave()
    {
        if(!spawnClicked)
        {
            spawnClicked = true;
            for(int i  = 0; i < spawnCount; i++)
            {
                //Debug.Log("Spawn Wave");
                CalcRect();
                minionList.Add(Instantiate(minion, spawnRegion, transform.rotation));
            }
            uiScript.UpdateGUILogs("Wave Spawned");
            uiScript.UpdateGUI();
            return;
        }

        Debug.Log("Spawn CoolDown");
    }

    void SpawnWaveTwo()
    {
        for(int i  = 0; i < spawnCount; i++)
        {
            //Debug.Log("Spawn Wave");
            CalcRect();
            minionList.Add(Instantiate(minion, spawnRegion, transform.rotation));
        }
        uiScript.UpdateGUILogs("Wave Spawned");
        uiScript.UpdateGUI();
    }

    public void EnableAutomateButton()
    {
        if(avatar.avatar.totalSouls > (autoSpawnerCost - 5))
        {
            automateButton.SetActive(true);
            automateButton.GetComponent<Button>().interactable = false; 
            //Debug.Log("Automate Spawner Active");
        }
    }

    public void EnableAutomate()
    {
        if(avatar.avatar.totalSouls > autoSpawnerCost)
        {
            //Debug.Log("Automate Spawner Ready to buy");
            automateButton.GetComponent<Button>().interactable = true;
        }
    }

    public void BuyAutomate()
    {
        if(avatar.avatar.totalSouls > autoSpawnerCost)
        {
            avatar.avatar.totalSouls = avatar.avatar.totalSouls - autoSpawnerCost;
            autoSpawnerActive = true;
            //automateButton.SetActive(false);
        }
    }

    public void SpawnCorrupted()
    {
        if(corruptedRef.Count > 0)
        {
            CalcRect();
            corruptedList.Add(Instantiate(corrupted, spawnRegion, transform.rotation));
            corrupted.GetComponent<CorruptedScript>().corrupted = corruptedRef[0];
            corruptedSpawnButton.GetComponentInChildren<TextMesh>().text = "Spawn Corrupted(" + corruptedList.Count + ")";
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
            
            x = Random.Range(playArea.transform.position.x - (width/2 + playAreaSpawnOffset.x), playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y + height/2, playArea.transform.position.y + (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            break;

            case(1):
            //Rectangle Right Spawn
            //Width ranges from x to X of larger Rect
            //Height ranges from Y to -Y

            x = Random.Range(playArea.transform.position.x + width/2, playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y + (height/2 + playAreaSpawnOffset.y), playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            break;

            case(2):
            //Rectangle Bottom Spawn
            //Width ranges from -X to X of larger Rect
            //Height ranges from -y to -Y

            x = Random.Range(playArea.transform.position.x - (width/2 + playAreaSpawnOffset.x), playArea.transform.position.x + (width/2 + playAreaSpawnOffset.x));
            y = Random.Range(playArea.transform.position.y - height/2, playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            break;

            case(3):
            //Rectangle Left Spawn
            //Width ranges from -x to -X of larger Rect
            //Height ranges from Y to -Y

            x = Random.Range(playArea.transform.position.x - (width/2 + playAreaSpawnOffset.x), playArea.transform.position.x - width/2);
            y = Random.Range(playArea.transform.position.y + (height/2 + playAreaSpawnOffset.y), playArea.transform.position.y - (height/2 + playAreaSpawnOffset.y));
            spawnRegion = new Vector2(x, y);
            break;

            default:
            Debug.Log("RANDOMGEN OUT OF REQUIRED BOUNDS");
            break;
        }
    }
}