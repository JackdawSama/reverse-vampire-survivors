using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 target;
    Transform attackPoint;
    float attackRange = 0.5f;
    LayerMask enemyLayers;

    Rigidbody2D rb;

    float attackTimer;
    [SerializeField]float attackCooldown;
    GameObject attackR;
    GameObject attackL;
    float spriteTimer;
    [SerializeField] float spriteTimerCoolDown;

    Vector3 lastPos;
    bool facingRight = false;
    bool facingLeft = false;

    bool attacked;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        attackR = GameObject.Find("Attack R");
        attackL = GameObject.Find("Attack L");

        attackR.SetActive(false);
        attackL.SetActive(false);

        rb = GetComponent<Rigidbody2D>();

        attackTimer = 0;

        facingLeft = false;
        facingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lastPos = transform.position;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        agent.SetDestination(target);

        attackTimer += Time.deltaTime;
        if(attackTimer >= attackCooldown)
        {
            CalculateDirection();

            Attack();
            attacked = true;
            SetAttack();
            attackTimer = 0;
        }

        if(attacked)
        {
            spriteTimer += Time.deltaTime;
            FinishAttack();
        }
        
    }

    void Attack()
    {
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // foreach(Collider2D enemy in hitEnemies)
        // {
             Debug.Log("Attack");
        // }
    }

    void SetAttack()
    {
        //Debug.Log(rb.velocity.x);
        if(facingRight)
        {
            //Debug.Log("Attack Right");
            attackR.SetActive(true);
            return;
        }

        if(facingLeft)
        {
            //Debug.Log("Attack Left");
            attackL.SetActive(true);
        }
    }

    void FinishAttack()
    {

        if(spriteTimer >= spriteTimerCoolDown)
        {
            attackR.SetActive(false);
            attackL.SetActive(false);
            facingLeft = false;
            facingRight = false;
            spriteTimer = 0;
            attacked = false;
            //Debug.Log("Attack Finished");
        }
    }

    void CalculateDirection()
    {
        float direction = target.x - transform.position.x;

        if(target.x > 0)
        {
            Debug.Log("Right");
            facingRight = true;
        }
        if(target.x < 0)
        {
            Debug.Log("Left");
            facingLeft = true;
        }
    }
}
