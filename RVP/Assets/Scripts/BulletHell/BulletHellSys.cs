using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSys : MonoBehaviour
{   
    [Header("Bullet Hell State")]
    public BulletHell bulletHell;
    public BulletHell bulletHellRefState;
    public BulletHell[] BulletHellStates = 
    {
        BulletHell.Inactive, 
        BulletHell.ModeOne, 
        BulletHell.ModeTwo, 
        BulletHell.ModeThree, 
        BulletHell.ModeFour, 
        BulletHell.Chaos,
        BulletHell.Chaos2
    };

    public enum BulletHell
    {
        Inactive,
        ModeOne,
        ModeTwo,
        ModeThree,
        ModeFour,
        Chaos,
        Chaos2
    }

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

        // modeTimer += Time.deltaTime;
        
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
                bulletHell = BulletHell.ModeOne;
                break;
            }

            case BulletHell.ModeOne:                 //Single Attack EmitterState
            {
                SetPattern(attackTypes[0]);

                ContinuousSE();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;
                }
                break;
            }

            case BulletHell.ModeTwo:                 //Single Attack EmitterState
            {
                modeTimer += Time.deltaTime;
                SetPattern(attackTypes[1]);

                ContinuousSE();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
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

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;
                }

                break;
            }

            case BulletHell.ModeFour:
            {
                modeTimer += Time.deltaTime;
                SetPattern(attackTypes[3]);

                ContinuousDE();
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  - emitterAngle));
                emitters[2].rotation = Quaternion.Euler(emitters[2].eulerAngles.x, emitters[2].eulerAngles.y, (emitters[2].eulerAngles.z  + emitterAngle));   

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
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

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;
                }

                break;
            }

            case BulletHell.Chaos2:
            {
                bulletHellRefState = BulletHell.Chaos;
                SetPattern(attackTypes[4]);

                Chaos2();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z));
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  + emitterAngle));
                emitters[2].rotation = Quaternion.Euler(emitters[2].eulerAngles.x, emitters[2].eulerAngles.y, (emitters[2].eulerAngles.z  - emitterAngle));  

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;
                }

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
            Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, emitters[0].rotation, hero.transform).transform;           //saves the Transform reference
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));                          //updates the angle between this and the next bullet
            bullet.transform.rotation = rot;                                                                                     //changes the emitter's current rotation
        }

        // reset the attack timer
        bulletHellTimer = 0f;
    }

    private void ContinuousDE()
    {
        //BH Attack that uses East and West Emitter - Sine and Cos //TODO - Set Projectiles to be Fast and Slow
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[1].position, emitters[1].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[1], emitters[2].position, emitters[2].rotation, hero.transform).transform;
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
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[2], emitters[1].position, emitters[1].rotation, hero.transform).transform;
            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo;

            Transform bulletThree = Instantiate(projectilePrefab[3], emitters[2].position, emitters[2].rotation, hero.transform).transform;
            Quaternion rotThree = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletThree.transform.rotation = rotThree;
        }

        bulletHellTimer = 0f;
    }

    private void Chaos2()
    {   
        //BH Attack that uses East, West and Origin Emitters
        for(int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
            Instantiate(projectilePrefab[0], emitters[0].position + 1.8f * bulletOne.right, bulletOne.rotation, hero.transform);
            Instantiate(projectilePrefab[0], emitters[0].position - 1.8f * bulletOne.right, bulletOne.rotation, hero.transform);
            bulletOne.transform.rotation = rotOne;

            // Transform bulletTwo = Instantiate(projectilePrefab[2], emitters[1].position, emitters[1].rotation).transform;
            // Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            // bulletTwo.transform.rotation = rotTwo;

            // Transform bulletThree = Instantiate(projectilePrefab[3], emitters[2].position, emitters[2].rotation).transform;
            // Quaternion rotThree = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            // bulletThree.transform.rotation = rotThree;
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
