using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : MonoBehaviour
{
    //Avatar References
    AvatarClass avatar;
    [SerializeField]Grid grid;
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
    Transform targetPos;
    //Movement Variables end


    // Start is called before the first frame update
    void Start()
    {
        avatar = new AvatarClass(true, startLevel, startHP, startDamage, startAttackSpeed, corruptionThreshold);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Attack()
    {

    }

    private void Move()
    {
        for(int i = 0; i < grid.path.Count; i++)
        {
            currentPos.position = transform.position;
            targetPos.position = grid.path[i].worldPosition;
            transform.position = Vector2.MoveTowards(currentPos.position, targetPos.position, movementSpeed * Time.deltaTime);
        }
    }

    private void FindNewTarget()
    {

    }
}
