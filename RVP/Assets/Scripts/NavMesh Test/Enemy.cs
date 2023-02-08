using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform target;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 direction;

    int damage = 2;

    public int health = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void FixedUpdate()
    {
        direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void TakeDamage()
    {
        health = health - damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
