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

    [Header("Enemy Abilities")]

    [SerializeField] string[] enemyAbilities;
    public bool isAlive;
    public bool doubleDamage = false;
    public Color doubleDamageColour;
    public bool bulletSplit = false;
    public Color splitColour;

    [Header("Enemy References")]
    public TheSpawner spawner;
    public TheHero hero;
    public TheManager manager;
    public GameObject damageTextPrefab;
    public SpriteRenderer rend;

    Vector2 dir;
    float angle;

    // public TheEnemy()
    // {
    //     SetAbility();
    // }

    private void Start() 
    {
        //rend = GetComponent<SpriteRenderer>();

        spawner = FindObjectOfType<TheSpawner>();
        hero = FindObjectOfType<TheHero>();

        manager = FindObjectOfType<TheManager>();

        currentHealth = maxHealth;
        isAlive = true;
    }

    private void Update() 
    {
        if(!isAlive)
        {
            Die();
        }

        if(isAlive)
        {   
            MoveToPlayer();    
        }
    }

    public void SetAbility()
    {
        int n = Random.Range(0, enemyAbilities.Length);

        if(enemyAbilities[n] == "None")
        {
            doubleDamage = false;
            bulletSplit = false;
            return;
        }
        if(enemyAbilities[n] == "Double Damage")
        {
            doubleDamage = true;
            bulletSplit = false;

            rend.color = doubleDamageColour;
            return;
        }
        if(enemyAbilities[n] == "Split")
        {
            doubleDamage = false;
            bulletSplit = true;

            rend.color = splitColour;
            return;
        }
    }

    private void MoveToPlayer()
    {
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
        manager.units++;

        spawner.enemies.Remove(this.gameObject);
        Destroy(gameObject);      
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise(damage);

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
}
