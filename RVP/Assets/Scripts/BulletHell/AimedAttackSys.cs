using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedAttackSys : MonoBehaviour
{
    public GameObject[] projectilePrefab;
    public Transform[] emitters;
    public Transform target;

    public float aimTimer, aimCooldown;
    public float bulletOffset = 1;

    public AimedSystem aimedSystem;
    public AimedSystem aimedSystemRefState;
    public enum AimedSystem
    {
        AimedSwitch,
        AimedSingle,
        AimedTriple
    }
    // Start is called before the first frame update
    void Start()
    {
       aimedSystem = AimedSystem.AimedSingle; 
    }

    // Update is called once per frame
    void Update()
    {
        aimTimer += Time.deltaTime;

        if(aimTimer >= aimCooldown)
        {
            AimedAttackHandler();
            aimTimer = 0;
        }
    }

    void AimedAttackHandler()
    {
        switch (aimedSystem)
        {
            case AimedSystem.AimedSwitch:
            {
                if(aimedSystemRefState == AimedSystem.AimedSingle)
                {
                    aimedSystem = AimedSystem.AimedTriple;
                }
                else if(aimedSystemRefState == AimedSystem.AimedTriple)
                {
                    aimedSystem = AimedSystem.AimedSingle;
                }

                break;
            }

            case AimedSystem.AimedSingle:
            {
                aimedSystemRefState = AimedSystem.AimedSingle;
                SingleShot();
                break;
            }

            case AimedSystem.AimedTriple:
            {
                aimedSystemRefState = AimedSystem.AimedTriple;
                TripleShot();
                break;
            }

            default:
                break;
        }
    }

    private void SingleShot()
    {
        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - emitters[0].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        Instantiate(projectilePrefab[0], emitters[0].position, rot);
    }

    private void TripleShot()
    {
        emitters[0].rotation = Quaternion.Euler(0, 0, 0);
        
        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - emitters[0].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        emitters[0].rotation = rot;
        Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, rot).transform;
        Instantiate(projectilePrefab[0], emitters[0].position + bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[0], emitters[0].position - bulletOffset * bullet.right, bullet.rotation);
    }
}
