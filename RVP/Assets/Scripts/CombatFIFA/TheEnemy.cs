using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    public float currentHealth;
    public float maxHealth = 20f;
    public float attackTimer;
    public float attackCooldown;
    public float moveSpeed;
    public Vector2 mousePos;

    [Header("Enemy Abilities")]
    public bool isControlled;
    public bool canAttack;


    [Header("Enemy Components")]
    public GameObject bulletPrefab;

    [Header("Enemy References")]
    public TheSpawner spawner;
    public TheHero hero;
    public Transform bulletSpawn;
    public Camera cam;
    public Rigidbody2D rb;

    private void Start() 
    {
        spawner = FindObjectOfType<TheSpawner>();
        hero = FindObjectOfType<TheHero>();
        currentHealth = maxHealth;
        attackTimer = 0f;

        rb = GetComponent<Rigidbody2D>();
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

        if(!isControlled)
        {
            MoveToPlayer();
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void Attack()
    {
        //Debug.Log("Attacking");
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, moveSpeed * Time.deltaTime);
    }

    private void Die()
    {

    }
}
