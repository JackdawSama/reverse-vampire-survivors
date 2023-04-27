using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSys : MonoBehaviour
{
    [SerializeField] float bulletHellTimer; 
    [SerializeField] float bulletHellCooldown;

    public List<AttackTypes> attackTypes;
    public GameObject[] projectilePrefab;

    public Transform[] emitters;
    public float emitterAngle, projectileAngle, projectileAmount;

    public BulletHell bulletHell;
    public BulletHell bulletHellRefState;
    public enum BulletHell
    {
        //SE - Single Emitter, DE - Double Emitter
        //N - North, S - South, E - East, W - West
        attackSwitch,
        SingleSineSlow,
        SingleSine,
        SineNCosChaos,
        DEEW,
        TENSO,
        TEEWO
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletHell = BulletHell.SingleSineSlow;
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
            case BulletHell.attackSwitch:              //Attack Switch state to allow for routine to end switch to another state
            {

                //Use to switch attacks once attacks have been defined
                if(bulletHellRefState == BulletHell.attackSwitch)
                {
                    bulletHell = BulletHell.SingleSine;
                }
                else if(bulletHellRefState == BulletHell.SingleSine)
                {
                    bulletHell = BulletHell.SingleSineSlow;
                }
                else if(bulletHellRefState == BulletHell.SingleSineSlow)
                {
                    bulletHell = BulletHell.SingleSine;
                }
                else if(bulletHellRefState == BulletHell.TENSO)
                {
                    bulletHell = BulletHell.TEEWO;
                }
                else if(bulletHellRefState == BulletHell.TEEWO)
                {
                    bulletHell = BulletHell.TENSO;
                }
                else if(bulletHellRefState == BulletHell.SineNCosChaos)
                {
                    bulletHell = BulletHell.DEEW;
                }
                else if(bulletHellRefState == BulletHell.DEEW)
                {
                    bulletHell = BulletHell.SineNCosChaos;
                }

                break;
            }

            case BulletHell.SingleSine:                 //Single Attack EmitterState
            {
                bulletHellRefState = BulletHell.SingleSine;

                SetPattern(attackTypes[0]);

                SimpleSine();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                break;
            }

            case BulletHell.SingleSineSlow:                 //Single Attack EmitterState
            {
                bulletHellRefState = BulletHell.SingleSineSlow;

                SetPattern(attackTypes[0]);

                SimpleSineSlow();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                break;
            }

            case BulletHell.SineNCosChaos:                 //Double Emitter State
            {
                bulletHellRefState = BulletHell.SineNCosChaos;
                SetPattern(attackTypes[1]);

                RadialSineAndCos();
                emitters[2].rotation = Quaternion.Euler(emitters[2].eulerAngles.x, emitters[2].eulerAngles.y, (emitters[2].eulerAngles.z  + emitterAngle));
                emitters[4].rotation = Quaternion.Euler(emitters[4].eulerAngles.x, emitters[4].eulerAngles.y, (emitters[4].eulerAngles.z  - emitterAngle));


                break;
            }


            case BulletHell.DEEW:
            {
                bulletHellRefState = BulletHell.DEEW;
                SetPattern(attackTypes[1]);

                DoubleEW();
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  + emitterAngle));
                emitters[3].rotation = Quaternion.Euler(emitters[3].eulerAngles.x, emitters[3].eulerAngles.y, (emitters[3].eulerAngles.z  - emitterAngle));   

                break;
            }

            case BulletHell.TEEWO:
            {
                bulletHellRefState = BulletHell.TEEWO;
                SetPattern(attackTypes[4]);

                TripleEWO();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  + emitterAngle));
                emitters[3].rotation = Quaternion.Euler(emitters[3].eulerAngles.x, emitters[3].eulerAngles.y, (emitters[3].eulerAngles.z  - emitterAngle));  

                break;
            }

            default:
                break;
        }
    }

    private void SimpleSine()
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

    private void SimpleSineSlow()
    {
        // new better way
        for (int i = 0; i < projectileAmount; i++)
        {
            //generating on instance
            Transform bullet = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation).transform;           //saves the Transform reference
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));                          //updates the angle between this and the next bullet
            bullet.transform.rotation = rot;                                                                                     //changes the emitter's current rotation
        }

        // reset the attack timer
        bulletHellTimer = 0f;
    }


    private void RadialSineAndCos()
    {
        //BH Attack that uses North and South Emitter - Sine and Cos Projectiles //TODO - Set Projectiles to be Sine and Cos
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[1], emitters[2].position, emitters[2].rotation).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[2], emitters[4].position, emitters[4].rotation).transform;
            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[4].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo; 
        }

        bulletHellTimer = 0f;
    }

    private void DoubleEW()
    {
        //BH Attack that uses East and West Emitter - Fast and Slow Projectiles //TODO - Set Projectiles to be Fast and Slow
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[1], emitters[1].position, emitters[1].rotation).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[2], emitters[3].position, emitters[3].rotation).transform;
            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[3].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo; 
        }

        bulletHellTimer = 0f;
    }

    private void TripleEWO()
    {   
        //BH Attack that uses East, West and Origin Emitters
        for(int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[5], emitters[3].position, emitters[3].rotation).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[6], emitters[1].position, emitters[1].rotation).transform;
            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotTwo;

            Transform bulletThree = Instantiate(projectilePrefab[1], emitters[0].position, emitters[0].rotation).transform;
            Quaternion rotThree = Quaternion.Euler(0, 0, emitters[3].eulerAngles.z + (projectileAngle * i));
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
