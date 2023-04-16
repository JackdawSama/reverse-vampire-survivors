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
    public float attackTimer, attackCooldown;
    public float aimTimer, aimTimerCooldown;
    public float timerOne, timerOneCooldown;
    public float timerTwo, timerTwoCooldown;

    [Header("State Timers")]
    public float idleTimer, idleCooldown;
    public float minIdleTime = 0, maxIdleTime = 3;
    public float attackStateTimer = 0, attackStateCoolDown = 4f;

    [Header("Range Variables")]
    public float attackRange;
    public float minRoamSearch, maxRoamSearch;
    public float emitterAngle, projectileAngle, projectileAmount; 

    [Header("Hero References")]
    public Transform target;
    public Vector2 roamPoint;

    [Header("States")]
    public HeroState currentState;
    public AttackState currentAttack;
    public enum HeroState
    {
        idle,
        roam,
        interested
    }

    public enum AttackState
    {
        attackSwitch,
        attackOne,
        attackTwo,
        attackThree
    }

    [Header("Hero Components")]
    public List<AttackTypes> attackTypes;
    public GameObject[] projectilePrefab;
    int projValue;
    public GameObject attackChangePrefab;
    public GameObject damageTextPrefab;

    [Header("Emitters Data")]
    public Transform[] emitters;


    [Header("Flash")]
    [SerializeField] Material flashMat;
    [SerializeField] float flashDuration;
    [SerializeField] SpriteRenderer rend;
    Material originalMat;
    Coroutine flashRoutine;

    private void Start() 
    {
        currentHealth = maxHealth;
        attackTimer = 0f;

        idleCooldown = Random.Range(minIdleTime, maxIdleTime);
        currentState = HeroState.idle;

        rend = GetComponent<SpriteRenderer>();
        originalMat = rend.material;
    }

    private void Update() 
    {   
        StateHandler();

        AttackHandler();

        aimTimer += Time.deltaTime;

        if(aimTimer > aimTimerCooldown)
        {
            projValue = 1;
            AimedAttack(projValue);
            aimTimer = 0;
        }

        if(doubleEmitter)
        {
            timerOneCooldown = attackTypes[0].AttackCoolDown;
            timerOne += Time.deltaTime;
            if(timerOne > timerOneCooldown)
            {
                projValue = 3;
                EmitterOneAttack(projValue, 1);
                emitters[1].rotation = Quaternion.Euler(emitters[1].eulerAngles.x, emitters[1].eulerAngles.y, (emitters[1].eulerAngles.z  + emitterAngle));
            }

            timerTwoCooldown = attackTypes[1].AttackCoolDown;
            timerTwo += Time.deltaTime;
            if(timerTwo > timerTwoCooldown)
            {
                projValue = 2;
                EmitterTwoAttack(projValue, 4);
                emitters[2].rotation = Quaternion.Euler(emitters[2].eulerAngles.x, emitters[2].eulerAngles.y, (emitters[2].eulerAngles.z  - emitterAngle));
            }
        }

        if(!doubleEmitter)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer > attackCooldown)
            {   
                projValue = 0;
                Attack(projValue, 0);
                emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitters[0].eulerAngles.z  + emitterAngle));
            }
        }

        attackStateTimer += Time.deltaTime;
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

    AttackState refState;   //attack reference to check what the last attack state was
    void AttackHandler()
    {
        switch (currentAttack)
        {
            case AttackState.attackSwitch:              //Attack Switch state to allow for routine to end switch to another state
            {
                if(flashRoutine == null)
                {
                    if(refState != AttackState.attackOne)
                    {
                        emitters[0].rotation = Quaternion.Euler(emitters[0].rotation.x, emitters[0].rotation.y, 0f);

                        //Set Projectile Prefab
                        //Set Attack Type

                        currentAttack = AttackState.attackOne;
                    }
                    else if(refState != AttackState.attackTwo)
                    {
                        emitters[1].rotation = Quaternion.Euler(emitters[1].rotation.x, emitters[1].rotation.y, 0f);
                        emitters[2].rotation = Quaternion.Euler(emitters[2].rotation.x, emitters[2].rotation.y, 0f);

                        //Set Projectile Prefab
                        //Set Attack Type

                        currentAttack = AttackState.attackTwo;
                    }
                }
                break;
            }

            case AttackState.attackOne:                 //Single Attack EmitterState
            {
                doubleEmitter = false;

                if(attackStateTimer > attackStateCoolDown)
                {
                    attackStateTimer = 0;
                    
                    Instantiate(attackChangePrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise("!"); //State change indicators
                    refState = AttackState.attackOne;
                    currentAttack = AttackState.attackSwitch;

                    //Flash();
                }

                break;
            }

            case AttackState.attackTwo:                 //Double Emitter State
            {
                doubleEmitter = true;

                if(attackStateTimer > attackStateCoolDown)
                {
                    attackStateTimer = 0;
                    
                    Instantiate(attackChangePrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise("!"); //State change indicators
                    refState = AttackState.attackTwo;
                    currentAttack = AttackState.attackSwitch;

                    //Flash();
                }

                break;
            }

            case AttackState.attackThree:               //Sinewave Projectile Single attack
            {
                doubleEmitter = false;
                break;
            }
            
            default:
                break;
        }
    }

    private void Attack(int projectileType, int attack)
    {
        // new better way

        SetPattern(attackTypes[attack]);
        float angleChange = projectileAngle;

        // Debug.Log("Attack Type 0");
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab[projectileType], emitters[0].position, emitters[0].rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
        }
        // reset the attack timer
        attackTimer = 0f;
    }


    private void EmitterOneAttack(int projectileType, int attack)
    {
        SetPattern(attackTypes[attack]);
        float angleChange = projectileAngle;

        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab[projectileType], emitters[1].position, emitters[1].rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emitters[1].eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
        }

        timerOne = 0;
    }

    private void EmitterTwoAttack(int projectileType, int attack)
    {
        SetPattern(attackTypes[attack]);

        float angleChange = projectileAngle;
        
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab[projectileType], emitters[2].position, emitters[2].rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emitters[2].eulerAngles.z + ((angleChange/2) * -i));
            bullet.transform.rotation = rot; 
        }
        timerTwo = 0;
    }

    private void AimedAttack(int projectileType)
    {
        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        Instantiate(projectilePrefab[projectileType], transform.position, rot);
    }


    private void SetPattern(AttackTypes attackData)
    {
        attackCooldown = attackData.AttackCoolDown;
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

    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        rend.material = flashMat;

        yield return new WaitForSeconds(flashDuration);

        rend.material = originalMat;

        yield return new WaitForSeconds(flashDuration);

        rend.material = flashMat;

        yield return new WaitForSeconds(flashDuration);

        rend.material = originalMat;

        yield return new WaitForSeconds(flashDuration);

        rend.material = flashMat;

        yield return new WaitForSeconds(flashDuration);

        rend.material = originalMat;

        flashRoutine = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRoamSearch);
        Gizmos.DrawWireSphere(transform.position, maxRoamSearch);
    }
}
