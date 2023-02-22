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
    [SerializeField] float CD;
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
        timer = CD;
    }

    // Update is called once per frame
    void Update()
    {
        if(inCD)runTimer();

        soulsText.text = "Souls: " + soulsFarmed.ToString();
        spawnedText.text = "Enemies: " + enemiesSpawned.ToString();
    }

    public void SpawnEnemy()
    {
        if(inCD)
        {
            Debug.Log("COOLDOWN!");
            return;
        }

        inCD = true;
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemiesSpawned++;
        spawnPoint.position = new Vector3(9, Random.Range(-4f, 4f), 0.1f);
    }

    void runTimer()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            inCD = false;
            timer = CD;
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
