using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FIFAUI : MonoBehaviour
{
    public TextMeshProUGUI yardsText;
    public TextMeshProUGUI unitsText;
    public TextMeshProUGUI timeText;

    public TheManager manager;
    public TheHero hero;
    public ThePlayerController player;

    public Slider heroHealth;
    public Slider playerHealth; 

    //int yards;

    // Start is called before the first frame update
    void Start()
    {
        heroHealth.minValue = 0;
        heroHealth.maxValue = hero.maxHealth;
        heroHealth.value = hero.currentHealth;

        playerHealth.minValue = 0;
        playerHealth.maxValue = player.maxHealth;
        playerHealth.value = player.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(manager.globalTime);
        //yardsText.text = "" + Mathf.FloorToInt(manager.yards * 5);
        unitsText.text = "" + manager.units;
        
        heroHealth.value = hero.currentHealth;
        playerHealth.value = player.currentHealth;
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
