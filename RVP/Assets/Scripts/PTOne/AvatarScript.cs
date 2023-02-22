using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : MonoBehaviour
{
    public bool testBool;

    //Avatar References
    public AvatarClass avatar;
    [SerializeField]Grid grid;
    [SerializeField] Pathfinding pathfinding;
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
    [SerializeField]Vector2 attackOrigin;
    [SerializeField]float attackRange; 
    //Attack Variables end


    // Start is called before the first frame update
    void Start()
    {
        testBool = false;
        attackTimer = 0;
        avatar = new AvatarClass(true, startLevel, startHP, startDamage, startAttackSpeed, corruptionThreshold);
        avatar.InitStats();
        currentLevel = avatar.playerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        // if(pathfinding.target != pathfinding.seeker)
        // {
        //     Move();
        // }

        if(Input.GetMouseButtonDown(0))
        {
            //avatar.Corrupt(0.1f);
            FindDirection();
        }

        if(avatar.currentCorruption >= avatar.corruptionThreshold & !testBool)
        {
            testBool = true;
            avatar.Corrupted();
        }

        if(attackTimer >= avatar.attackSpeed)
        {
            Attack();
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
            Debug.Log("Facing Right");
            Debug.Log(direction);
        }
        else if(direction.x < 0)
        {
            //facing left
            //change avatar sprite to face left
            //set attack sprite to left
            Debug.Log("Facing Left");
            Debug.Log(direction);
        }

    }

    private void SetSprite()
    {
        //Set sprite based on direction
    }

    private void Attack()
    {
        Debug.Log("Attack");
        //Use OverlapCircle to get enemy colliders. Cycle through colliders and call TakeDamage on them.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackOrigin, attackRange);
        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<MinionScript>().TakeDamage(avatar.Damage());
        }
        attackTimer = 0;
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
        distance = targetPos - (Vector2)currentPos.position;
        direction = distance.normalized;
    }

    private void FindNewTarget()
    {

    }
}
