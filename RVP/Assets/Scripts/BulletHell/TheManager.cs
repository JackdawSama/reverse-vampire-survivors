using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheManager : MonoBehaviour
{
    public int units;
    public float globalTime;

    // Start is called before the first frame update
    void Start()
    {
        units = 0;
    }

    // Update is called once per frame
    void Update()
    {
        globalTime += Time.deltaTime;
    }
}
