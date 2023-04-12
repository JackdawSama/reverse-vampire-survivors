using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacter : MonoBehaviour
{
    [Header("Attack Cooldown")]
    [SerializeField] float attackTimer;
    [SerializeField] float attackCoolDown;

    [Header("Bullet Hell Variables")]
    [SerializeField] GameObject preFab;
    [SerializeField] float projectiles;
    [SerializeField] float angle;
    [SerializeField] float projectileAngle;
    [SerializeField] Transform cannon;
    [SerializeField] Quaternion rot;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rot = Quaternion.Euler(0, 0, cannon.rotation.z);
            Attack();
        }
    }

    void Attack()
    {
        for(int i = 0; i < projectiles; i++)
        {
            Debug.Log(rot.z);
            GameObject bullet = Instantiate(preFab, transform.position, Quaternion.Euler(0 , 0, rot.z));
            rot.z += angle;
        }
    }

    void Attack2()
    {
        float dirX = transform.position.x + Mathf.Sin((angle * Mathf.PI)/180f);
        float dirY = transform.position.y + Mathf.Cos((angle * Mathf.PI)/180f);
    }

}
