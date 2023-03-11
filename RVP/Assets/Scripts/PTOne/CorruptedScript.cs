using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedScript : MonoBehaviour
{
    AvatarScript avatar;
    public CorruptionClass corrupted;
    Rigidbody2D rb;

    [SerializeField] float knockBackForce;
    [SerializeField] float movementSpeed;
    [SerializeField] int health;

    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        rb = GetComponent<Rigidbody2D>();
        corrupted.InitStats();

        health = corrupted.maxHp;
    }

    void Update()
    {
        Move();

        if(avatar == null)
        {
            avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        }

        if(health <= 0)
        {
            corrupted.DeathState(avatar);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        //Corrupted takes damage

        Debug.Log("TakeDamage Called"); 
        health -= damage;
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, avatar.transform.position, movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Minion dealt damage" + corrupted.maxDamage);
            avatar.avatar.TakeDamage(corrupted.maxDamage);
            Vector2 knockback = avatar.KnockBackCalc(knockBackForce, transform.position);
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }   
        
        if(other.gameObject.tag == "Weapon")
        {
            avatar.avatar.Damage();
            TakeDamage(avatar.avatar.currentDamage);
            Debug.Log("Minion took damage" + avatar.avatar.currentDamage);
            Vector2 knockback = avatar.KnockBackCalc(knockBackForce, transform.position);
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

}
