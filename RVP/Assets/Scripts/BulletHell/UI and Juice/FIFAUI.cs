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

    public Image heroHealth;
    public Image heroShield;

    // public Slider heroHP;
    // public Slider heroShields;
    public Slider playerHealth; 

    // Start is called before the first frame update
    void Start()
    {
        heroHealth.fillAmount = 1f;
        heroShield.fillAmount = 1f;

        playerHealth.minValue = 0;
        playerHealth.maxValue = player.maxHealth;
        playerHealth.value = player.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(manager.globalTime);
        unitsText.text = "" + manager.units;

        heroHealth.fillAmount = hero.currentHealth / hero.maxHealth;
        heroShield.fillAmount = hero.currentShields / hero.maxShields;
        
        playerHealth.value = player.currentHealth;
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
