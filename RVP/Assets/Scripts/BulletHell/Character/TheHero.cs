using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isActive;

    [Header("Shields Variables")]
    public float currentShields;
    public float maxShields = 100f;
    public float shieldRegenRate = 5f;

    [Header("Shields Timer")]
    public float shieldsTimer;
    public float shieldsCooldown;
    public float noDamagerTimer;

    [Header("Hero References")]
    public SpriteRenderer rend;

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

        isActive = true;
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
        //Destroy(gameObject);

        if(isActive)
        {
            isActive = false;
            damageFeedback.enabled = false;

            StartCoroutine(FadeOut());
        }
    }

    //?SECONDARY FUNCTIONS
    public float HealthPercentage()
    {
        float healthPercent = (currentHealth/maxHealth) * 100f;
        return healthPercent;
    }

    private IEnumerator FadeOut()
    {
        if(!isActive && !damageFeedback.enabled)
        {
            while(rend.color.a > 0)
            {
                Color newColor = rend.color;
                newColor.a -= Time.deltaTime;
                rend.color = newColor;
            }
            

            yield return new WaitForSeconds(2f);

            //Scene Change
            //Do a Scene Change to HighScore Screen
            SceneManager.LoadScene("Highscore");

        }
    }
}
