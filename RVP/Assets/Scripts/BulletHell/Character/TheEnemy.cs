using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    public float currentHealth;
    public float maxHealth = 20f;
    public float moveSpeed;
    float damage;

    [Header("Enemy Imbue Variables")]
    public bool isImbued = false;
    public float imbuedDamage = 1.5f;
    public float imbuedHealth = 45f;


    [Header("Enemy Checks")]
    public bool isAlive;
    public bool isInvincible;

    [Header("Enemy References")]
    public TheSpawner spawner;
    public TheHero hero;
    public TheManager manager;
    public GameObject damageTextPrefab;
    public SpriteRenderer rend;
    public Sprite newSprite;
    public Animator animator;
    public RuntimeAnimatorController animController;

    private void Start() 
    {
        spawner = FindObjectOfType<TheSpawner>();
        hero = FindObjectOfType<TheHero>();

        manager = FindObjectOfType<TheManager>();

        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        isAlive = true;
        isInvincible = true;
    }

    private void Update() 
    {
        if(!isAlive)
        {
            Die();
        }

        if(isAlive && hero)
        {   
            MoveToPlayer();    
        }

        float distance = Vector2.Distance(transform.position, hero.transform.position);

        if(distance <= 9.5f)
        {
            isInvincible = false;
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
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform).GetComponent<TheDamageText>().Initialise(damage);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Imbued()
    {
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform).GetComponent<TheDamageText>().Initialise("IM");

        damage = imbuedDamage;

        maxHealth = imbuedHealth;
        currentHealth = maxHealth;

        rend.sprite = newSprite;
        animator.runtimeAnimatorController = animController;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Hero" && isImbued)
        {
            other.gameObject.GetComponent<TheHero>().TakeDamage(damage);
            Die();
        }
        else 
        {
            Die();
        }   
    }
}
