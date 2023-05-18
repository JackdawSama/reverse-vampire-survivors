using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [Header("Score Variables")]
    public int score;
    public int totalScore;
    public int distTravelled;
    public bool canAddScore;
    public string playerName;

    [Header("Save Data")]
    public List<HighscoreElement> highscoreList = new List<HighscoreElement>();
    [SerializeField] int maxCount = 7;
    [SerializeField] string filename;


    [Header("Score Multipliers")]
    public int distMultiplier = 100;
    public int healthmultiplier = 200;

    [Header("References")]
    public ThePlayerController player;
    public TheHero hero;

    [Header("Final Score UI")]
    [SerializeField] GameObject highScoreElementUI;
    [SerializeField] Transform elementWrapper;

    List<GameObject> highScoreElements = new List<GameObject>();

    void Start()
    {
        score = 0;
        canAddScore = false;

        LoadHighScores();
        UpdateUI(highscoreList);
    }

    void Update()
    {
        if(player)
        {
            SetDistance();
        }

        if(hero){
            if(!hero.isActive)
            {
                CalculateTotalScore();
                AddHighScore(new HighscoreElement(playerName, totalScore));
            }
        }
    }

    private void LoadHighScores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighscoreElement>(filename);

        while(highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(maxCount);
        }
    }

    private void SaveHighScores()
    {
        FileHandler.SaveToJSON<HighscoreElement>(highscoreList, filename);
    }

    public void AddHighScore(HighscoreElement element)
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(i >= highscoreList.Count || element.points > highscoreList[i].points)
            {
                highscoreList.Insert(i, element);

                while(highscoreList.Count > maxCount)
                {
                    highscoreList.RemoveAt(maxCount);
                }

                SaveHighScores();

                Debug.Log(i);
                break;              //breaks loop because anything below this would be lower so have to break out of loop to avoid rewriting that
            }
        }
    }

    public void AddScore(int points)
    {
        score = score + points;
    }

    public void SetDistance()
    {
        distTravelled = player.CalculateDistanceTravelled();
    }

    public void  CalculateTotalScore()
    {
        totalScore = score + (player.healthCounter * healthmultiplier);
    }

    public void UpdateUI(List<HighscoreElement> list)
    {
        if(!highScoreElementUI)
        {
            return;
        }
        for(int i = 0; i < list.Count; i++)
        {
            HighscoreElement el = list[i];

            if(el.points > 0)
            {
                if(i >= highScoreElements.Count)
                {
                    //instantiate new entry
                    var inst = Instantiate(highScoreElementUI, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper, false);

                    highScoreElements.Add(inst);
                }

                var texts = highScoreElements[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = el.playerName;
                texts[1].text = el.points.ToString();
            }
        }
    }

    
}