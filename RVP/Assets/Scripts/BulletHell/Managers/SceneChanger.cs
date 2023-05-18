using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Scene currentScene;
    KeyCode startKey = KeyCode.Delete;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if(currentScene.name == "Tutorial")
        {
            if(Input.GetKeyDown(startKey))
            {
                SceneManager.LoadScene("Game Screen");
            }
        }
        else if(currentScene.name == "Highscore")
        {
            if(Input.GetKeyDown(startKey))
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    } 
}
