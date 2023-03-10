using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackCollider : MonoBehaviour
{
    AvatarScript avatar;

    //Vector2 distance;
    //[SerializeField] float knockBackForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // void CalculateDistance()
    // {
    //     //Calculate distance between avatar and enemy
    //     distance = transform.position - avatar.transform.position;
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {

            Debug.Log("Damage Give : " + avatar.avatar.currentDamage);
            other.gameObject.GetComponent<MinionScript>().TakeDamage(avatar.avatar.currentDamage);
        }
    }
}
