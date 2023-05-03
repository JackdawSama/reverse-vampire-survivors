using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MovementStateHandler))]
[RequireComponent(typeof(AimedAttackSys))]
[RequireComponent(typeof(BulletHellSys))]
[RequireComponent(typeof(AimedBulletHell))]
[RequireComponent(typeof(AttackSysController))]
[RequireComponent(typeof(DamageFlash))]

public class TheHero : MonoBehaviour
{
    [Header("Hero Variables")]
    public float currentHealth;
    public float maxHealth = 100f;
    public float moveSpeed;

    [Header("Checks")]
    public bool isRoaming;

    [Header("Shields Variables")]
    public float currentShields;
    public float maxShields = 100f;

    [Header("Shields Timer")]
    public float shieldsTimer;
    public float shieldsCooldown;

    [Header("Hero References")]
    public Vector2 roamPoint;

    [Header("Hero Components")]
    public DamageFlash damageFeedback;
    public GameObject attackChangePrefab;
    public GameObject damageTextPrefab;

    private void Start() 
    {
        damageFeedback = GetComponent<DamageFlash>();

        currentHealth = maxHealth;
    }

    private void Update() 
    {   
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        damageFeedback.Flash();
        //Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform).GetComponent<TheDamageText>().Initialise(damage);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void DamageShields(float damage)
    {
        currentShields -= damage;
        damageFeedback.Flash();

        if(currentShields <= 0)
        {
            currentShields = 0;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    //?SECONDARY FUNCTIONS
    public float HealthPercentage()
    {
        float healthPercent = (currentHealth/maxHealth) * 100f;
        return healthPercent;
    }
}
