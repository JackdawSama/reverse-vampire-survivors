using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemy : TheSubject
{
    [Header("Enemy Variables")]
    public float currentHealth;
    public float maxHealth = 20f;
    public float attackTimer;
    public float attackCooldown;

    [Header("Enemy Abilities")]
    public bool isControlled;
    public bool canAttack;


    [Header("Enemy Components")]
    public GameObject bulletPrefab;

    [Header("Enemy References")]
    public TheSpawner spawner;
    public TheHero hero;
    public Transform bulletSpawn;

    private void Start() 
    {
        spawner = FindObjectOfType<TheSpawner>();
        hero = FindObjectOfType<TheHero>();
        currentHealth = maxHealth;
        attackTimer = 0f;
    }

    private void Update() 
    {
        if(canAttack)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
        
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void Die()
    {

    }
}
