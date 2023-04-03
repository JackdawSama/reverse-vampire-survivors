using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FIFAUI : MonoBehaviour
{
    public TextMeshProUGUI yardsText;
    public TextMeshProUGUI unitsText;

    public TheManager manager;
    public TheHero hero;

    public Slider heroHealth; 

    //int yards;

    // Start is called before the first frame update
    void Start()
    {
        heroHealth.minValue = 0;
        heroHealth.maxValue = hero.maxHealth;
        heroHealth.value = hero.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        yardsText.text = "" + Mathf.FloorToInt(manager.yards * 5);
        unitsText.text = "" + manager.units;
        
        heroHealth.value = hero.currentHealth;
    }
}
