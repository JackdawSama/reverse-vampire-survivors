using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheManager : MonoBehaviour, TheObserver
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Notify(TheAction action)
    {
        if(action == TheAction.ControlledUnitDie)
        {
            Debug.Log("Unit Dead");
            Debug.Log("New Unit Switched");
        }
    }
}
