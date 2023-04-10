using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemyBullet : MonoBehaviour
{
    [Header("Bullet Variables")]
    public float moveSpeed;
    public float baseDamage;
    public float damage;
    float deathTimer;
    public float deadTimer;
    
    public float powerUpRadius;
    public ThePlayerController reference;

    Vector2 refFire;
    Transform refDir;

    // Start is called before the first frame update
    void Start()
    {
        refDir = GetComponent<Transform>();

        refFire = reference.bulletSpawn.up;
        deathTimer = 0;
    }

    void Update()
    {

        if(reference != null)
        {
            transform.position += (Vector3)reference.bulletSpawn.up * moveSpeed * Time.deltaTime;
            refDir.position.Normalize();
        }
        
        deathTimer += Time.deltaTime;

        if(deathTimer > deadTimer)
        {
            Destroy(gameObject);
        }
        
    }

    public void SetReference(ThePlayerController player)
    {
        reference = player;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Hero"))
        {
            //Debug.Log("HitHero");
            other.gameObject.GetComponent<TheHero>().TakeDamage(baseDamage);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, powerUpRadius);
    }
}
