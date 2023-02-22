using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    PlayerController player;

    Collider2D[] enemies;

    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit " + other.gameObject.name);
            other.gameObject.GetComponent<EnemyScript>().health = other.gameObject.GetComponent<EnemyScript>().health - player.damage;
        }
    }
}
