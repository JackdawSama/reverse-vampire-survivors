using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool isTakingFire;

    [Header("Shields Variables")]
    public float currentShields;
    public float maxShields = 100f;
    public float shieldRegenRate = 5f;

    [Header("Shields Timer")]
    public float shieldsTimer;
    public float shieldsCooldown;
    public float noDamagerTimer;

    [Header("Hero References")]
    public Vector2 roamPoint;

    [Header("Hero Components")]
    public DamageFlash damageFeedback;
    public GameObject attackChangePrefab;
    public GameObject damageTextPrefab;
    public AttackSysController attackController;

    private void Start() 
    {
        attackController = GetComponent<AttackSysController>();
        attackController.enabled = false;

        damageFeedback = GetComponent<DamageFlash>();

        currentHealth = maxHealth;
        currentShields = maxShields;
    }

    private void Update() 
    {   
        shieldsTimer += Time.deltaTime;

        if(shieldsTimer > shieldsCooldown)
        {
            isTakingFire = false;
            regenShields();
        }
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

    void regenShields()
    {
        if(currentShields <= maxShields && !isTakingFire)
        {
            currentShields += Time.deltaTime * shieldRegenRate;
            
            if(currentShields > maxShields)
            {
                currentShields = maxShields;
            }
        }
    }

    public void DamageShields(float damage)
    {
        isTakingFire = true;
        shieldsTimer = 0;

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
