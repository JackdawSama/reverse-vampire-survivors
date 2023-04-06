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

    [Header("State Timers")]
    public float idleTimer;
    public float idleCooldown;
    public float minIdleTime = 0;
    public float maxIdleTime = 3;

    [Header("Range Variables")]
    public float attackRange;
    public float minRoamSearch;
    public float maxRoamSearch;

    [Header("Reference Lists")]
    public Vector2 roamPoint;
    public List<GameObject> targetEnemies;
    public List<Transform> pointOfInterests;

    //public Vector2 initialPos;
    public enum HeroState
    {
        idle,
        roam,
        interested
    }

    public HeroState currentState;

    [Header("Hero Components")]
    public GameObject projectilePrefab;

    [Header("Hero References")]
    public Transform bulletSpawn;
    public TheManager manager;

    

    private void Start() 
    {
        currentHealth = maxHealth;
        attackTimer = 0f;

        idleCooldown = Random.Range(minIdleTime, maxIdleTime);
        currentState = HeroState.idle;

        //initialPos = transform.position;
    }

    private void Update() 
    {   

        StateHandler();

        attackTimer += Time.deltaTime;

        if(attackTimer > attackCooldown)
        {
            Debug.Log("Attack");
            Attack();
        }

        //transform.position += new Vector3(0, moveSpeed * Time.deltaTime,0);
        //manager.yards = Mathf.Abs(initialPos.y) + Mathf.Abs(transform.position.y);
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

    private void Attack()
    {
        CheckforEnemies();
        Debug.Log(targetEnemies.Count);

        for(int i = 0; i < targetEnemies.Count; i++)
        {
            GameObject bullet = Instantiate(projectilePrefab, bulletSpawn.position, bulletSpawn.rotation);
            
            bullet.TryGetComponent<TheHeroBullet>(out TheHeroBullet component);
            component.SetTarget(targetEnemies[i].transform);
        }

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
