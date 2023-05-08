using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreenButtons : MonoBehaviour
{
    GameObject targetButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(targetButton.tag == "StartButton")
        {
            SceneManager.LoadScene("Level1");
        }
        else if(targetButton.tag == "QuitButton")
        {
            Application.Quit();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        //
    }
}
