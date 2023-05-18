using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Scene currentScene;
    public KeyCode startKey = KeyCode.Delete;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.name);
    }

    private void Update()
    {
        if(Input.GetKeyDown(startKey))
        {
            Debug.Log("Start Key Pressed");

            if(currentScene.name == "Tutorial")
            {
                SceneManager.LoadScene("Game Screen");
                return;
            }
            if(currentScene.name == "HighScore")
            {
                SceneManager.LoadScene("Tutorial");
                return;
            } 
        }
    } 
}
