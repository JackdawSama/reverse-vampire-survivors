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

    [Header("Range Variables")]
    public float attackRange;
    public float roamSearchRadius;



    [Header("Reference Lists")]
    public Transform roamPoint;
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

        //initialPos = transform.position;
    }

    private void Update() 
    {   
        attackTimer += Time.deltaTime;

        if(attackTimer > attackCooldown)
        {
            Debug.Log("Attack");
            Attack();

            // if(!isRunning)
            // {
            //     isRunning = true;
            //     StartCoroutine(AttackEnemy());
            // }

            // attackTimer = 0f;
        }

        transform.position += new Vector3(0, moveSpeed * Time.deltaTime,0);
        //manager.yards = Mathf.Abs(initialPos.y) + Mathf.Abs(transform.position.y);
    }

    private void StateHandler()
    {
        switch(currentState)
        {
            case HeroState.idle:
                //In Idle state stay at one point and deal some damage

                break;
            case HeroState.roam:
                //In Roam state find a random point and move towards it

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

    void IdleRoamPoint()
    {

    }

    void CheckForInterests()
    {

    }

    void FindPointWithinRadius()
    {
        
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
