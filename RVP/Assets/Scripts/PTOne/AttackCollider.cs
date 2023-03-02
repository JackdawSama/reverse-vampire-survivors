using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    AvatarScript avatar;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(avatar.attackOrigin.position, avatar.attackRange);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.gameObject.tag == "Enemy")
            {
                enemy.gameObject.GetComponent<MinionScript>().TakeDamage(avatar.avatar.currentDamage);
                Debug.Log("Enemy Hit");
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "Enemy")
    //     {
    //         collision.gameObject.GetComponent<MinionScript>().TakeDamage(avatar.avatar.currentDamage);
    //         Debug.Log("Enemy Hit");
    //     }
    // }
}
