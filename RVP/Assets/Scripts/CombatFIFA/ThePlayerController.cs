using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed = 2f;
    public float angle;
    public Vector2 center;
    public float radius;

    [Header("Controls")]
    public KeyCode moveLeft;
    public KeyCode moveRight;

    [Header("Player Variables")]
    public TheHero hero;
    public GameObject bullet;

    void Start()
    {
        center = hero.transform.position;
    }

    void Update()
    {
        MovePlayer();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(center, 0.1f);
        Gizmos.DrawLine(center, transform.position);
    }

    void MovePlayer()
    {
        center = hero.transform.position;
        
        if(Input.GetKey(moveLeft))
        {
            angle += moveSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(moveRight))
        {
            angle += -moveSpeed * Time.deltaTime;
        }


        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * -radius;
        transform.position = center + offset;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Unit Died");
            Die();
        }
    }

    private void Die()
    {
        //maybe switch off sprite and set a bool to dead later?
        Destroy(gameObject);
    }
}
