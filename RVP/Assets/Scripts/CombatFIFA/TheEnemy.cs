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

    [Header("Enemy Abilities")]
    public bool isControlled;
    public bool canAttack;
    public bool test;


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
            controller.gameObject.GetComponent<TheEnemyController>().enabled = false;
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isControlled = !isControlled;
        }

        if(!isControlled)
        {
            canAttack = false;
            controller.gameObject.GetComponent<TheEnemyController>().enabled = false;
            MoveToPlayer();
        }

        if(isControlled)
        {
            canAttack = true;
            controller.gameObject.GetComponent<TheEnemyController>().enabled = true;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Die();
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
        controller.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, moveSpeed * Time.deltaTime);
    }

    private void Die()
    {
        if(test)
        {
            int position = spawner.enemies.IndexOf(gameObject);
            Debug.Log(position);

            // int newEnemy = Random.Range(0,spawner.enemies.Count);
            // if(newEnemy != position)
            // {
            //     test = false;
            //     spawner.enemies[newEnemy].gameObject.GetComponent<TheEnemy>().test = true;
            // }
            // else
            // {
            //     newEnemy = Random.Range(0, spawner.enemies.Count);
            //     test = false;
            //     spawner.enemies[newEnemy].gameObject.GetComponent<TheEnemy>().test = true;
            // }

            test = false;
            spawner.enemies[position+1].gameObject.GetComponent<TheEnemy>().test = true;
            spawner.enemies.Remove(this.gameObject);
        }
    }
}
