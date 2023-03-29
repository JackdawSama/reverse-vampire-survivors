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

    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }
}
