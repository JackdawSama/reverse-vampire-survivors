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
        StateThree
    }

    public float altTimer;

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
                aimedBulletHell.bulletHell = AimedBulletHell.AimedBulletHellSys.AttackOne;
                
                if(hero.HealthPercentage() < 98f && hero.HealthPercentage() >=90f)
                {
                    stateController = StateController.StateTwo;
                }
                break;

            case StateController.StateTwo:

                aimedAttack.enabled = true;

                aimedAttack.aimedSystem = AimedAttackSys.AimedSystem.ModeOne;
                aimedBulletHell.bulletHell = AimedBulletHell.AimedBulletHellSys.QuadOneThree;
                
                if(hero.HealthPercentage() < 90f && hero.HealthPercentage() >=85f)
                {
                    aimedBulletHell.enabled = false;
                    aimedBulletHell.bulletHellTimer = 0;

                    stateController = StateController.StateThree;
                }
                break;

            case StateController.StateThree:
                bulletHell.enabled = true;
                aimedBulletHell.enabled = true;

                aimedBulletHell.bulletHell = AimedBulletHell.AimedBulletHellSys.QuadOne;
                bulletHell.bulletHell = BulletHellSys.BulletHell.ModeOneAlt;
                aimedAttack.aimedSystem = AimedAttackSys.AimedSystem.ModeThree;
                break;
        }
    }

}
