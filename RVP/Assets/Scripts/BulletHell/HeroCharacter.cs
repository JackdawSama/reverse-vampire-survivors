using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacter : MonoBehaviour
{
    [Header("Hero Variables")]
    public float currentHealth;
    public float maxHealth = 100f;
    private float  attackTimer;
    public float attackCooldown;
    public float moveSpeed;

    [Header("Checks")]
    public bool isRoaming;
    public bool doubleEmitter;
    public bool doubleSpawn;

    [Header("Emitter Timers")]
    public float timerOne;
    public float timerOneCooldown;
    public float timerTwo;
    public float timerTwoCooldown;

    [Header("State Timers")]
    public float attackStateTimer = 0;
    public float attackStateCoolDown = 4f;

    [Header("Range Variables")]
    public float attackRange;
    public float minRoamSearch;
    public float maxRoamSearch;
    public float emitterAngle, projectileAngle, projectileAmount; 

    [Header("Reference Lists")]
    public Vector2 roamPoint;
    public Transform target;
    public List<Transform> pointOfInterests;
    public List<AttackTypes> attackTypes;

    public Transform emmiterOne;
    public Transform emmiterTwo;

    [Header("States")]
    public AttackState currentAttack;

    public enum AttackState
    {
        attackSwitch,
        attackOne,
        attackTwo,
        attackThree
    }

    [Header("Hero Components")]
    public GameObject projectilePrefab;

    [Header("Hero References")]
    public Transform bulletSpawn;
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

        rend = GetComponent<SpriteRenderer>();
        originalMat = rend.material;

        currentAttack = AttackState.attackOne;
    }

    private void Update() 
    {   
        AttackHandler();

        if(doubleEmitter)
        {
            timerOneCooldown = attackTypes[0].AttackCoolDown;
            timerOne += Time.deltaTime;
            if(timerOne > timerOneCooldown)
            {
                // SetPattern(attackTypes[0]);
                EmitterOneAttack();
                emmiterOne.rotation = Quaternion.Euler(emmiterOne.eulerAngles.x, emmiterOne.eulerAngles.y, (emmiterOne.eulerAngles.z  + emitterAngle));
            }

            timerTwoCooldown = attackTypes[1].AttackCoolDown;
            timerTwo += Time.deltaTime;
            if(timerTwo > timerTwoCooldown)
            {
                // SetPattern(attackTypes[1]);
                EmitterTwoAttack();
                emmiterTwo.rotation = Quaternion.Euler(emmiterTwo.eulerAngles.x, emmiterTwo.eulerAngles.y, (emmiterTwo.eulerAngles.z  - emitterAngle));
            }
        }

        if(!doubleEmitter && doubleSpawn)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer > attackCooldown)
            {   
                DoubleAttack();
                bulletSpawn.rotation = Quaternion.Euler(bulletSpawn.eulerAngles.x, bulletSpawn.eulerAngles.y, (bulletSpawn.eulerAngles.z  + emitterAngle));
            }
        }

        if(!doubleEmitter && !doubleSpawn)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer > attackCooldown)
            {   
                Attack();
                bulletSpawn.rotation = Quaternion.Euler(bulletSpawn.eulerAngles.x, bulletSpawn.eulerAngles.y, (bulletSpawn.eulerAngles.z  + emitterAngle));
            }
        }

        attackStateTimer += Time.deltaTime;

        if(Input.GetMouseButtonDown(1))
        {
            AimedAttack();
        }
    }

    AttackState refState;   //attack reference to check what the last attack state was
    void AttackHandler()
    {
        switch (currentAttack)
        {
            case AttackState.attackSwitch:
            {
                if(flashRoutine == null)
                {
                    if(refState == AttackState.attackOne)
                    {
                        currentAttack = AttackState.attackTwo;
                    }
                    else if(refState == AttackState.attackTwo)
                    {
                        currentAttack = AttackState.attackThree;
                    }
                    else if(refState == AttackState.attackThree)
                    {
                        currentAttack = AttackState.attackOne;
                    }
                }
                break;
            }

            case AttackState.attackOne:                 //Single Attack EmitterState
            {
                doubleEmitter = false;
                doubleSpawn = false;

                if(attackStateTimer > attackStateCoolDown)
                {
                    attackStateTimer = 0;
                    
                    Instantiate(attackChangePrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise("!"); //State change indicators
                    refState = currentAttack;
                    //currentAttack = AttackState.attackSwitch;

                    //Flash();
                }

                break;
            }

            case AttackState.attackTwo:
            {
                doubleEmitter = true;
                doubleSpawn = false;

                if(attackStateTimer > attackStateCoolDown)
                {
                    attackStateTimer = 0;
                    
                    Instantiate(attackChangePrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise("!"); //State change indicators
                    refState = currentAttack;
                    //currentAttack = AttackState.attackSwitch;

                    //Flash();
                }

                break;
            }

            case AttackState.attackThree:
            {
                doubleEmitter = false;
                doubleSpawn = true;

                if(attackStateTimer > attackStateCoolDown)
                {
                    attackStateTimer = 0;
                    
                    Instantiate(attackChangePrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise("!"); //State change indicators
                    refState = currentAttack;
                    //currentAttack = AttackState.attackSwitch;

                    //Flash();
                }

                break;
            }
            
            default:
                break;
        }
    }
    private void SetPattern(AttackTypes attackData)
    {
        attackCooldown = attackData.AttackCoolDown;
        emitterAngle = attackData.EmitterAngle;
        projectileAngle = attackData.ProjectileAngle;
        projectileAmount = attackData.Projectiles;
    }

    private void Attack()
    {
        // new better way
        SetPattern(attackTypes[0]);
        float angleChange = projectileAngle;

        Debug.Log("Attack Type 0");
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab, bulletSpawn.position, bulletSpawn.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, bulletSpawn.eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
        }
        // reset the attack timer
        attackTimer = 0f;
    }

    private void DoubleAttack()
    {
        // new better way
        SetPattern(attackTypes[3]);
        float angleChange = projectileAngle;

        Debug.Log("Attack Type 0");
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab, bulletSpawn.position, bulletSpawn.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, bulletSpawn.eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
            Instantiate(projectilePrefab, bulletSpawn.position, Quaternion.Euler(0, 0, bulletSpawn.eulerAngles.z + 15));
        }
        // reset the attack timer
        attackTimer = 0f;
    }


    private void EmitterOneAttack()
    {
        SetPattern(attackTypes[0]);
        float angleChange = projectileAngle;

        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab, emmiterOne.position, emmiterOne.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emmiterOne.eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
        }

        timerOne = 0;
    }

    private void EmitterTwoAttack()
    {
        SetPattern(attackTypes[1]);

        float angleChange = projectileAngle;
        
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab, emmiterTwo.position, emmiterTwo.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, emmiterTwo.eulerAngles.z + (angleChange * -i));
            bullet.transform.rotation = rot; 
        }
        timerTwo = 0;
    }

    private void AimedAttack()
    {
        SetPattern(attackTypes[2]);

        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        for(int i = 0; i < projectileAmount; i++)
        {
            Instantiate(projectilePrefab, transform.position, rot);
        }
        
        Debug.Log("Aimed");
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
        yield return new WaitForSeconds(0.2f);

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

}
