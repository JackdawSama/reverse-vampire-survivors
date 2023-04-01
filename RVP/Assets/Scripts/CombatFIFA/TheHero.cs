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
    public float attackRange;
    public float moveSpeed;
    public List<GameObject> targetEnemies;

    public bool isRunning = false;

    [Header("Hero Components")]
    public GameObject bulletPrefab;

    [Header("Hero References")]
    public Transform bulletSpawn;

    private void Start() 
    {
        currentHealth = maxHealth;
        attackTimer = 0f;
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
    }

    private void Attack()
    {
        CheckforEnemies();
        Debug.Log(targetEnemies.Count);

        for(int i = 0; i < targetEnemies.Count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            
            bullet.TryGetComponent<TheHeroBullet>(out TheHeroBullet component);
            component.SetTarget(targetEnemies[i].transform);
        }

        attackTimer = 0f;
    }

    private void CheckforEnemies()
    {
        //int counter = 0;

        targetEnemies.Clear();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                targetEnemies.Add(collider.gameObject);
                //counter++;
            }
        }
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

    IEnumerator AttackEnemy()
    {
        CheckforEnemies();

        int count = 0;

        while(count < 3)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.TryGetComponent<TheHeroBullet>(out TheHeroBullet component);
            component.SetTarget(targetEnemies[count].transform);

            yield return new WaitForSeconds(0.2f);

            count++;
        }

        isRunning = false;
        attackTimer = 0f;
        Debug.Log("RESET");

        yield return null;
    }
}
