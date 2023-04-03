using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    public float currentHealth;
    public float maxHealth = 20f;
    public float moveSpeed;
    float moveHorizontal;
    public Color setColor;

    [Header("Enemy Abilities")]

    //Bools to check
    //If unit is alive
    //If unit is controlled by player
    //If unit can attack
    public bool isAlive;
    public bool isControlled;
    public bool canAttack;

    //How do these work ?
    //If not alive then the unit is removed from the spawner llist and deleted
    //If it was controlled and then it died, then player is granted new unit
    //it is removed from the spawner list and later deleted
    //Can attack is the case when the player is in control and can attack the hero using projectiles

    [Header("Enemy Components")]
    public GameObject bulletPrefab;

    [Header("Enemy References")]
    public TheSpawner spawner;
    public TheHero hero;
    public Transform bulletSpawn;
    public Camera cam;
    public TheEnemyController controller;
    public TheManager manager;

    Vector2 dir;
    float angle;

    private void Start() 
    {
        spawner = FindObjectOfType<TheSpawner>();
        hero = FindObjectOfType<TheHero>();
        controller = GetComponentInChildren<TheEnemyController>();
        manager = FindObjectOfType<TheManager>();

        currentHealth = maxHealth;

        isAlive = true;
        //isControlled = false;

        if(!isControlled)
        {
            cam.gameObject.SetActive(false);
            controller.gameObject.GetComponent<TheEnemyController>().enabled = false;
            canAttack = false;
        }
    }

    private void Update() 
    {
        if(!isAlive)
        {
            Die();
        }

        if(!isControlled && isAlive)
        {
            canAttack = false;
            cam.gameObject.SetActive(false);
            controller.gameObject.GetComponent<TheEnemyController>().enabled = false;
            
            MoveToPlayer();    
        }

        if(isControlled && isAlive)
        {
            canAttack = true;
            cam.gameObject.SetActive(true);
            controller.gameObject.GetComponent<TheEnemyController>().enabled = true;
            controller.gameObject.GetComponent<SpriteRenderer>().color = setColor;

            MoveinControl();
        }

        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<TheEnemyBullet>().SetReference(gameObject.GetComponent<TheEnemy>());
    }

    private void MoveToPlayer()
    {
        dir = hero.transform.position - controller.gameObject.transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        controller.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // controller.gameObject.transform.up = hero.transform.position - controller.gameObject.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, moveSpeed * Time.deltaTime);
    }

    private void MoveinControl()
    {
        moveHorizontal = Input.GetAxis ("Horizontal");
        transform.Translate(new Vector2(moveHorizontal * moveSpeed * Time.deltaTime, 0));

        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, moveSpeed * Time.deltaTime);
    }

    private void Die()
    {
        //If the player is controlling the Unit then player is assigned a new unit and this unit is destroyed
        //In case it is not player controlled the unit is destroyed normally

        manager.units++;

        if(isControlled)
        {
            if(spawner.enemies != null)
            {
                NextUnit();
            }
            Destroy(gameObject);
            return;
        }
        spawner.enemies.Remove(this.gameObject);
        Destroy(gameObject);
        
    }

    private void NextUnit()
    {
        int arrayCursor = spawner.enemies.IndexOf(gameObject);

        int newUnit = Random.Range(0, spawner.enemies.Count);

        if(newUnit == arrayCursor)
        {
            NextUnit();
        }
        else
        {
            spawner.enemies[newUnit].gameObject.GetComponent<TheEnemy>().isControlled = true;
            spawner.enemies.Remove(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Unit Died");
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Hero")
        {
            Die();
        }    
    }

    // public void MovingTowards()
    // {
    //     transform.position = Vector2.Lerp(transform.position, hero.transform.position);
    // }
}
