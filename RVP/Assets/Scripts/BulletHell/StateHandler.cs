using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public Vector2 roamPoint;
    public float moveSpeed;
    public bool isRoaming;
    public float idleTimer, idleCooldown;
    public float minIdleTime = 0, maxIdleTime = 3;
    public float minRoamSearch, maxRoamSearch;
    public HeroState currentState;
    public enum HeroState
    {
        idle,
        roam,
        interested
    }

    private void Start() 
    {
        idleCooldown = Random.Range(minIdleTime, maxIdleTime);
        currentState = HeroState.idle;
    }
    private void StateSystem()
    {
        switch(currentState)
        {
            case HeroState.idle:
                //In Idle state stay at one point and deal some damage
                isRoaming = false;
                idleTimer += Time.deltaTime;

                if(idleTimer >= idleCooldown)
                {
                    currentState = HeroState.roam;
                    roamPoint = FindPointWithinRadius(minRoamSearch, maxRoamSearch);
                    isRoaming = true;
                    idleTimer = 0;
                }
                break;

            case HeroState.roam:
                //In Roam state find a random point and move towards it

                MoveToPoint(roamPoint);
                if(transform.position == (Vector3)roamPoint)
                {
                    idleCooldown = Random.Range(minIdleTime, maxIdleTime);
                    currentState = HeroState.idle;
                }
                break;

            case HeroState.interested:
                //Start moving towards this point and when reached switch to either idle or roam
                break;
            default:
                break;

        }
    }

    private void MoveToPoint(Vector2 target)
    {
        //Change the turn toward into facing left or facing right

        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    Vector2 FindPointWithinRadius(float minRad, float maxRad)
    {
        Vector2 newPos;

        float angle = Random.Range(0f, 360f);

        newPos.x = transform.position.x + (Random.Range(minRad, maxRad) * Mathf.Cos(angle / (180f / Mathf.PI)));
        newPos.y = transform.position.y + (Random.Range(minRad, maxRad) * Mathf.Sin(angle / (180f / Mathf.PI)));

        return newPos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRoamSearch);
        Gizmos.DrawWireSphere(transform.position, maxRoamSearch);
    }
}
