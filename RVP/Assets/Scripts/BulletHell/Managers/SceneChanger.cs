using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   private void Update()
   {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Game Screen");
        }
   } 
}
