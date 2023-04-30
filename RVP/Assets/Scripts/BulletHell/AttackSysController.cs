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

    private void Start() 
    {
        hero = GetComponent<TheHero>();

        bulletHell = GetComponent<BulletHellSys>();
        aimedAttack = GetComponent<AimedAttackSys>();
        aimedBulletHell = GetComponent<AimedBulletHell>();    
    }
}
