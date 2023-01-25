using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Attacked Player");
        }
    }
}
