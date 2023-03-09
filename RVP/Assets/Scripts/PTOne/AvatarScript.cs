using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : MonoBehaviour
{
    //public bool testBool;
    bool isAttacking;

    [Header("AVATAR References")]
    //Avatar References
    public AvatarClass avatar;
    [SerializeField] GameObject attackSprite;
    [SerializeField] SpriteRenderer avatarSprite;
    //Avatar References end

    [Header("INIT Variables")]
    //Avatar Init Variables
    [SerializeField]int startLevel;
    [SerializeField]int startHP;
    [SerializeField]int startDamage;
    [SerializeField]int startAttackSpeed;
    [SerializeField]int corruptionThreshold;
    [SerializeField]float movementSpeed;
    public int currentLevel;
    [SerializeField] int startingBaseExp;
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
    }

    // Update is called once per frame
    void Update()
    {   
        attackTimer += Time.deltaTime;

        // if(avatar.currentCorruption >= avatar.corruptionThreshold)
        // {
        //     avatar.Corrupted();
        //     gameObject.SetActive(false);
        // }

        // if(attackTimer >= avatar.attackSpeed & !avatar.isCorrupted)
        // {
        //     isAttacking = true;
        //     Attack();
        //     if(!isAttacking)
        //     {
        //         attackSprite.transform.localScale = new Vector3(0, 0, 1);
        //         attackSprite.SetActive(false);
        //         attackTimer = 0;
        //     }
        // }

        // if(avatar.currentHP <= 0)
        // {
        //     avatar.currentHP = 0;
        //     avatar.PlayerDeath();
        // }
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
}
