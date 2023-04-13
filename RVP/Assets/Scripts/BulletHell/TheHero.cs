using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHero : MonoBehaviour
{
    [Header("Hero Variables")]
    public float currentHealth;
    public float maxHealth = 100f;
    public float moveSpeed;

    [Header("Checks")]
    public bool isRoaming;
    public bool doubleEmitter;

    [Header("Emitter Timers")]
    public float attackTimer;
    public float attackCooldown;
    public float aimTimer;
    public float aimTimerCooldown;
    public float timerOne;
    public float timerOneCooldown;
    public float timerTwo;
    public float timerTwoCooldown;

    [Header("State Timers")]
    public float idleTimer;
    public float idleCooldown;
    public float minIdleTime = 0;
    public float maxIdleTime = 3;
    public float attackStateTimer = 0;
    public float attackStateCoolDown = 4f;

    [Header("Range Variables")]
    public float attackRange;
    public float minRoamSearch;
    public float maxRoamSearch;
    public float emitterAngle, projectileAngle, projectileAmount; 

    [Header("Reference Lists")]
    public Transform target;
    public Vector2 roamPoint;
    public List<GameObject> targetEnemies;
    public List<Transform> pointOfInterests;
    public List<AttackTypes> attackTypes;

    [Header("States")]
    public HeroState currentState;
    public enum HeroState
    {
        idle,
        roam,
        interested
    }

    public AttackState currentAttack;

    public enum AttackState
    {
        attackSwitch,
        attackOne,
        attackTwo
    }

    [Header("Hero Components")]
    public GameObject[] projectilePrefab;

    [Header("Hero Emitters")]
    public Transform emitterZero;
    public Transform emitterOne;
    public Transform emitterTwo;

    [Header("Hero References")]
    public GameObject damageTextPrefab;
    public GameObject attackChangePrefab;

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
            AimedAttack();
            aimTimer = 0;
        }

        if(doubleEmitter)
        {
            timerOneCooldown = attackTypes[0].AttackCoolDown;
            timerOne += Time.deltaTime;
            if(timerOne > timerOneCooldown)
            {
                // SetPattern(attackTypes[0]);
                emitterOneAttack(1);
                emitterOne.rotation = Quaternion.Euler(emitterOne.eulerAngles.x, emitterOne.eulerAngles.y, (emitterOne.eulerAngles.z  + emitterAngle));
            }

            timerTwoCooldown = attackTypes[1].AttackCoolDown;
            timerTwo += Time.deltaTime;
            if(timerTwo > timerTwoCooldown)
            {
                // SetPattern(attackTypes[1]);
                emitterTwoAttack(2);
                emitterTwo.rotation = Quaternion.Euler(emitterTwo.eulerAngles.x, emitterTwo.eulerAngles.y, (emitterTwo.eulerAngles.z  - emitterAngle));
            }
        }

        if(!doubleEmitter)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer > attackCooldown)
            {   
                Attack(0);
                emitterZero.rotation = Quaternion.Euler(emitterZero.eulerAngles.x, emitterZero.eulerAngles.y, (emitterZero.eulerAngles.z  + emitterAngle));
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


    [SerializeField] bool offset; // are we offsetting the shots?

    AttackState refState;   //attack reference to check what the last attack state was
    void AttackHandler()
    {
        switch (currentAttack)
        {
            case AttackState.attackSwitch:
            {
                if(flashRoutine == null)
                {
                    if(refState != AttackState.attackOne)
                    {
                        currentAttack = AttackState.attackOne;
                    }
                    else if(refState != AttackState.attackTwo)
                    {
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
            
            default:
                break;
        }
    }

    private void Attack(int projectileType)
    {
        // new better way

        SetPattern(attackTypes[0]);
        float angleChange = projectileAngle;

        Debug.Log("Attack Type 0");
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab[projectileType], emitterZero.position, emitterZero.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emitterZero.eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
        }
        // reset the attack timer
        attackTimer = 0f;
    }


    private void emitterOneAttack(int projectileType)
    {
        SetPattern(attackTypes[0]);
        float angleChange = projectileAngle;

        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab[projectileType], emitterOne.position, emitterOne.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emitterOne.eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
        }

        timerOne = 0;
    }

    private void emitterTwoAttack(int projectileType)
    {
        SetPattern(attackTypes[1]);

        float angleChange = projectileAngle;
        
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab[projectileType], emitterTwo.position, emitterTwo.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emitterTwo.eulerAngles.z + (angleChange * -i));
            bullet.transform.rotation = rot; 
        }
        timerTwo = 0;
    }

    private void AimedAttack()
    {
        //SetPattern(attackTypes[2]);

        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        Instantiate(projectilePrefab[3], transform.position, rot);
        
        Debug.Log("Aimed");
    }


    private void SetPattern(AttackTypes attackData)
    {
        attackCooldown = attackData.AttackCoolDown;
        emitterAngle = attackData.EmitterAngle;
        projectileAngle = attackData.ProjectileAngle;
        projectileAmount = attackData.Projectiles;
    }

    private void CheckforEnemies()
    {
        targetEnemies.Clear();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                targetEnemies.Add(collider.gameObject);
            }
        }
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
    }
}
