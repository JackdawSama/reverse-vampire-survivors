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

    [SerializeField] PlayerUIScript playerUI;
    //Minion References end

    //Minion INIT Variables
    public int initLevel;
    public int initHealth;
    public int initDamage;
    public int initExp;
    public float initCorruptVal;
    public float movementSpeed;
    //Minion INIT Variables end

    public int minLevel;

    float resetDamageTimer;
    bool resetDamage;
    [SerializeField] float resetDamageCooldown;

    //DamageText Variables
    //[SerializeField]GameObject damageTextPrefab;
    bool isInvincible = false;
    //DamageText Variables end

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerUI = GameObject.Find("Canvas").GetComponent<PlayerUIScript>();

        avatarRef = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        //playerUI.minionLevel = avatarRef.avatar.playerLevel;
        minion = new MinionClass( avatarRef.avatar.playerLevel, initHealth, initDamage, initExp, initCorruptVal);
        minion.InitStats();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(avatarRef.avatar.isAlive)
        {
            Move();
            if(resetDamage)
            {
                resetDamageTimer += Time.deltaTime;
                resetDamage = false;
            }
        }
    }

    public void Die()
    {
        //Minion death
        minion.isAlive = false;
        
        //Add minion exp to player exp
        avatarRef.avatar.GainEXP(minion.currentExp);
        avatarRef.avatar.Corrupt(minion.corruptVal);
        avatarRef.avatar.soulsCollected++;

        //Set minion to inactive
        DestroyImmediate(gameObject);
    }

    Transform textPos;

    public void TakeDamage(int damage)
    {
        //Minion takes damage
        //Debug.Log("TakeDamage Called"); 
        // GameObject DamageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        // DamageText.transform.GetComponent<TextMeshPro>().SetText(damage.ToString());

        Debug.Log("TakeDamage Called"); 
        minion.currentHP -= damage;

        // if(!isInvincible)
        // {
        //     // GameObject DamageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        //     // DamageText.transform.GetComponent<TextMeshPro>().SetText(damage.ToString());

            
        //     //rb.AddForce(knockback, ForceMode2D.Impulse);
        //     isInvincible = true;
        //     if(isInvincible)
        //     {
        //         StartCoroutine(Invincibility());
        //     }
        // }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, avatarRef.transform.position, movementSpeed * Time.deltaTime);
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Minion dealt damage" + minion.currentDamage);
            avatarRef.avatar.TakeDamage(minion.maxDamage);
        }   
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && !resetDamage)
        {
            avatarRef.avatar.TakeDamage(minion.maxDamage);
            resetDamage = true;
            Debug.Log("Minion dealt damage" + minion.currentDamage);
            resetDamageTimer = 0;
        } 
    }

    
}
