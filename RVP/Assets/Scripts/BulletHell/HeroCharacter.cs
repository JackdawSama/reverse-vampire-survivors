using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(StateHandler))]
[RequireComponent(typeof(DamageFlash))]

public class HeroCharacter : MonoBehaviour
{
    [Header("Hero Variables")]
    public float currentHealth;
    public float maxHealth = 100f;

    public float currentShields;
    public float maxShields = 100f;

    [Header("Checks")]
    public bool shieldsActive = true;

    [Header("Emitter Timers")]
    public float bulletHellTimer, bulletHellCooldown;
    public float aimTimer, aimCooldown;

    [Header("State Timers")]
    public float attackStateTimer = 0;
    public float attackStateCoolDown = 4f;

    [Header("Hero References")]
    public Transform target;

    [Header("States")]
    public AttackMode attack;
    public enum AttackMode
    {
        AimedMode,
        BulletHellMode,
        Both
    }
    public BulletHell bulletHell;
    BulletHell bulletHellRefState;
    public enum BulletHell
    {
        //SE - Single Emitter, DE - Double Emitter
        //N - North, S - South, E - East, W - West
        attackSwitch,
        SEO,
        SEN,
        SES,
        SEE,
        SEW,
        DENS,
        DEEW,
        attackThree,
        test
    }

    public AimedSystem aimedSystem;
    public enum AimedSystem
    {
        AimedSwitch,
        AimedSingle,
        AimedTriple
    }

    [Header("Hero Components")]
    public List<AttackTypes> attackTypes;
    public GameObject[] projectilePrefab;
    public GameObject attackChangePrefab;
    public GameObject damageTextPrefab;

    [Header("Emitters Data")]
    public Transform[] emitters;
    public Emitter[] Emitters;
    public float emitterAngle, projectileAngle, projectileAmount;

    [Header("State Machine Dbug Chcks")]
    public bool isBulletHell;
    public bool isAimed;

    private void Start() 
    {
        currentHealth = maxHealth;
        bulletHellTimer = 0f;

        attack = AttackMode.BulletHellMode;
        aimedSystem = AimedSystem.AimedTriple;
        bulletHell = BulletHell.DEEW;
    }

    private void Update() 
    {   
        //for debugging remember to remove this
        if(isAimed && isBulletHell)
        {
            attack = AttackMode.Both;
        }
        else if(isBulletHell)
        {
            attack = AttackMode.BulletHellMode;
        }
        else if(isAimed)
        {
            attack = AttackMode.AimedMode;
        }

        AttackStateHandler();
    }

    void AttackStateHandler()
    {
        switch (attack)
        {
            case AttackMode.AimedMode:
            {
                //Fires an aimed attack at the player at set intervals
                aimTimer += Time.deltaTime;
                if(aimTimer > aimCooldown)
                {
                    AimedAttackHandler();
                    aimTimer = 0;
                }

                //Have an if to check when to switch to BulletHell Mode/ Both
                break;
            }

            case AttackMode.BulletHellMode:
            {
                //Calls the AttackState and defines the Type of Attack that happens
                bulletHellTimer += Time.deltaTime;
                if(bulletHellTimer > bulletHellCooldown)
                {
                    BulletHellHandler();
                }
                break;
            }

            case AttackMode.Both:
            {
                //Calls both attack types

                aimTimer += Time.deltaTime;
                bulletHellTimer += Time.deltaTime;

                if(aimTimer > aimCooldown)
                {
                    AimedAttackHandler();
                    aimTimer = 0;
                }   

                if(bulletHellTimer > bulletHellCooldown)
                {
                    BulletHellHandler();
                }

                break;
            }

            default:
                break;
        }
    }

    void AimedAttackHandler()
    {
        switch (aimedSystem)
        {
            case AimedSystem.AimedSwitch:
            {
                break;
            }

            case AimedSystem.AimedSingle:
            {
                CentralAttackAimedSingle();
                break;
            }

            case AimedSystem.AimedTriple:
            {
                CentralAttackAimedTriple(1);
                break;
            }

            default:
                break;
        }
    }

