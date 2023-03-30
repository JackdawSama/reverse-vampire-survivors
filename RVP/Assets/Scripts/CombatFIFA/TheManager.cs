using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject cam = GameObject.Find("Main Camera");
        cam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
