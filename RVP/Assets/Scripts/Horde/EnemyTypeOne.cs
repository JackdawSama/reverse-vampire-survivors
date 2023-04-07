using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeOne : MonoBehaviour
{
    [Header("Enemy Variables")]
    public float currentHealth;
    public float maxHealth = 20f;
    public float moveSpeed;
    float moveHorizontal;
    float moveVertical;
    Vector2 moveVector;

    void Update()
    {
        Move();
    }

    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        moveVector = new Vector2(moveHorizontal, moveVertical);
        moveVector = moveVector.normalized * moveSpeed * Time.deltaTime;

        transform.position += (Vector3)moveVector;
    }

    
}