    void BulletHellHandler()
    {
        switch (bulletHell)
        {
            case BulletHell.attackSwitch:              //Attack Switch state to allow for routine to end switch to another state
            {
                //Use to switch attacks once attacks have been defined
                break;
            }

            case BulletHell.SEO:                 //Single Attack EmitterState
            {
                SetPattern(attackTypes[0]);

                Attack();
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));

                break;
            }

            case BulletHell.DEEW:                 //Double Emitter State
            {
                SetPattern(attackTypes[1]);

                AttackTwo();
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  + emitterAngle));
                emitters[3].rotation = Quaternion.Euler(emitters[3].eulerAngles.x, emitters[3].eulerAngles.y, (emitters[3].eulerAngles.z  - emitterAngle));


                break;
            }


            case BulletHell.attackThree:
            {

                break;
            }
            
            case BulletHell.test :
            {

                //currentAttack = AttackState.attackThree;


                break;
            }

            default:
                break;
        }
    }

    private void CentralAttackAimedSingle()
    {
        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - emitters[0].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        Instantiate(projectilePrefab[0], emitters[0].position, rot);
    }

    private void CentralAttackAimedTriple()
    {
        emitters[0].rotation = Quaternion.Euler(0, 0, 0);
        
        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - emitters[0].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        emitters[0].rotation = rot;
        Instantiate(projectilePrefab[0], emitters[0].position, rot);
        Instantiate(projectilePrefab[0], new Vector3(emitters[0].position.x * ( Mathf.Cos(rot.z)) + 0.75f * ( Mathf.Cos(rot.z)), emitters[0].position.y * ( Mathf.Sin(rot.z)) + 0.75f * ( Mathf.Sin(rot.z)), 0f), rot);
        Instantiate(projectilePrefab[0], new Vector3(emitters[0].position.x * (-Mathf.Cos(rot.z)) + 0.75f * (-Mathf.Cos(rot.z)), emitters[0].position.y * (-Mathf.Sin(rot.z)) + 0.75f * (-Mathf.Sin(rot.z)), 0f), rot);
    }

    private void CentralAttackAimedTriple(int o)
    {
        //emitters[0].rotation = Quaternion.Euler(0, 0, 0);
        
        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - emitters[0].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        //emitters[0].rotation = rot;
        Instantiate(projectilePrefab[0], emitters[0].position, rot);
        Instantiate(projectilePrefab[0], emitters[0].position, rot);
        Instantiate(projectilePrefab[0], emitters[0].position, rot);
    }

    private void Attack()
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


    private void AttackTwo()
    {
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bulletOne = Instantiate(projectilePrefab[2], emitters[1].position, emitters[1].rotation).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;

            Transform bulletTwo = Instantiate(projectilePrefab[3], emitters[3].position, emitters[3].rotation).transform;
            Quaternion rotTwo = Quaternion.Euler(0, 0, emitters[3].eulerAngles.z + (projectileAngle * i));
            bulletTwo.transform.rotation = rotOne; 
        }

        bulletHellTimer = 0f;
    }

    private void AttackThree()
    {

    }

    //Sets the Attack data for the bullet hell
    private void SetPattern(AttackTypes attackData)
    {
        bulletHellCooldown = attackData.AttackCoolDown;
        emitterAngle = attackData.EmitterAngle;
        projectileAngle = attackData.ProjectileAngle;
        projectileAmount = attackData.Projectiles;
    }

    //Function called when emitter is deleted
    public void UpdateShields()
    {
        currentShields -= 25f;
    }

    //Function is called whenever a boosted unit comes in contact with the Hero Shield
    public void DamageShields(float damage)
    {
        if(shieldsActive)
        {
            currentShields -= damage;
        }
    }

    //Function is called whenever Projectile comes in contact with the Hero Character
    public void TakeDamage(float damage)
    {
        if(shieldsActive)
        {
            return;
        }
        currentHealth -= damage;
        
        //Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise(damage);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
