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
    public TheEnemyController controller;

    Vector2 dir;
    float angle;

    private void Start() 
    {
        spawner = FindObjectOfType<TheSpawner>();
        hero = FindObjectOfType<TheHero>();
        controller = GetComponentInChildren<TheEnemyController>();
        currentHealth = maxHealth;
        attackTimer = 0f;

        if(!isControlled)
        {
            cam.gameObject.SetActive(false);
            controller.gameObject.SetActive(false);
            canAttack = false;
        }
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
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void MoveToPlayer()
    {
        dir = hero.transform.position - controller.gameObject.transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        //controller.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        controller.gameObject.transform.rotation.SetLookRotation((Vector3)dir);

        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, moveSpeed * Time.deltaTime);
    }

    private void Die()
    {

    }
}
