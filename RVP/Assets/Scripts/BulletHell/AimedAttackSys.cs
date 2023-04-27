using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedAttackSys : MonoBehaviour
{
    public GameObject[] projectilePrefab;
    public Transform[] emitters;
    public Transform target;
    public TheHero hero;

    public float healthPercent;

    public float aimTimer, aimCooldown;
    public float bulletOffset = 1;

    public AimedSystem aimedSystem;
    public AimedSystem aimedSystemRefState;
    public enum AimedSystem
    {
        Inactive,
        TripleFireSame,
        TripleFireMixed,
        FiveFire
    }
    // Start is called before the first frame update
    void Start()
    {
        aimedSystem = AimedSystem.TripleFireSame;
        hero = GetComponent<TheHero>();

        healthPercent = hero.HealthPercentage();
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

    void StatusCheck()
    {
        healthPercent = hero.HealthPercentage();

        if(healthPercent <= 75f && healthPercent > 50f)
        {
            aimedSystem = AimedSystem.TripleFireSame;
        }
        else if(healthPercent <= 50f && healthPercent > 25f)
        {
            aimedSystem = AimedSystem.TripleFireMixed;
        }
        else if(healthPercent <= 25f)
        {
            aimedSystem = AimedSystem.FiveFire;
        }
    }

    void AimedAttackHandler()
    {
        switch (aimedSystem)
        {
            case AimedSystem.Inactive:
            {
                break;
            }

            case AimedSystem.TripleFireSame:
            {
                aimedSystemRefState = AimedSystem.TripleFireSame;
                SingleShot();
                break;
            }

            case AimedSystem.TripleFireMixed:
            {
                aimedSystemRefState = AimedSystem.TripleFireMixed;
                TripleShotMixed();
                break;
            }

            case AimedSystem.FiveFire:
            {
                aimedSystemRefState = AimedSystem.FiveFire;
                TripleShotMixed();
                break;
            }

            default:
                break;
        }
    }


    private void TripleShotSame()
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

    private void TripleShotMixed()
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
    private void SingleShot()
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
        Instantiate(projectilePrefab[1], emitters[0].position + bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[1], emitters[0].position - bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[0], emitters[0].position + 2 * bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[0], emitters[0].position - 2 * bulletOffset * bullet.right, bullet.rotation);
    }
}
