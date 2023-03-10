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
    SpawnerSystem spawnerRef;

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

    public float knockBackForce;

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

            if(minion.currentHP <= 0)
            {
                Die();
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

        // //flush out missing items in the MinionList
        // foreach(GameObject item in spawnerRef.minionList)
        // {
        //     if(item == null)
        //     {
        //         spawnerRef.minionList.Remove(item);
        //     }
        // }
        //Set minion to inactive
        DestroyImmediate(gameObject);

    }

    Transform textPos;

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, avatarRef.transform.position, movementSpeed * Time.deltaTime);
    }

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


    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Minion dealt damage" + minion.maxDamage);
            avatarRef.avatar.TakeDamage(minion.maxDamage);
            Vector2 knockback = avatarRef.KnockBackCalc(knockBackForce, transform.position);
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }   
        
        if(other.gameObject.tag == "Weapon")
        {
            avatarRef.avatar.Damage();
            TakeDamage(avatarRef.avatar.currentDamage);
            Debug.Log("Minion took damage" + avatarRef.avatar.currentDamage);
            Vector2 knockback = avatarRef.KnockBackCalc(knockBackForce, transform.position);
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    // void OnCollisionStay2D(Collision2D other)
    // {
    //     if(other.gameObject.tag == "Player" && !resetDamage)
    //     {
    //         Debug.Log("!");
    //         avatarRef.avatar.TakeDamage(minion.maxDamage);
    //         resetDamage = true;
    //         Debug.Log("Minion dealt damage" + minion.currentDamage);
    //         resetDamageTimer = 0;
    //     } 
    // }

    
}
