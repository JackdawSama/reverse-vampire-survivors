using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float attackCD;
    [SerializeField] float attackTimer;
    [SerializeField] float spriteTimer = 0;
    [SerializeField] float spriteCD = 0.5f;
    bool isAttack = false;

    public int health;
    public int damage = 10;
    [SerializeField] int maxDamage;
    [SerializeField] int minDamage;
    [SerializeField] Collider2D[] colliders;

    [SerializeField] GameObject attackSprite;
    [SerializeField] Vector2 attackSize;
    [SerializeField] EnemyScript enemy;
    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackCD;
        attackSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0)
        {
            isAttack = true;
            Attack();
        }
        if(isAttack)
        {
            SetSprite();
        }

        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Attack()
    {
        //Debug.Log("Attacking!");
        attackSprite.SetActive(true);
        colliders = Physics2D.OverlapBoxAll(attackSprite.transform.position, attackSize, 0);
        ApplyDamage(colliders);
        attackTimer = attackCD;
        //damage = CalculateDamage(maxDamage, minDamage);
    }

    void ApplyDamage(Collider2D[] colliders)
    {
        for(int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log(colliders[i].gameObject.name);
            if(colliders[i].gameObject.tag == "Enemy")
            {
                enemy = colliders[i].GetComponent<EnemyScript>();
                enemy.TakeDamage(damage);
            }
            
        }
    }

    int CalculateDamage(int min, int max)
    {
        int returnDamage = Random.Range(min, max);

        return returnDamage;
    }

    void SetSprite()
    {
        spriteTimer += Time.deltaTime;
        if(spriteTimer >= spriteCD)
        {
            attackSprite.SetActive(false);
            isAttack = false;
            spriteTimer = 0;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         Debug.Log("Taking Damage!");

    //     }
    // }
}
