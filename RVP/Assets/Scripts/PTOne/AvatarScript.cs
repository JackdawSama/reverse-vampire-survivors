using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : MonoBehaviour
{
    //public bool testBool;
    bool isAttacking;

    //Avatar References
    public AvatarClass avatar;
    [SerializeField]Grid grid;
    [SerializeField] Pathfinding pathfinding;
    [SerializeField] GameObject attackL;
    [SerializeField] GameObject attackR;
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

    //Movement Variables
    [SerializeField]Transform currentPos;
    Vector2 targetPos;
    [SerializeField]bool isMoving;
    [SerializeField] bool reachedNode;
    Vector2 distance;
    Vector2 direction;
    //Movement Variables end

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
        //testBool = false;
        isAttacking = false;
        attackTimer = 0;
        resetTimer = 0;
        avatar = new AvatarClass(true, startLevel, startHP, startDamage, startAttackSpeed, startingBaseExp, corruptionThreshold);
        avatar.InitStats();
        currentLevel = avatar.playerLevel;

        avatarSprite = GetComponent<SpriteRenderer>();
        currentPos = transform;
        targetPos = grid.path[0].worldPosition;

        attackOrigin = transform;

        attackL.SetActive(false);
        attackR.SetActive(false);
        attackSprite.transform.localScale = new Vector3(0, 0, 1);
        attackSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        SetSprite();

        if(avatar.currentCorruption >= avatar.corruptionThreshold)
        {
            avatar.Corrupted();
        }

        if(attackTimer >= avatar.attackSpeed)
        {
            isAttacking = true;
            Attack();
            if(!isAttacking)
            {
                attackSprite.transform.localScale = new Vector3(0, 0, 1);
                attackSprite.SetActive(false);
                attackTimer = 0;
                // Debug.Log(avatar.currentDamage);
                Debug.Log("Attack timer reset");
            }
        }

        if(avatar.currentHP <= 0)
        {
            avatar.currentHP = 0;
            avatar.PlayerDeath();
        }
    }

    private void FindDirection()
    {
        //Normalise vector and then use Y component to determine direction

        CalculateDistance();
        if(direction.x > 0)
        {
            //facing right
            //change avatar sprite to face right
            //set attack sprite to right
            attackOrigin = attackR.transform;
        }
        else if(direction.x < 0)
        {
            //facing left
            //change avatar sprite to face left
            //set attack sprite to left
            attackOrigin = attackL.transform;
        }

    }

    private void SetSprite()
    {
        //Set sprite based on direction

        CalculateDistance();

        if(direction.x > 0)
        {
            avatarSprite.flipX = false;
        }
        else if(direction.x < 0)
        {
            avatarSprite.flipX = true;
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

    private void Move()
    {
        for(int i = 0; i < grid.path.Count; i++)
        {
            if(!isMoving)
            {
                isMoving = true;
                reachedNode = false;
                currentPos.position = transform.position;
                targetPos = grid.path[i].worldPosition;
            }
            else if(isMoving)
            {
                transform.position = Vector2.MoveTowards(currentPos.position, targetPos, movementSpeed * Time.deltaTime);
                if((Vector2)currentPos.position == targetPos)
                {
                    isMoving = false;
                }
            }
        }
    }

    private void CalculateDistance()
    {
        targetPos = grid.path[0].worldPosition;
        currentPos.position = transform.position;
        distance = targetPos - (Vector2)currentPos.position;
        direction = distance.normalized;
    }
}
