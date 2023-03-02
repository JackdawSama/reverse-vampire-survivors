using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinionScript : MonoBehaviour
{
    //Minion References
    [SerializeField]AvatarScript avatarRef;
    MinionClass minion;
    Rigidbody2D rb;
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
    bool isInvincible = false;
    //DamageText Variables end

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        avatarRef = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        minion = new MinionClass(avatarRef.currentLevel, initHealth, initDamage, initExp, initCorruptVal);
        minion.InitStats();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        if(minion.currentHP <= 0)
        {
            minion.currentHP = 0;
            gameObject.SetActive(false);
            avatarRef.avatar.GainEXP(minion.currentExp);
            avatarRef.avatar.Corrupt(minion.corruptVal);
            avatarRef.avatar.soulsCollected++;
           // Die();
        }
    }

    public void Die()
    {
        //Minion death
        minion.isAlive = false;
        //Set minion to inactive
        // gameObject.SetActive(false);
        DestroyImmediate(gameObject);
        //Add minion exp to player exp
        avatarRef.avatar.GainEXP(minion.currentExp);
        avatarRef.avatar.Corrupt(minion.corruptVal);
        //reset minion stats
        
    }

    Transform textPos;

    public void TakeDamage(int damage, Vector2 knockback)
    {
        //Minion takes damage
        // Debug.Log("TakeDamage Called"); 
        // GameObject DamageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        // DamageText.transform.GetComponent<TextMeshPro>().SetText(damage.ToString());

        if(!isInvincible)
        {
            Debug.Log("TakeDamage Called"); 
            GameObject DamageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
            DamageText.transform.GetComponent<TextMeshPro>().SetText(damage.ToString());

            minion.currentHP -= damage;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            isInvincible = true;
            if(isInvincible)
            {
                StartCoroutine(Invincibility());
            }
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, avatarRef.transform.position - new Vector3(0.5f, 0.5f,0) , movementSpeed * Time.deltaTime);
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    
}
