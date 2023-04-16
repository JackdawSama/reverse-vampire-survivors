using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [Header("Emitter Variables")]
    public bool isActive;
    public float currentHealth;
    public float maxHealth;

    [Header("Rest Timer")]
    public bool startTimer = false;
    public float restTimer;
    public float restCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 && isActive)
        {
            currentHealth = maxHealth;
            Rest();
        }
        if(startTimer)
        {
            restTimer += Time.deltaTime;

            if(restTimer > restCooldown)
            {
                Reset();
            }
        } 
    }

    public void Rest()
    {
        //Switch Off Collider
        //Set animation to Sleep

        Debug.Log("Resting");
        restTimer = 0;
        isActive = false;
        startTimer = true;
    }

    public void Reset()
    {
        //Switch On Collider
        //Set Animation to Awake

        Debug.Log("Awake");
        currentHealth = maxHealth;
        startTimer = false;
        isActive = true;
        restTimer = 0;

    }
}
