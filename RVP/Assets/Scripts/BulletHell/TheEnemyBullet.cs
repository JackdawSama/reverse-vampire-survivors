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
    public bool boostedDamage = false;
    
    public ThePlayerController reference;

    Vector2 refFire;
    Transform refDir;

    // Start is called before the first frame update
    void Start()
    {
        refDir = GetComponent<Transform>();

        refFire = reference.bulletSpawn.up;
        deathTimer = 0;

        damage = baseDamage;
    }

    void Update()
    {

        if(reference != null)
        {
            transform.position += (Vector3)refFire * moveSpeed * Time.deltaTime;
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
            other.gameObject.GetComponent<TheHero>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
