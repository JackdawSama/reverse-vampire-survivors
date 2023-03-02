using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinionScript : MonoBehaviour
{
    //Minion References
    [SerializeField]AvatarScript avatarRef;
    MinionClass minion;
    //Minion References end

    //Minion INIT Variables
    public int initHealth;
    public int initDamage;
    public int initExp;
    public int movementSpeed;
    public float initCorruptVal;
    //Minion INIT Variables end

    //DamageText Variables
    [SerializeField]GameObject damageTextPrefab;
    //DamageText Variables end

    // Start is called before the first frame update
    void Start()
    {
        avatarRef = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        minion = new MinionClass(avatarRef.currentLevel, initHealth, initDamage, initExp, initCorruptVal);
        minion.InitStats();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Die()
    {
        //Minion death
        minion.isAlive = false;
        //Set minion to inactive
        gameObject.SetActive(false);
        //Add minion exp to player exp
        avatarRef.avatar.GainEXP(minion.currentExp);
        avatarRef.avatar.Corrupt(minion.corruptVal);
        //reset minion stats
        
    }

    public void TakeDamage(int damage)
    {
        //Minion takes damage
        Debug.Log("TakeDamage Called");
        GameObject DamageText = Instantiate(damageTextPrefab, transform);
        DamageText.transform.GetComponent<TextMeshPro>().SetText(damage.ToString());

        minion.currentHP -= damage;
        if(minion.currentHP <= 0)
        {
            minion.currentHP = 0;
            Die();
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, avatarRef.transform.position, movementSpeed * Time.deltaTime);
    }

    
}
