using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    public float currentHealth;
    public float maxHealth = 20f;
    public float moveSpeed;
    public float damage;
    public Color imbueColour;


    [Header("Enemy Checks")]
    public bool isAlive;
    public bool isImbued = false;

    [Header("Enemy References")]
    public TheSpawner spawner;
    public TheHero hero;
    public TheManager manager;
    public GameObject damageTextPrefab;
    public SpriteRenderer rend;

    private void Start() 
    {

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

   

    private void MoveToPlayer()
    {
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

    public void Imbued()
    {
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise("IM");
        rend.color = imbueColour;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Hero")
        {
            //if of imbued deals damage to hero shields
            if(isImbued)
            {
                //other.gameObject.GetComponent<HeroCharacter>().DamageShields(damage);
                other.gameObject.GetComponent<TheHero>().DamageShields(damage);
            }
            Die();
        }    
    }
}
