using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class TheHero : MonoBehaviour
{
    [Header("Hero Variables")]
    public float currentHealth;
    public float maxHealth = 100f;
    public float currentShields;
    public float maxShields = 100f;
    public float moveSpeed;

    [Header("Checks")]
    public bool shieldsActive;
    public bool isRoaming;
    public bool doubleEmitter;

    [Header("Emitter Timers")]
    public float bulletHellTimer, bulletHellCooldown;
    public float aimTimer, aimCooldown;

    [Header("State Timers")]
    public float idleTimer, idleCooldown;
    public float minIdleTime = 0, maxIdleTime = 3;
    public float attackStateTimer = 0, attackStateCoolDown = 4f;

    [Header("Search Range Variables")]
    public float minRoamSearch, maxRoamSearch;

    [Header("Hero References")]
    public Transform target;
    public Vector2 roamPoint;

    [Header("States")]
    public HeroState currentState;
    public enum HeroState
    {
        idle,
        roam,
        interested
    }

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
        currentShields = maxShields;
        bulletHellTimer = 0f;

        idleCooldown = Random.Range(minIdleTime, maxIdleTime);

        currentState = HeroState.idle;
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
        idleTimer += Time.deltaTime;
        StateHandler();
    }

    private void StateHandler()
    {
        switch(currentState)
        {
            case HeroState.idle:
                //In Idle state stay at one point and deal some damage
                isRoaming = false;
                idleTimer += Time.deltaTime;

                if(idleTimer >= idleCooldown)
                {
                    currentState = HeroState.roam;
                    roamPoint = FindPointWithinRadius(minRoamSearch, maxRoamSearch);
                    isRoaming = true;
                    idleTimer = 0;
                }
                break;

            case HeroState.roam:
                //In Roam state find a random point and move towards it

                MoveToPoint(roamPoint);
                if(transform.position == (Vector3)roamPoint)
                {
                    idleCooldown = Random.Range(minIdleTime, maxIdleTime);
                    currentState = HeroState.idle;
                }
                break;

            case HeroState.interested:
                //Start moving towards this point and when reached switch to either idle or roam
                break;
            default:
                break;

        }
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

    private void SetPattern(AttackTypes attackData)
    {
        bulletHellCooldown = attackData.AttackCoolDown;
        emitterAngle = attackData.EmitterAngle;
        projectileAngle = attackData.ProjectileAngle;
        projectileAmount = attackData.Projectiles;
    }

    private void MoveToPoint(Vector2 target)
    {
        //Change the turn toward into facing left or facing right

        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    Vector2 FindPointWithinRadius(float minRad, float maxRad)
    {
        Vector2 newPos;

        float angle = Random.Range(0f, 360f);

        newPos.x = transform.position.x + (Random.Range(minRad, maxRad) * Mathf.Cos(angle / (180f / Mathf.PI)));
        newPos.y = transform.position.y + (Random.Range(minRad, maxRad) * Mathf.Sin(angle / (180f / Mathf.PI)));

        return newPos;
    }

    //Function called when emitter is deleted
    public void UpdateShields()
    {
        currentShields -= 25f;

        if(currentShields <= 0)
        {
            shieldsActive = false;
            currentShields = 0;
        }
    }

    //Function is called whenever a boosted unit comes in contact with the Hero Shield
    public void DamageShields(float damage)
    {
        if(shieldsActive)
        {
            currentShields -= damage;
        }

        if(currentShields <= 0)
        {
            shieldsActive = false;
            currentShields = 0;
        }
    }
    public void TakeDamage(float damage)
    {
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRoamSearch);
        Gizmos.DrawWireSphere(transform.position, maxRoamSearch);
    }
}
