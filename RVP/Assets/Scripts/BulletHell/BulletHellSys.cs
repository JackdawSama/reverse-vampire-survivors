using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSys : MonoBehaviour
{   
    [Header("References")]
    public TheHero hero;

    [Header("Timers")]
    [SerializeField] float bulletHellTimer; 
    [SerializeField] float bulletHellCooldown;
    [SerializeField] float modeTimer;
    [SerializeField] float modeCooldown = 3f;

    [Header("Emitter Data")]
    public Transform[] emitters;
    public GameObject[] projectilePrefab;
    public List<AttackTypes> attackTypes;
    float emitterAngle, projectileAngle, projectileAmount;

    [Header("Bullet Hell State")]
    public BulletHell bulletHell;
    public BulletHell bulletHellRefState;
    public enum BulletHell
    {
        Inactive,
        ModeOne,
        ModeTwo,
        ModeThree,
        ModeFour,
        Chaos
    }

    // Start is called before the first frame update
    void Start()
    {
        hero = GetComponent<TheHero>();

        bulletHell = BulletHell.Inactive;
        SetPattern(attackTypes[0]);
    }

    // Update is called once per frame
    void Update()
    {
        bulletHellTimer += Time.deltaTime;
        
        if(bulletHellTimer >= bulletHellCooldown)
        {
            BulletHellHandler();
            bulletHellTimer = 0;
        }
    }

    void BulletHellHandler()
    {
        switch (bulletHell)
        {
            case BulletHell.Inactive:              //Attack Switch state to allow for routine to end switch to another state
            {
                if(!hero.shieldsActive)
                {
                    //Do a health check and based on health percentage, set the bullet hell state
                    if(hero.HealthPercentage() > 75f)
                    {
                        bulletHell = BulletHell.ModeOne;
                    }
                }
                break;
            }

            case BulletHell.ModeOne:                 //Single Attack EmitterState
            {
                SetPattern(attackTypes[0]);

                ContinuousSE();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                if(hero.HealthPercentage() <= 75f && hero.HealthPercentage() > 50f)
                {
                    bulletHell = BulletHell.ModeThree;
                }

                break;
            }

            case BulletHell.ModeTwo:                 //Single Attack EmitterState
            {
                modeTimer += Time.deltaTime;
                SetPattern(attackTypes[1]);

                ContinuousSE();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                if(hero.HealthPercentage() <= 25f)
                {
                    modeTimer = 0;
                    bulletHell = BulletHell.Chaos;
                }

                if(modeTimer >= modeCooldown)
                {
                    bulletHell = BulletHell.ModeFour;
                    modeTimer = 0;
                }

                break;
            }


            case BulletHell.ModeThree:
            {
                SetPattern(attackTypes[2]);

                ContinuousDE();
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  + emitterAngle));
                emitters[2].rotation = Quaternion.Euler(emitters[2].eulerAngles.x, emitters[2].eulerAngles.y, (emitters[2].eulerAngles.z  + emitterAngle));   

                if(hero.HealthPercentage() <= 50f && hero.HealthPercentage() > 25f)
                {
                    bulletHell = BulletHell.ModeTwo;
                }

                break;
            }

            case BulletHell.ModeFour:
            {
                modeTimer += Time.deltaTime;
                SetPattern(attackTypes[3]);

                ContinuousDE();
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  - emitterAngle));
                emitters[2].rotation = Quaternion.Euler(emitters[2].eulerAngles.x, emitters[2].eulerAngles.y, (emitters[2].eulerAngles.z  - emitterAngle));   

                if(hero.HealthPercentage() <= 25f)
                {
                    bulletHell = BulletHell.Chaos;
                }

                if(modeTimer >= modeCooldown)
                {
                    bulletHell = BulletHell.ModeFour;
                    modeTimer = 0;
                }
                
                break;
            }

            case BulletHell.Chaos:
            {
                bulletHellRefState = BulletHell.Chaos;
                SetPattern(attackTypes[4]);

                Chaos();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z));
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  + emitterAngle));
                emitters[2].rotation = Quaternion.Euler(emitters[2].eulerAngles.x, emitters[2].eulerAngles.y, (emitters[2].eulerAngles.z  - emitterAngle));  

                break;
            }

            default:
                break;
        }
    }

    private void ContinuousSE()
    {
        // new better way
        for (int i = 0; i < projectileAmount; i++)
        {
            //generating on instance
            Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, emitters[0].rotation).transform;           //saves the Transform reference
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));                          //updates the angle between this and the next bullet
            bullet.transform.rotation = rot;                                                                                     //changes the emitter's current rotation
        }

        // reset the attack timer
        bulletHellTimer = 0f;
    }

    private void ContinuousDE()
    {
        //BH Attack that uses East and West Emitter - Fast and Slow Projectiles //TODO - Set Projectiles to be Fast and Slow
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[1].position, emitters[1].rotation).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[1], emitters[2].position, emitters[2].rotation).transform;
            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo; 
        }

        bulletHellTimer = 0f;
    }

    private void Chaos()
    {   
        //BH Attack that uses East, West and Origin Emitters
        for(int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[2], emitters[1].position, emitters[1].rotation).transform;
            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo;

            Transform bulletThree = Instantiate(projectilePrefab[3], emitters[2].position, emitters[2].rotation).transform;
            Quaternion rotThree = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotThree;
        }

        bulletHellTimer = 0f;
    }

    private void SetPattern(AttackTypes attackData)
    {
        bulletHellCooldown = attackData.AttackCoolDown;
        emitterAngle = attackData.EmitterAngle;
        projectileAngle = attackData.ProjectileAngle;
        projectileAmount = attackData.Projectiles;
    }
}
