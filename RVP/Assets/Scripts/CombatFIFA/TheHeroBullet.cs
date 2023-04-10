using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeroBullet : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed;
    public float deltaInc, gammaInc;
    public float damage;
    public float deathTimer = 6f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BufferDeath());
    }

    IEnumerator BufferDeath()
    {
        yield return new WaitForSecondsRealtime(deathTimer);
        Destroy(gameObject);
    }

    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        deltaInc = deltaInc * gammaInc;
        moveSpeed += deltaInc;
    }

    void Move()
    {
        transform.position += transform.up * moveSpeed * Time.fixedDeltaTime; 
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        // if(other.gameObject.CompareTag("Enemy"))
        // {
        //     other.gameObject.GetComponentInChildren<TheEnemy>().TakeDamage(damage);
        //     Destroy(gameObject);
        // }
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<ThePlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInChildren<TheEnemy>().TakeDamage(damage);
            //Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<ThePlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }      
    }
}
