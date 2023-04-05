using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheManager : MonoBehaviour
{
    public float yards;
    public int units;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cam = GameObject.Find("Main Camera");
        cam.SetActive(false);

        units = 0;
        yards = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
