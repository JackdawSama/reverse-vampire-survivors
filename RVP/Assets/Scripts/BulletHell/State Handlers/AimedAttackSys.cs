using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedAttackSys : MonoBehaviour
{
    [Header("Aimed System State")]
    public AimedSystem aimedSystem;
    public AimedSystem aimedSystemRefState;
    public AimedSystem[] AimedSystemStates = {AimedSystem.ModeOne};
    public AimedSystem[] TripleArray = 
    {
        AimedSystem.ModeTwo,
        AimedSystem.ModeThree
    };

    public AimedSystem[] ChaosArray = 
    {
        AimedSystem.ModeChaos,
        AimedSystem.ModeChaosFlipped
    };
    public enum AimedSystem
    {
        Inactive,
        ModeOne,
        ModeTwo,
        ModeThree,
        ModeChaos,
        ModeChaosFlipped
    }

    public GameObject[] projectilePrefab;
    public Transform[] emitters;
    public Transform target;
    public TheHero hero;

    public float healthPercent;
    public float bulletOffset = 1;

    [Header("Timers")]
    public float aimTimer; 
    public float aimCooldown;
    public float modeTimer;
    public float modeCooldown;

    // Start is called before the first frame update
    void Start()
    {
        aimedSystem = AimedSystem.ModeOne;
        aimTimer = 0;
        //aimCooldown = modeOneCooldown;
        hero = GetComponent<TheHero>();

        healthPercent = hero.HealthPercentage();
    }

    // Update is called once per frame
    void Update()
    {
        aimTimer += Time.deltaTime;

        //modeTimer += Time.deltaTime;

        if(aimTimer >= aimCooldown)
        {
            AimedHandler();
            aimTimer = 0;
        }
    }

    void AimedHandler()
    {
        switch (aimedSystem)
        {
            case AimedSystem.Inactive:
            {
                
                aimedSystem = AimedSystem.ModeOne;

                break;
            }

            case AimedSystem.ModeOne:
            {
                aimedSystemRefState = AimedSystem.ModeOne;
                TripleShotSame();

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(1, AimedSystemStates.Length);
                    aimedSystem = AimedSystemStates[randomNum];
                    modeTimer = 0;
                }

                break;
            }

            case AimedSystem.ModeTwo:
            {
                aimedSystemRefState = AimedSystem.ModeTwo;
                TripleShotMixed();

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(1, AimedSystemStates.Length);
                    aimedSystem = AimedSystemStates[randomNum];
                    modeTimer = 0;
                }

                break;
            }

            case AimedSystem.ModeThree:
            {
                aimedSystemRefState = AimedSystem.ModeThree;
                TripleShotMixedFlipped();

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(1, AimedSystemStates.Length);
                    aimedSystem = AimedSystemStates[randomNum];
                    modeTimer = 0;
                }
                break;
            }

            case AimedSystem.ModeChaos:
            {
                aimedSystemRefState = AimedSystem.ModeChaos;
                FiveFire();

                if(modeTimer >= modeCooldown)
                {
                    aimedSystem = AimedSystem.ModeChaosFlipped;
                    modeTimer = 0;
                }

                break;
            }

            case AimedSystem.ModeChaosFlipped:
            {
                aimedSystemRefState = AimedSystem.ModeChaosFlipped;
                FiveFireFlipped();

                if(modeTimer >= modeCooldown)
                {
                    int randomNum = Random.Range(1, AimedSystemStates.Length);
                    aimedSystem = AimedSystemStates[randomNum];
                    modeTimer = 0;
                }

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
        // Transform bullet = Instantiate(projectilePrefab[0], emitters[0].position, rot, hero.transform).transform;
        // Instantiate(projectilePrefab[0], emitters[0].position + bulletOffset * 1.5f * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[0], emitters[0].position - bulletOffset * 1.5f * bullet.right, bullet.rotation, hero.transform);

        GameObject bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position + bulletOffset * 1.5f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position - bulletOffset * 1.5f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

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
        // Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, rot, hero.transform).transform;
        // Instantiate(projectilePrefab[0], emitters[0].position + bulletOffset * 1.8f * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[0], emitters[0].position - bulletOffset * 1.8f * bullet.right, bullet.rotation, hero.transform);

        GameObject bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position + bulletOffset * 1.8f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position - bulletOffset * 1.8f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }
    
    }

    private void TripleShotMixedFlipped()
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
        // Transform bullet = Instantiate(projectilePrefab[0], emitters[0].position, rot, hero.transform).transform;
        // Instantiate(projectilePrefab[1], emitters[0].position + bulletOffset * 1.8f * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[1], emitters[0].position - bulletOffset * 1.8f * bullet.right, bullet.rotation, hero.transform);

        GameObject bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position + bulletOffset * 1.8f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position - bulletOffset * 1.8f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

    }
    private void FiveFire()
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
        // Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, rot, hero.transform).transform;
        // Instantiate(projectilePrefab[1], emitters[0].position + bulletOffset * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[1], emitters[0].position - bulletOffset * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[0], emitters[0].position + 2 * bulletOffset * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[0], emitters[0].position - 2 * bulletOffset * bullet.right, bullet.rotation, hero.transform);
    
        GameObject bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position + bulletOffset * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position - bulletOffset * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position + bulletOffset * 2f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position - bulletOffset * 2f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }
    
    }

    private void FiveFireFlipped()
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
        // Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, rot, hero.transform).transform;
        // Instantiate(projectilePrefab[0], emitters[0].position + bulletOffset * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[0], emitters[0].position - bulletOffset * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[1], emitters[0].position + 2 * bulletOffset * bullet.right, bullet.rotation, hero.transform);
        // Instantiate(projectilePrefab[1], emitters[0].position - 2 * bulletOffset * bullet.right, bullet.rotation, hero.transform);
    
        GameObject bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position + bulletOffset * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(5);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position - bulletOffset * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position + bulletOffset * 2f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }

        bullet = PoolingManager.Instance.GetProjectile(6);
        if(bullet != null)
        {
            bullet.transform.position = emitters[0].position - bulletOffset * 2f * bullet.transform.right;
            bullet.transform.rotation = emitters[0].rotation;
        }
    
    }
}
