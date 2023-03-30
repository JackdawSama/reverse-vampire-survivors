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
    Collider2D[] enemies;

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
        if(Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
    }

    private void Attack()
    {
        CheckforEnemies();

        for(int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Debug.Log("Attack");
            //bullet.GetComponent<TheHeroBullet>().SetTarget(enemies[i].transform);
        }

        attackTimer = 0f;
    }

    private void CheckforEnemies()
    {
        enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, 3);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
