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
    //public Rigidbody2D rb;
    public TheEnemy reference;

    Vector2 refFire;
    Transform refDir;

    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody2D>();
        //reference = FindObjectOfType<TheEnemy>();

        refDir = GetComponent<Transform>();

        refFire = reference.bulletSpawn.up;
        //refDir = transform;
        deathTimer = 0;
    }

    void Update()
    {
        //transform.position += (Vector3)refFire * moveSpeed * Time.deltaTime;
        if(reference != null)
        {
            transform.position += (Vector3)reference.bulletSpawn.up * moveSpeed * Time.deltaTime;
            refDir.position.Normalize();
        }
        else if(reference == null)
        {
            transform.position += refDir.up * moveSpeed * Time.deltaTime;
        }
        
        deathTimer += Time.deltaTime;

        if(deathTimer > deadTimer)
        {
            Destroy(gameObject);
        }
        
    }

    public void SetReference(TheEnemy enemy)
    {
        reference = enemy;
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
