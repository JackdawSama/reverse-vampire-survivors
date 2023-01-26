using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField]GameObject enemyPrefab;
    [SerializeField]Image cooldownImage;
    [SerializeField]TMP_Text cooldownText;
    [SerializeField]Transform spawnPoint;

    //cooldown variables
    [SerializeField] bool inCD = false;
    [SerializeField] float CD = 2f;
    [SerializeField] float timer = 0f;

    //points variables
    PlayerController player;
    int enemiesSpawned = 0;
    [SerializeField]TMP_Text spawnedText;

    public int soulsFarmed = 0;
    [SerializeField]TMP_Text soulsText;


    // Start is called before the first frame update
    void Start()
    {
        cooldownText.text = "Spawn\nEnemy";
        cooldownImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(inCD)runTimer();

        soulsText.text = "Souls Farmed: " + soulsFarmed.ToString();
        spawnedText.text = "Enemies Spawned: " + enemiesSpawned.ToString();
    }

    public void SpawnEnemy()
    {
        if(inCD)
        {
            Debug.Log("Button on cooldown");
            return;
        }

        inCD = true;
        // cooldownText.gameObject.SetActive(true);
        timer = CD;
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemiesSpawned++;
    }

    void runTimer()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            inCD = false;
            cooldownText.text = "Spawn\nEnemy";
            cooldownImage.fillAmount = 0;
        }
        else
        {
            cooldownText.text = Mathf.RoundToInt(timer).ToString();
            cooldownImage.fillAmount = timer / CD;
        }
    }
}
