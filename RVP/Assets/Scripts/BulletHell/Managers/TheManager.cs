using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheManager : MonoBehaviour
{
    public int units;
    public float globalTime;
    public KeyCode resetKey;

    // Start is called before the first frame update
    void Start()
    {
        units = 0;
    }

    // Update is called once per frame
    void Update()
    {
        globalTime += Time.deltaTime;

        if(Input.GetKeyDown(resetKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
