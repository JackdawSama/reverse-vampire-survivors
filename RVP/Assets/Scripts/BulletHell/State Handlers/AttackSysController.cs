using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSysController : MonoBehaviour
{
    //Script to control the other Attack Systems

    [Header("Attack System Reference")]
    public BulletHellSys bulletHell;
    public AimedAttackSys aimedAttack;
    public AimedBulletHell aimedBulletHell;

    [Header("Hero Reference")]
    public TheHero hero;

    [Header("State Machine")]
    public StateController stateController;
    public enum StateController
    {
        StateOne,
        StateTwo,
        StateThree,
        StateFour,
        StateFive,
        StateSix
    }

    public float altTimer;
    public float altCooldown = 2f;

    private void Start() 
    {
        hero = GetComponent<TheHero>();

        bulletHell = GetComponent<BulletHellSys>();
        aimedAttack = GetComponent<AimedAttackSys>();
        aimedBulletHell = GetComponent<AimedBulletHell>();

        bulletHell.enabled = false;
        aimedAttack.enabled = false;
        aimedBulletHell.enabled = false;

        stateController = StateController.StateOne;   
    }

    private void Update() 
    {
        StateHandler();
    }

    private void StateHandler()
    {
        altTimer += Time.deltaTime;
        switch(stateController)
        {
            case StateController.StateOne:
                
                aimedBulletHell.enabled = true;

                if(altTimer >= altCooldown)
                {
                    aimedBulletHell.bulletHell = aimedBulletHell.AttackOneArray[Random.Range(0, aimedBulletHell.AttackOneArray.Length)];
                    
                    altTimer = 0;
                }
                
                if(hero.HealthPercentage() <= 95f)
                {
                    //set attack systems to false
                    aimedBulletHell.enabled = false;

                    //reset timers
                    aimedBulletHell.bulletHellTimer = 0;
                    
                    //set next state's attack
                    bulletHell.bulletHell = bulletHell.PatternArrayOne[Random.Range(0, bulletHell.PatternArrayOne.Length)];
                    aimedAttack.aimedSystem = aimedAttack.TripleArray[Random.Range(0, aimedAttack.TripleArray.Length)];

                    stateController = StateController.StateTwo;
                }
                break;

            case StateController.StateTwo:

                //aimedBulletHell.enabled = true;
                bulletHell.enabled = true;
                aimedAttack.enabled = true;

                aimedAttack.aimCooldown = 1f;

                if(altTimer >= altCooldown)
                {
                    bulletHell.bulletHell = bulletHell.PatternArrayOne[Random.Range(0, bulletHell.PatternArrayOne.Length)];
                    // aimedBulletHell.bulletHell = aimedBulletHell.SingleArray[Random.Range(0, aimedBulletHell.SingleArray.Length)];
                    aimedAttack.aimedSystem = aimedAttack.TripleArray[Random.Range(0, aimedAttack.TripleArray.Length)];
                    altTimer = 0;
                }
                
                if(hero.HealthPercentage() <= 70f)
                {
                    //set attack systems to false
                    aimedBulletHell.enabled = false;
                    bulletHell.enabled = false;
                    aimedAttack.enabled = false;

                    // reset timers
                    bulletHell.bulletHellTimer = 0;
                    aimedBulletHell.bulletHellTimer = 0;
                    aimedAttack.aimTimer = 0;
                    altTimer = 0;

                    // set next state's attack
                    aimedAttack.aimedSystem = aimedAttack.ChaosArray[Random.Range(0, aimedAttack.ChaosArray.Length)];
                    // aimedBulletHell.bulletHell = aimedBulletHell.DoubleArray[Random.Range(0, aimedBulletHell.DoubleArray.Length)];
                    bulletHell.bulletHell = bulletHell.PatternArrayTwo[Random.Range(0, bulletHell.PatternArrayTwo.Length)];

                    stateController = StateController.StateThree;
                }
                break;

            case StateController.StateThree:

                //aimedBulletHell.enabled = true;
                bulletHell.enabled = true;

                if(altTimer >= altCooldown)
                {
                    // aimedBulletHell.bulletHell = aimedBulletHell.DoubleArray[Random.Range(0, aimedBulletHell.DoubleArray.Length)];
                    bulletHell.bulletHell = bulletHell.PatternArrayTwo[Random.Range(0, bulletHell.PatternArrayTwo.Length)];
                    altTimer = 0;
                }

                if(hero.HealthPercentage() <= 55f)
                {
                    //set attack systems to false
                    aimedBulletHell.enabled = false;
                    bulletHell.enabled = false;
                    aimedAttack.enabled = false;

                    // reset timers
                    bulletHell.bulletHellTimer = 0;
                    aimedBulletHell.bulletHellTimer = 0;
                    aimedAttack.aimTimer = 0;
                    altTimer = 0;

                    // set next state's attack
                    bulletHell.bulletHell = bulletHell.PatternArrayThree[Random.Range(0, bulletHell.PatternArrayThree.Length)];
                    aimedBulletHell.bulletHell = aimedBulletHell.DoubleArray[Random.Range(0, aimedBulletHell.DoubleArray.Length)];
                    // aimedAttack.aimedSystem = aimedAttack.ChaosArray[Random.Range(0, aimedAttack.ChaosArray.Length)];

                    stateController = StateController.StateFour;
                }

                break;

            case StateController.StateFour:

                bulletHell.enabled = true;
                // aimedAttack.enabled = true;

                aimedAttack.aimCooldown = 3f;
                altCooldown = 3.5f;


                if(altTimer >= altCooldown)
                {
                    bulletHell.bulletHell = bulletHell.PatternArrayThree[Random.Range(0, bulletHell.PatternArrayThree.Length)];
                    aimedBulletHell.bulletHell = aimedBulletHell.DoubleArray[Random.Range(0, aimedBulletHell.DoubleArray.Length)];

                    // aimedAttack.aimedSystem = aimedAttack.ChaosArray[Random.Range(0, aimedAttack.ChaosArray.Length)];
                    altTimer = 0;
                }

                if(hero.HealthPercentage() <= 35f)
                {
                    //set attack systems to false
                    aimedBulletHell.enabled = false;
                    bulletHell.enabled = false;
                    aimedAttack.enabled = false;

                    // reset timers
                    bulletHell.bulletHellTimer = 0;
                    aimedBulletHell.bulletHellTimer = 0;
                    aimedAttack.aimTimer = 0;
                    altTimer = 0;

                    // set next state's attack
                    // bulletHell.bulletHell = BulletHellSys.BulletHell.Chaos;
                    bulletHell.bulletHell = bulletHell.PatternArrayFour[Random.Range(0, bulletHell.PatternArrayFour.Length)];

                    stateController = StateController.StateFive;
                }

                break;

            case StateController.StateFive:

                bulletHell.enabled = true;

                // bulletHell.bulletHell = BulletHellSys.BulletHell.Chaos;

                if(altTimer >= altCooldown)
                {
                    bulletHell.bulletHell = bulletHell.PatternArrayFour[Random.Range(0, bulletHell.PatternArrayFour.Length)];

                    altTimer = 0;
                }

                if(hero.HealthPercentage() <= 15f)
                {
                    //set attack systems to false
                    aimedBulletHell.enabled = false;
                    bulletHell.enabled = false;
                    aimedAttack.enabled = false;

                    // reset timers
                    bulletHell.bulletHellTimer = 0;
                    aimedBulletHell.bulletHellTimer = 0;
                    aimedAttack.aimTimer = 0;
                    altTimer = 0;

                    //set next state's attack
                    // bulletHell.bulletHell = bulletHell.PatternArrayFour[Random.Range(0, bulletHell.PatternArrayFour.Length)];
                    // aimedAttack.aimedSystem = AimedAttackSys.AimedSystem.ModeOne;

                    bulletHell.bulletHell = BulletHellSys.BulletHell.Chaos;

                    stateController = StateController.StateSix;
                }

                break;
            
            case StateController.StateSix:

                bulletHell.enabled = true;

                bulletHell.bulletHell = BulletHellSys.BulletHell.Chaos;
                // aimedAttack.enabled = true;

                // aimedAttack.aimCooldown = 5f;

                // if(altTimer >= altCooldown)
                // {
                //     bulletHell.bulletHell = bulletHell.PatternArrayFour[Random.Range(0, bulletHell.PatternArrayFour.Length)];
                // }

                break;
        }
    }

}
