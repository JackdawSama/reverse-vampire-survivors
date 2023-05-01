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
        // BulletHell.Chaos
    };

    public enum BulletHell
    {
        Inactive,
        ModeOne,
        ModeOneAlt,
        ModeTwo,
        ModeTwoAlt,
        ModeThree,
        ModeFour,
        Chaos
    }

    [Header("References")]
    public TheHero hero;

    [Header("Checks")]
    public bool isModeOneAlt = false;
    public bool isModeTwoAlt = false;

    [Header("Timers")]
    public float bulletHellTimer; 
    [SerializeField] float bulletHellCooldown;
    public float modeTimer;
    [SerializeField] float modeCooldown = 3f;
    [SerializeField] float altTimer;
    [SerializeField] float altCooldown = 1f;

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

        // altTimer += Time.deltaTime;

        //modeTimer += Time.deltaTime;
        
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

                TypeA();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                if(altTimer >= altCooldown)
                {
                    bulletHell = BulletHell.ModeOneAlt;
                    altTimer = 0;
                }

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;

                    altTimer = 0;
                }
                break;
            }

            case BulletHell.ModeOneAlt:                 //Single Attack EmitterState
            {
                SetPattern(attackTypes[0]);

                TypeAAlt();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                if(altTimer >= altCooldown)
                {
                    bulletHell = BulletHell.ModeOne;
                    altTimer = 0;
                }

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;

                    altTimer = 0;
                }
                break;
            }

            case BulletHell.ModeTwo:                 //Single Attack EmitterState
            {
                SetPattern(attackTypes[1]);

                TypeA("SineCosine");
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));
                
                if(altTimer >= altCooldown)
                {
                    bulletHell = BulletHell.ModeTwoAlt;
                    altTimer = 0;
                }

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;

                    altTimer = 0;
                }

                break;
            }

            case BulletHell.ModeTwoAlt:                 //Single Attack EmitterState
            {
                SetPattern(attackTypes[1]);

                TypeAAlt("SineCosine");
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                if(altTimer >= altCooldown)
                {
                    bulletHell = BulletHell.ModeTwo;
                    altTimer = 0;
                }

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(0, BulletHellStates.Length);
                    bulletHell = BulletHellStates[randomNum];
                    modeTimer = 0;

                    altTimer = 0;
                }

                break;
            }


            case BulletHell.ModeThree:
            {
                SetPattern(attackTypes[2]);

                TypeB();
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

                TypeB("Alt");
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

                TypeChaos();
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

    private void TypeA()
    {
        // new better way
        for (int i = 0; i < projectileAmount; i++)
        {
            //generating on instance
            GameObject bullet = PoolingManager.Instance.GetProjectile(1);
            if(bullet != null)
            {
                bullet.transform.position = emitters[0].position;
                bullet.transform.rotation = emitters[0].rotation;
            }
            //Transform bullet = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;           //saves the Transform reference
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));                          //updates the angle between this and the next bullet
            bullet.transform.rotation = rot;                                                                                     //changes the emitter's current rotation
        }

        // reset the attack timer
        bulletHellTimer = 0f;
    }

    private void TypeA(string letter)
    {
        // new better way
        for (int i = 0; i < projectileAmount; i++)
        {
            //generating on instance
            GameObject bullet = PoolingManager.Instance.GetProjectile(2);
            if(bullet != null)
            {
                bullet.transform.position = emitters[0].position;
                bullet.transform.rotation = emitters[0].rotation;
            }
            // Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, emitters[0].rotation, hero.transform).transform;           //saves the Transform reference
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));                          //updates the angle between this and the next bullet
            bullet.transform.rotation = rot;                                                                                     //changes the emitter's current rotation
        }

        // reset the attack timer
        bulletHellTimer = 0f;
    }

    private void TypeAAlt()
    {
        // new better way
        for (int i = 0; i < projectileAmount; i++)
        {
            //generating on instance
            GameObject bullet = PoolingManager.Instance.GetProjectile(3);
            if(bullet != null)
            {
                bullet.transform.position = emitters[0].position;
                bullet.transform.rotation = emitters[0].rotation;
            }
            // Transform bullet = Instantiate(projectilePrefab[2], emitters[0].position, emitters[0].rotation, hero.transform).transform;           //saves the Transform reference
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));                          //updates the angle between this and the next bullet
            bullet.transform.rotation = rot;                                                                                     //changes the emitter's current rotation
        }

        // reset the attack timer
        bulletHellTimer = 0f;
    }

    private void TypeAAlt(string letter)
    {
        // new better way
        for (int i = 0; i < projectileAmount; i++)
        {
            //generating on instance
            GameObject bullet = PoolingManager.Instance.GetProjectile(4);
            if(bullet != null)
            {
                bullet.transform.position = emitters[0].position;
                bullet.transform.rotation = emitters[0].rotation;
            }
            // Transform bullet = Instantiate(projectilePrefab[3], emitters[0].position, emitters[0].rotation, hero.transform).transform;           //saves the Transform reference
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));                          //updates the angle between this and the next bullet
            bullet.transform.rotation = rot;                                                                                     //changes the emitter's current rotation
        }

        // reset the attack timer
        bulletHellTimer = 0f;
    }

    private void TypeB()
    {
        for (int i = 0; i < projectileAmount; i++)
        {
            // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[1].position, emitters[1].rotation, hero.transform).transform;
            GameObject bulletOne = PoolingManager.Instance.GetProjectile(1);
            if(bulletOne != null)
            {
                bulletOne.transform.position = emitters[1].position;
                bulletOne.transform.rotation = emitters[1].rotation;
            }
            
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            // Transform bulletTwo = Instantiate(projectilePrefab[1], emitters[2].position, emitters[2].rotation, hero.transform).transform;
            GameObject bulletTwo = PoolingManager.Instance.GetProjectile(2);
            if(bulletTwo != null)
            {
                bulletTwo.transform.position = emitters[2].position;
                bulletTwo.transform.rotation = emitters[2].rotation;
            }

            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo; 
        }

        bulletHellTimer = 0f;
    }

    private void TypeB(string letter)
    {
        for (int i = 0; i < projectileAmount; i++)
        {
            // Transform bulletOne = Instantiate(projectilePrefab[2], emitters[1].position, emitters[1].rotation, hero.transform).transform;
            
            GameObject bulletOne = PoolingManager.Instance.GetProjectile(3);
            if(bulletOne != null)
            {
                bulletOne.transform.position = emitters[1].position;
                bulletOne.transform.rotation = emitters[1].rotation;
            }

            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            // Transform bulletTwo = Instantiate(projectilePrefab[3], emitters[2].position, emitters[2].rotation, hero.transform).transform;
            
            GameObject bulletTwo = PoolingManager.Instance.GetProjectile(2);
            if(bulletTwo != null)
            {
                bulletTwo.transform.position = emitters[2].position;
                bulletTwo.transform.rotation = emitters[2].rotation;
            }

            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo; 
        }

        bulletHellTimer = 0f;
    }

    private void TypeChaos()
    {   
        //BH Attack that uses East, West and Origin Emitters
        for(int i = 0; i < projectileAmount; i++)
        {
            // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            
            GameObject bulletOne = PoolingManager.Instance.GetProjectile(1);
            if(bulletOne != null)
            {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
            }

            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            // Transform bulletTwo = Instantiate(projectilePrefab[2], emitters[1].position, emitters[1].rotation, hero.transform).transform;
            
            GameObject bulletTwo = PoolingManager.Instance.GetProjectile(3);
            if(bulletTwo != null)
            {
                bulletTwo.transform.position = emitters[1].position;
                bulletTwo.transform.rotation = emitters[1].rotation;
            }

            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo;

            // Transform bulletThree = Instantiate(projectilePrefab[3], emitters[2].position, emitters[2].rotation, hero.transform).transform;
            
            GameObject bulletThree = PoolingManager.Instance.GetProjectile(4);
            if(bulletThree != null)
            {
                bulletThree.transform.position = emitters[2].position;
                bulletThree.transform.rotation = emitters[2].rotation;
            }
            Quaternion rotThree = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletThree.transform.rotation = rotThree;
        }

        bulletHellTimer = 0f;
    }

    private void TypeChaos2()
    {   
        //BH Attack that uses East, West and Origin Emitters
        for(int i = 0; i < projectileAmount; i++)
        {
            // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            
            GameObject bulletOne = PoolingManager.Instance.GetProjectile(1);
            if(bulletOne != null)
            {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
            }

            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            // Transform bulletTwo = Instantiate(projectilePrefab[2], emitters[1].position, emitters[1].rotation, hero.transform).transform;
            
            GameObject bulletTwo = PoolingManager.Instance.GetProjectile(3);
            if(bulletTwo != null)
            {
                bulletTwo.transform.position = emitters[0].position;
                bulletTwo.transform.rotation = emitters[0].rotation;
            }

            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo;

            // Transform bulletThree = Instantiate(projectilePrefab[3], emitters[2].position, emitters[2].rotation, hero.transform).transform;
            
            GameObject bulletThree = PoolingManager.Instance.GetProjectile(4);
            if(bulletThree != null)
            {
                bulletThree.transform.position = emitters[0].position;
                bulletThree.transform.rotation = emitters[0].rotation;
            }
            Quaternion rotThree = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletThree.transform.rotation = rotThree;
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
