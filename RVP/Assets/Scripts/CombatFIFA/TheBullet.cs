using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBullet : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public TheEnemy reference;

    Vector2 refFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reference = FindObjectOfType<TheEnemy>();

        refFire = reference.bulletSpawn.up;
    }

    void Update()
    {
        transform.position += (Vector3)refFire * moveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(reference.bulletSpawn.up * moveSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
