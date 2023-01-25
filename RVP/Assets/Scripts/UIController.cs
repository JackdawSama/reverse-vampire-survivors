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

    //cooldown variables
    [SerializeField] bool inCD = false;
    [SerializeField] float CD = 3f;
    [SerializeField] float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        cooldownText.gameObject.SetActive(false);
        cooldownImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(inCD)runTimer();
    }

    public void SpawnEnemy()
    {
        if(inCD)
        {
            Debug.Log("Button on cooldown");
        }

        inCD = true;
        cooldownText.gameObject.SetActive(true);
        timer = CD;
        Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void runTimer()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            inCD = false;
            cooldownText.gameObject.SetActive(false);
            cooldownImage.fillAmount = 0;
        }
        else
        {
            cooldownText.text = Mathf.RoundToInt(timer).ToString();
            cooldownImage.fillAmount = timer / CD;
        }
    }
}
