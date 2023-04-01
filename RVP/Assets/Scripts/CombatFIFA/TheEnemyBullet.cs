using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemyBullet : MonoBehaviour
{
    [Header("")]
    public float moveSpeed;
    public float damage;
    float deathTimer;
    public float deadTimer;
    public Rigidbody2D rb;
    public TheEnemy reference;

    Vector2 refFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reference = FindObjectOfType<TheEnemy>();

        refFire = reference.bulletSpawn.up;
        deathTimer = 0;
    }

    void Update()
    {
        //transform.position += (Vector3)refFire * moveSpeed * Time.deltaTime;
        transform.position += (Vector3)reference.bulletSpawn.up * moveSpeed * Time.deltaTime;
        deathTimer += Time.deltaTime;

        if(deathTimer > deadTimer)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Hero"))
        {
            other.gameObject.GetComponent<TheHero>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }
}
