using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : MonoBehaviour
{
    public bool testBool;

    //Avatar References
    AvatarClass avatar;
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
    //Avatar Init Variables end

    //Movement Variables
    [SerializeField]Transform currentPos;
    Vector2 targetPos;
    [SerializeField]bool isMoving;
    [SerializeField] bool reachedNode;
    //Movement Variables end


    // Start is called before the first frame update
    void Start()
    {
        testBool = false;
        avatar = new AvatarClass(true, startLevel, startHP, startDamage, startAttackSpeed, corruptionThreshold);
    }

    // Update is called once per frame
    void Update()
    {
        // if(pathfinding.target != pathfinding.seeker)
        // {
        //     Move();
        // }

        if(Input.GetMouseButtonDown(0))
        {
            avatar.Corrupt(0.1f);
        }

        if(avatar.currentCorruption >= avatar.corruptionThreshold & !testBool)
        {
            testBool = true;
            avatar.Corrupted();
        }
    }

    private void Attack()
    {

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

    }

    private void FindNewTarget()
    {

    }
}
