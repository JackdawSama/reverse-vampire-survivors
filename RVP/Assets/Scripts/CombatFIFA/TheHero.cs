using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHero : MonoBehaviour
{
    [Header("Hero Variables")]
    public float currentHealth;
    public float maxHealth = 100f;
    public float attackTimer;
    public float attackCooldown;
    public float moveSpeed;

    [Header("Checks")]
    public bool isRoaming;

    public int count = 0;

    [Header("State Timers")]
    public float idleTimer;
    public float idleCooldown;
    public float minIdleTime = 0;
    public float maxIdleTime = 3;

    [Header("Range Variables")]
    public float attackRange;
    public float minRoamSearch;
    public float maxRoamSearch;
    public float attackAngle, attackAngleChange, projectileAmount; 

    [Header("Reference Lists")]
    public Vector2 roamPoint;
    public List<GameObject> targetEnemies;
    public List<Transform> pointOfInterests;

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
        attackOne,
        attackTwo
    }

    [Header("Hero Components")]
    public GameObject projectilePrefab;

    [Header("Hero References")]
    public Transform bulletSpawn;
    public TheManager manager;
    public GameObject damageTextPrefab;

    

    private void Start() 
    {
        currentHealth = maxHealth;
        attackTimer = 0f;

        idleCooldown = Random.Range(minIdleTime, maxIdleTime);
        currentState = HeroState.idle;
    }

    private void Update() 
    {   

        StateHandler();

        attackTimer += Time.deltaTime;

        if(attackTimer > attackCooldown)
        {
            Debug.Log("Attack");
            AttackHandler();
        }
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

    void AttackHandler()
    {
        switch (currentAttack)
        {
            case AttackState.attackOne:
            {
                AttackOne();
                bulletSpawn.rotation = Quaternion.Euler(bulletSpawn.eulerAngles.x, bulletSpawn.eulerAngles.y, (bulletSpawn.eulerAngles.z  + attackAngle ));
                count++;
                break;
            }
            
            default:
                break;
        }
    }

    private void AttackOne()
    {

        // new better way
        float angleChange = attackAngleChange;
        for (int i = 0; i < projectileAmount; i++)
        {
            Transform bullet = Instantiate(projectilePrefab, bulletSpawn.position, bulletSpawn.rotation).transform;
            Quaternion rot = Quaternion.Euler(0, 0, bulletSpawn.eulerAngles.z + (angleChange * i));
            bullet.transform.rotation = rot; 
        }

        // reset the attack timer
        attackTimer = 0f;
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
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise(damage);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Hero Died");
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
