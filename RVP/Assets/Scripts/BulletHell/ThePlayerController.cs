using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThePlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed = 2f;
    public float angle;
    public Vector2 center;
    public float radius;

    [Header("Units Imbue")]
    public Collider2D[] unitsList;
    public float imbueRadius;
    public LayerMask imbueLayer;

    [Header("Player Checks")]
    public bool invincible;

    [Header("Attack Cooldown")]
    public float attackTimer = 0;
    public float attackCooldown = 0.2f;

    [Header("Controls")]
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public Vector2 lookDir;

    [Header("Player References")]
    public TheHero hero;
    public Transform bulletSpawn;
    public GameObject projectilePrefab;
    public GameObject damageTextPrefab;
    public SpriteRenderer rend;

    void Start()
    {
        center = hero.transform.position;
        rend = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        MovePlayer();
        MouseLook();

        attackTimer += Time.deltaTime;

        if(Input.GetKey(KeyCode.Space) && attackTimer > attackCooldown)
        {
            Attack();

            attackTimer = 0;
        }

        if(Input.GetMouseButtonDown(0))
        {
            //Imbues Units

            Debug.Log("Mouse Button Clicked");
            ImbueUnits();
        }
    }

    void MovePlayer()
    {
        center = hero.transform.position;

        if(Input.GetKey(moveLeft))
        {
            angle += moveSpeed * Time.deltaTime;
            rend.flipX = false;

        }
        else if(Input.GetKey(moveRight))
        {
            angle += -moveSpeed * Time.deltaTime;
            rend.flipX = true;
        }


        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * -radius;
        transform.position = center + offset;
    }

    void MouseLook()
    {
        lookDir = (hero.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        bulletSpawn.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Attack()
    {
        Vector2 attackRef = bulletSpawn.position;
        GameObject bullet = Instantiate(projectilePrefab, attackRef, bulletSpawn.rotation);
        bullet.GetComponent<TheEnemyBullet>().SetReference(gameObject.GetComponent<ThePlayerController>());
    }

    private void ImbueUnits()
    {
        Debug.Log("Imbue Units Called");
        //gets an array of Units and Imbues them shields damage 
        unitsList = Physics2D.OverlapCircleAll(transform.position, imbueRadius, imbueLayer);
        if(unitsList == null)
        {
            Debug.Log("List is Empty");
        }

        foreach(Collider2D unit in unitsList)
        {
            Debug.Log("Units Imbued");
            unit.GetComponent<TheEnemy>().isImbued = true;                  //sets imbue to true which deals damage to shields
            unit.GetComponent<TheEnemy>().Imbued();
        }
    }

    public void TakeDamage(float damage)
    {
        if(invincible)
        {
            //if invincible doesn't take damage
            return;
        }
        
        currentHealth -= damage;
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise(damage);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Unit Died");
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(center, transform.position);
        Gizmos.DrawWireSphere(transform.position, imbueRadius);
    }
}
