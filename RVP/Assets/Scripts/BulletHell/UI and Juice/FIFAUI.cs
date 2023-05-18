using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FIFAUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public TheManager manager;
    public TheHero hero;
    public ThePlayerController player;
    public ScoreHandler scoreHandler;

    public Image heroHealth;
    public Image heroShield;

    public Image[] PlayerHealth;

    public GameObject[] countDown;

    public bool canAddScore;

    public int score;
    public TextMeshProUGUI scoreText;

    public int distTravelled;
    public TextMeshProUGUI distText;

    public int totalScore;

    [Header("Score Multipliers")]
    public int distMultiplier = 100;
    public int healthmultiplier = 200;

    [Header("Final Score UI")]
    [SerializeField] GameObject highScoreElementUI;
    [SerializeField] Transform elementWrapper;

    List<GameObject> highScoreElements = new List<GameObject>();

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI totalDistText;
    public TextMeshProUGUI healthBonusText;
    public TextMeshProUGUI damageScoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreHandler = GetComponent<ScoreHandler>();

        heroHealth.fillAmount = 1f;
        heroShield.fillAmount = 1f;

        StartCoroutine(StartCountDown());

        score = 0;
        canAddScore = false;
    }

    // Update is called once per frame
    void Update()
    {

        DisplayTime(manager.globalTime);

        heroHealth.fillAmount = hero.currentHealth / hero.maxHealth;
        heroShield.fillAmount = hero.currentShields / hero.maxShields;

        TrackPlayerHP();

        UpdateScore();

        SetDistance();

    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateScore()
    {
        scoreText.text = scoreHandler.score.ToString();
    }

    public void AddScore(int points)
    {
        score = score + points;
    }

    public void SetDistance()
    {
        distText.text = scoreHandler.distTravelled.ToString();
    }

    public void  CalculateTotalScore()
    {
        totalScore = score - Mathf.FloorToInt((distTravelled / distMultiplier)) + (player.healthCounter * healthmultiplier);
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = totalScore.ToString();
        totalDistText.text = distTravelled.ToString();
        healthBonusText.text = (player.healthCounter * healthmultiplier).ToString();
        damageScoreText.text = score.ToString();
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(1f);
        //Show 3
        countDown[0].SetActive(true);
        yield return new WaitForSeconds(1f);

        //Show 2
        countDown[0].SetActive(false);
        countDown[1].SetActive(true);
        yield return new WaitForSeconds(1f);

        //Show 1
        countDown[1].SetActive(false);
        countDown[2].SetActive(true);
        yield return new WaitForSeconds(1f);

        //Show Go!
        countDown[2].SetActive(false);
        countDown[3].SetActive(true);
        manager.start = true;
        //canAddScore = true;
        scoreHandler.canAddScore = true;
        hero.attackController.enabled = true;
        yield return new WaitForSeconds(1f);

        //Hide Go
        countDown[3].SetActive(false);
    }

    void TrackPlayerHP()
    {
        switch(player.healthCounter)
        {
            case 1:
                PlayerHealth[0].enabled = true;
                PlayerHealth[1].enabled = false;
                PlayerHealth[2].enabled = false;
                PlayerHealth[3].enabled = false;
                PlayerHealth[4].enabled = false;
                break;

            case 2:
                PlayerHealth[0].enabled = true;
                PlayerHealth[1].enabled = true;
                PlayerHealth[2].enabled = false;
                PlayerHealth[3].enabled = false;
                PlayerHealth[4].enabled = false;
                break;

            case 3:
                PlayerHealth[0].enabled = true;
                PlayerHealth[1].enabled = true;
                PlayerHealth[2].enabled = true;
                PlayerHealth[3].enabled = false;
                PlayerHealth[4].enabled = false;
                break;

            case 4:
                PlayerHealth[0].enabled = true;
                PlayerHealth[1].enabled = true;
                PlayerHealth[2].enabled = true;
                PlayerHealth[3].enabled = true;
                PlayerHealth[4].enabled = false;
                break;

            case 5:
                PlayerHealth[0].enabled = true;
                PlayerHealth[1].enabled = true;
                PlayerHealth[2].enabled = true;
                PlayerHealth[3].enabled = true;
                PlayerHealth[4].enabled = true;
                break;

            default:
                PlayerHealth[0].enabled = false;
                PlayerHealth[1].enabled = false;
                PlayerHealth[2].enabled = false;
                PlayerHealth[3].enabled = false;
                PlayerHealth[4].enabled = false;
                break;
        }
    }
}
