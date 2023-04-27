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
    public float bulletOffset = 1;

    [Header("Timers")]
    public float aimTimer; 
    public float aimCooldown;
    public float modeTimer;
    public float modeCooldown;
    [SerializeField] float modeOneCooldown;
    [SerializeField] float modeTwoCooldown;
    [SerializeField] float modeThreeCooldown;
    [SerializeField] float modeFourCooldown;
    [SerializeField] float modeChaosCooldown;

    [Header("Aimed System State")]
    public AimedSystem aimedSystem;
    public AimedSystem aimedSystemRefState;
    public enum AimedSystem
    {
        Inactive,
        ModeOne,
        ModeTwo,
        ModeThree,
        ModeFour,
        ModeFive,
        ModeChaos,
        ModeChaosFlipped
    }
    // Start is called before the first frame update
    void Start()
    {
        aimedSystem = AimedSystem.Inactive;
        aimTimer = 0;
        aimCooldown = modeOneCooldown;
        hero = GetComponent<TheHero>();

        healthPercent = hero.HealthPercentage();
    }

    // Update is called once per frame
    void Update()
    {
        aimTimer += Time.deltaTime;

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
                aimedSystemRefState = AimedSystem.Inactive;

                if(hero.shieldsActive)
                {
                    aimCooldown = modeOneCooldown;
                    aimedSystem = AimedSystem.ModeOne;
                }

                break;
            }
            case AimedSystem.ModeOne:
            {
                aimedSystemRefState = AimedSystem.ModeOne;
                TripleShotSame();

                if(hero.HealthPercentage() > 75f && !hero.shieldsActive)
                {
                    modeCooldown = modeTwoCooldown;
                    modeTimer = 0;
                    aimedSystem = AimedSystem.ModeTwo;
                }

                break;
            }

            case AimedSystem.ModeTwo:
            {
                modeTimer += Time.deltaTime;
                aimedSystemRefState = AimedSystem.ModeTwo;
                TripleShotMixed();

                if(hero.HealthPercentage() <= 75f && hero.HealthPercentage() > 50f)
                {
                    aimCooldown = modeThreeCooldown;
                    modeTimer = 0;
                    aimedSystem = AimedSystem.ModeThree;
                }

                break;
            }

            case AimedSystem.ModeThree:
            {
                modeTimer += Time.deltaTime;
                aimedSystemRefState = AimedSystem.ModeThree;
                TripleShotMixedFlipped();

                if(hero.HealthPercentage() <= 50f && hero.HealthPercentage() > 25f)
                {
                    aimCooldown = modeChaosCooldown;
                    modeTimer = 0;
                    aimedSystem = AimedSystem.ModeChaos;
                }

                if(modeTimer >= modeCooldown)
                {
                    aimedSystem = AimedSystem.ModeFour;
                    modeTimer = 0;
                }
                break;
            }

            case AimedSystem.ModeFour:
            {
                modeTimer += Time.deltaTime;
                aimedSystemRefState = AimedSystem.ModeFour;
                TripleShotMixed();

                if(hero.HealthPercentage() <= 50f && hero.HealthPercentage() > 25f)
                {
                    aimCooldown = modeChaosCooldown;
                    modeTimer = 0;
                    aimedSystem = AimedSystem.ModeChaos;
                }

                if(modeTimer >= modeCooldown)
                {
                    aimedSystem = AimedSystem.ModeThree;
                    modeTimer = 0;
                }
                
                break;
            }

            case AimedSystem.ModeChaos:
            {
                modeTimer += Time.deltaTime;
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
                modeTimer += Time.deltaTime;
                aimedSystemRefState = AimedSystem.ModeChaosFlipped;
                FiveFireFlipped();

                if(modeTimer >= modeCooldown)
                {
                    aimedSystem = AimedSystem.ModeChaos;
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
        Transform bullet = Instantiate(projectilePrefab[0], emitters[0].position, rot).transform;
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
        Transform bullet = Instantiate(projectilePrefab[0], emitters[0].position, rot).transform;
        Instantiate(projectilePrefab[0], emitters[1].position + bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[0], emitters[1].position - bulletOffset * bullet.right, bullet.rotation);
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
        Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, rot).transform;
        Instantiate(projectilePrefab[1], emitters[0].position + bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[1], emitters[0].position - bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[0], emitters[0].position + 2 * bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[0], emitters[0].position - 2 * bulletOffset * bullet.right, bullet.rotation);
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
        Transform bullet = Instantiate(projectilePrefab[1], emitters[0].position, rot).transform;
        Instantiate(projectilePrefab[0], emitters[0].position + bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[0], emitters[0].position - bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[1], emitters[0].position + 2 * bulletOffset * bullet.right, bullet.rotation);
        Instantiate(projectilePrefab[1], emitters[0].position - 2 * bulletOffset * bullet.right, bullet.rotation);
    }
}
