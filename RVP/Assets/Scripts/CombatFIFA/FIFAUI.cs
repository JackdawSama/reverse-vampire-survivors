using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FIFAUI : MonoBehaviour
{
    public TextMeshProUGUI yardsText;
    public TextMeshProUGUI unitsText;

    public TheManager manager;

    //int yards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yardsText.text = "" + Mathf.FloorToInt(manager.yards);
        unitsText.text = "" + manager.units;
    }
}
