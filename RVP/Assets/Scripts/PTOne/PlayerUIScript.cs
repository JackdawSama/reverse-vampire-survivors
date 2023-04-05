using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour, IObserver
{
    //References
    AvatarScript avatar;
    [SerializeField] List<AvatarScript> corruptedAvatarList;

    [SerializeField] Subject avatarSubject;
    //References End

    //Avatar Variables
    [SerializeField] int currentLevel;
    [SerializeField] int lastLevel;
    [SerializeField] int currentHP;
    [SerializeField] int maxHP;
    [SerializeField] float currentAttackSpeed;
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

    [SerializeField]List<string> guiLogsArray = new List<string>();
    [SerializeField] TextMeshProUGUI guiLogsText;
    [SerializeField] float globalTimer;
    [SerializeField] TextMeshProUGUI globalTimerText;

    [SerializeField] GameObject avatarDiedText;
    [SerializeField] GameObject corruptedAvatarText;
    //Avatar UI Variables End

    //Avatar Respawn Button
    [SerializeField] GameObject avatarRespawnButton; //! MOVE THIS TO SPAWNER SYSTEM
    //Avatar Respawn Button End


    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        lastLevel = currentLevel;

        avatarRespawnButton.SetActive(false);
        avatarDiedText.SetActive(false);
        corruptedAvatarText.SetActive(false);

        //avatarSubject = GameObject.Find("Avatar").GetComponent<AvatarScript>();

        

        for(int i = 0; i < 5; i++)
        {
            guiLogsArray.Add("-");
        }

        UpdateGUI(); 
    }

    // Update is called once per frame
    void Update()
    {
        globalTimer += Time.deltaTime;

        //CheckCorruption();
        //CheckLevel();
        CheckAvatarState();

        updateAvatarStats();
        updateAvatarUI();

        DisplayTime(globalTimer);

        
    }

    private void OnEnable() 
    {
        avatarSubject.AddObserver(this);
    }

    private void OnDisable() 
    {
        avatarSubject.RemoveObserver(this);
    }

    public void OnNotify(Actions action)
    {
        if(action == Actions.AvatarInitialise)
        {
            Debug.Log("Avatar Init");
            UpdateGUILogs("Avatar Init");
            UpdateGUI();   
        }
    }

    void updateAvatarStats()
    {
        currentLevel = avatar.avatar.playerLevel;
        currentHP = avatar.avatar.currentHP;
        maxHP = avatar.avatar.maxHP;
        currentAttackSpeed = avatar.avatar.attackSpeed;
        maxDamage = avatar.avatar.maxDamage;
        currentSouls = avatar.avatar.totalSouls;
        currentExp = avatar.avatar.currentExp;
        expToNextLevel = avatar.avatar.expToNextLevel;
    }

    void updateAvatarUI()
    {
        levelText.text = "Lvl. " + currentLevel;
        attackSpeedText.text = "ATK : " + currentAttackSpeed + "s";
        damageText.text = "DMG : " + maxDamage;
        soulsText.text = "Souls: " + currentSouls;

        healthBar.value = currentHP;
        healthBar.maxValue = maxHP;

        expBar.value = currentExp;
        expBar.maxValue = expToNextLevel;
    }

    public void CheckLevel()
    {
        if(currentLevel > lastLevel)
        {
            UpdateGUILogs("Avatar Level Up! Level : " + currentLevel);
            lastLevel = currentLevel;
            UpdateGUI();
        }
    }

    public void CheckCorruption()
    {
        if(avatar.avatar.corruptionThreshold - avatar.avatar.currentCorruption <= 1.5f)
        {
            UpdateGUILogs("Avatar nearing corruption");
        }
    }

    public void RespawnAvatar()
    {
        //avatar.avatar.PlayerReset(true, avatar.startLevel,avatar.startHP, avatar.startDamage, avatar.startAttackSpeed, avatar.corruptionThreshold);
        avatar.avatar.RespawnNewHero();
        avatar.gameObject.SetActive(true);
        avatarDiedText.SetActive(false);
        corruptedAvatarText.SetActive(false);
        avatarRespawnButton.SetActive(false);
    }

    public void UpdateGUI()
    {
        DisplayTime(globalTimer);

        guiLogsText.text = "[" + globalTimerText.text + "] : " + guiLogsArray[0] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[1] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[2] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[3] + "\n" +
                           "[" + globalTimerText.text + "] : " + guiLogsArray[4] + "\n";
    }

    void CheckAvatarState()
    {
       if(!avatar.avatar.isAlive)
        {
            avatarDiedText.SetActive(true);
            UpdateGUILogs("Avatar Died!");
            UpdateGUI();
            //set respawnbutton to active
            avatarRespawnButton.SetActive(true);
        }

        else if(avatar.avatar.isCorrupted)
        {
            corruptedAvatarText.SetActive(true);
            UpdateGUILogs("Avatar Corrupted!");
            UpdateGUI();
            //set respawnbutton to active
            avatarRespawnButton.SetActive(true);
        } 
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        globalTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateGUILogs(string newLog)
    {
        //Updates the GUI logs
        guiLogsArray.RemoveAt(0);
        guiLogsArray.Add(newLog);
    }
}
