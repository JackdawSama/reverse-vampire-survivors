using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : Subject
{
    //public bool testBool;
    bool isAttacking;

    [Header("AVATAR References")]
    //Avatar References
    public AvatarClass avatar;
    //AvatarScript avatar;
    [SerializeField] GameObject attackSprite;
    [SerializeField] SpriteRenderer avatarSprite;
    //Avatar References end

    [Header("INIT Variables")]
    //Avatar Init Variables
    public int startLevel;
    public int startHP;
    public int startDamage;
    public int startAttackSpeed;
    public int corruptionThreshold;
    public int currentLevel;
    public  int startingBaseExp;
    //Avatar Init Variables end

    //Tracking Variables
    int totalSouls;
    //End Tracking Variables

    //Attack Variables
    float attackTimer;
    [SerializeField]public Transform attackOrigin;
    [SerializeField]public float attackRange;
    float resetTimer;
    [SerializeField]float growRate; 
    //Attack Variables end


    // Start is called before the first frame update
    void Start()
    {   
        isAttacking = false;
        attackTimer = 0;
        resetTimer = 0;
        avatar = new AvatarClass(true, startLevel, startHP, startDamage, startAttackSpeed, startingBaseExp, corruptionThreshold);
        avatar.InitStats();
        currentLevel = avatar.playerLevel;

        avatar.totalSouls = 0;

        avatarSprite = GetComponent<SpriteRenderer>();

        attackOrigin = transform;

        attackSprite.transform.localScale = new Vector3(0, 0, 1);
        attackSprite.SetActive(false);

        NotifyObservers(Actions.AvatarLevelUp);
    }

    // Update is called once per frame
    void Update()
    {   
        attackTimer += Time.deltaTime;

        if(avatar.currentCorruption >= avatar.corruptionThreshold)
        {
            avatar.Corrupted();
            gameObject.SetActive(false);
        }

        if(attackTimer >= avatar.attackSpeed & !avatar.isCorrupted)
        {
            isAttacking = true;
            Attack();
            if(!isAttacking)
            {
                attackSprite.transform.localScale = new Vector3(0, 0, 1);
                attackSprite.SetActive(false);
                attackTimer = 0;
            }
        }

        if(avatar.currentHP <= 0)
        {
            avatar.currentHP = 0;
            avatar.PlayerDeath();
            Debug.Log("PLAYER DIED");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attack Speed :" + avatar.attackSpeed);
            //Debug.Log("Attack Base :" + avatar);
        }

        if(avatar.currentExp >= avatar.expToNextLevel)
        {
            NotifyObservers(Actions.AvatarLevelUp);
            Debug.Log("LEVEL UP");
        }
    }


    private void Attack()
    {
        avatar.Damage();
        attackSprite.SetActive(true);
        attackSprite.transform.localScale = Vector3.Lerp(attackSprite.transform.localScale, new Vector3(attackRange, attackRange, 1), Time.deltaTime * growRate);

        if(attackSprite.transform.localScale.x >= attackRange - 0.15f)
        {
            attackSprite.transform.localScale = new Vector3(attackRange, attackRange, 1);
            isAttacking = false;
        }
    }

    public Vector2 KnockBackCalc(float knockBackForce,  Vector2 minionPos)
    {
        Vector2 distance = (Vector2) transform.position - minionPos;
        distance.Normalize();
        Vector2 knockback = distance * knockBackForce;

        Debug.Log("Knockback: " + knockBackForce +", " + knockback);

        // Vector2 dir = transform.position - collision.transform.position;
        // dir.Normalize();

         if(distance.x > 0)
        {
            knockback = new Vector2(-knockBackForce, 0);
        }
        else if(distance.x < 0)
        {
            knockback = new Vector2(knockBackForce, 0);
        }
        else if(distance.y > 0)
        {
            knockback = new Vector2(0, -knockBackForce);
        }
        else if(distance.y < 0)
        {
            knockback = new Vector2(0, knockBackForce);
        }

        return knockback;
    }

}
