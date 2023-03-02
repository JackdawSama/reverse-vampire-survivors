using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackCollider : MonoBehaviour
{
    AvatarScript avatar;

    Vector2 distance;
    [SerializeField] float knockBackForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CalculateDistance()
    {
        //Calculate distance between avatar and enemy
        distance = transform.position - avatar.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            distance = transform.position - collision.transform.position;
            distance.Normalize();
            Vector2 knockback = distance * knockBackForce;

            Vector2 dir = avatar.transform.position - collision.transform.position;
            dir.Normalize();

            if(dir.x > 0)
            {
                knockback = new Vector2(-knockBackForce, 0);
            }
            else if(dir.x < 0)
            {
                knockback = new Vector2(knockBackForce, 0);
            }
            else if(dir.y > 0)
            {
                knockback = new Vector2(0, -knockBackForce);
            }
            else if(dir.y < 0)
            {
                knockback = new Vector2(0, knockBackForce);
            }
            
            collision.gameObject.GetComponent<MinionScript>().TakeDamage(avatar.avatar.currentDamage, knockback);
            // Debug.Log("from collider " +  avatar.avatar.currentDamage);
            // Debug.Log("Enemy Hit");
        }
    }
}
