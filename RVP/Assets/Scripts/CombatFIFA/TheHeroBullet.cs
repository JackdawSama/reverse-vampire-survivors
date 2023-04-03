using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeroBullet : MonoBehaviour
{
    [Header("Variabless")]
    public float moveSpeed;
    public float damage;
    public Transform target;

    //[Header("References")]
    //public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();

        //target = FindObjectOfType<TheEnemy>().transform;
    }

    void Update()
    {
        Move();
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Move()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInChildren<TheEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }   
    }
}
