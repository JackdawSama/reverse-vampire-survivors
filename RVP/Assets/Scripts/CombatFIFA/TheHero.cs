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
    public List<GameObject> enemies;

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
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Attack();
        }

        transform.position += new Vector3(0, moveSpeed * Time.deltaTime,0);
    }

    private void Attack()
    {
        CheckforEnemies();

        for(int i = 0; i < enemies.Count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            
            bullet.TryGetComponent<TheHeroBullet>(out TheHeroBullet component);
            component.SetTarget(enemies[i].transform);
        }

        attackTimer = 0f;
    }

    private void CheckforEnemies()
    {
        int counter = 0;

        enemies.Clear();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                enemies.Add(collider.gameObject);
                counter++;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
