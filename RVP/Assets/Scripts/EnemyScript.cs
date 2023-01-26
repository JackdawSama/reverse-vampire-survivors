using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] Vector3 direction;
    [SerializeField] UIController ui;
    [SerializeField] PlayerController player;

    int damage = 5;
    [SerializeField] int maxDamage;
    [SerializeField] int minDamage;

    public int health = 10;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        ui = FindObjectOfType<UIController>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took damage!");
        health = health - damage;
        Debug.Log("Enemy health: " + health);
    }

    void Die()
    {
        ui.soulsFarmed++;
        Destroy(gameObject);
    }

    // void OnTriggerEnter2D(Collider2D other) {
        
    //     if (other.gameObject.tag == "Player")
    //     {
    //         TakeDamage(player.damage);
    //     }
    // }
}
