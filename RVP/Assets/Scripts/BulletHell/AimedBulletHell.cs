using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedBulletHell : MonoBehaviour
{   
    [Header("Bullet Hell State")]
    public AimedBulletHellSys bulletHell;
    public AimedBulletHellSys bulletHellRefState;
    public AimedBulletHellSys[] BulletHellStates;

    public enum AimedBulletHellSys
    {
        AttackOne,
        QuadOne,
        QuadTwo,
        QuadThree,
        QuadFour,
        QuadOneThree,
        QuadTwoFour
    }

    [Header("References")]
    public TheHero hero;
    public Transform target;

    [Header("Timers")]
    [SerializeField] float bulletHellTimer; 
    [SerializeField] float bulletHellCooldown;
    [SerializeField] float modeTimer;
    // [SerializeField] float modeCooldown = 6f;

    [Header("Emitter Data")]
    public Transform[] emitters;
    public GameObject[] projectilePrefab;
    public List<AttackTypes> attackTypes;
    float emitterAngle, projectileAngle, projectileAmount;

    // Start is called before the first frame update
    void Start()
    {
        hero = GetComponent<TheHero>();

        bulletHell = AimedBulletHellSys.QuadOne;
        SetPattern(attackTypes[0]);
    }

    // Update is called once per frame
    void Update()
    {
        bulletHellTimer += Time.deltaTime;

        modeTimer += Time.deltaTime;
        
        if(bulletHellTimer >= bulletHellCooldown)
        {
            BulletHellHandler();
            bulletHellTimer = 0;
        }
    }

    void BulletHellHandler()
    {
        switch (bulletHell)
        {

            case AimedBulletHellSys.AttackOne:
            {
                bulletHellRefState = AimedBulletHellSys.AttackOne;
                SetPattern(attackTypes[0]);

                Chaos();

                break;
            }

            case AimedBulletHellSys.QuadOne:
            {
                bulletHellRefState = AimedBulletHellSys.QuadOne;
                SetPattern(attackTypes[1]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadTwo:
            {
                bulletHellRefState = AimedBulletHellSys.QuadTwo;
                SetPattern(attackTypes[1]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadThree:
            {
                bulletHellRefState = AimedBulletHellSys.QuadThree;
                SetPattern(attackTypes[1]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadFour:
            {
                bulletHellRefState = AimedBulletHellSys.QuadFour;
                SetPattern(attackTypes[1]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadOneThree:
            {
                bulletHellRefState = AimedBulletHellSys.QuadOneThree;
                SetPattern(attackTypes[2]);
                QuadDouble();

                break;
            }

            case AimedBulletHellSys.QuadTwoFour:
            {
                bulletHellRefState = AimedBulletHellSys.QuadTwoFour;
                SetPattern(attackTypes[2]);
                QuadDouble();

                break;
            }

            default:
                break;
        }
    }

    private void Chaos()
    {   
        //BH Attack that uses East, West and Origin Emitters
        
        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - emitters[0].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        emitters[0].rotation = rot;
        int x = (int) projectileAmount/2;
        for(int i = 0; i < projectileAmount; i++)
        {
            if (i<x)
            {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z - (projectileAngle * (i - projectileAmount/2)));
                bulletOne.transform.rotation = rotOne;
            }
        }

        bulletHellTimer = 0f;
    }

    private void QuadSingle()
    {  
        emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitterAngle));

        int x = (int) projectileAmount/2;
        for(int i = 0; i < projectileAmount; i++)
        {
            if (i<x)
            {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z - (projectileAngle * (i - projectileAmount/2)));
                bulletOne.transform.rotation = rotOne;
            }
        }

        bulletHellTimer = 0f;
    }
    private void QuadDouble()
    {  
        emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitterAngle));

        int x = (int) projectileAmount/2;
        for(int i = 0; i < projectileAmount; i++)
        {
            if (i<x)
            {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z - (projectileAngle * (i - projectileAmount/2)));
                bulletOne.transform.rotation = rotOne;
            }
        }

        emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitterAngle + 180f));

        x = (int) projectileAmount/2;
        for(int i = 0; i < projectileAmount; i++)
        {
            if (i<x)
            {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z - (projectileAngle * (i - projectileAmount/2)));
                bulletOne.transform.rotation = rotOne;
            }
        }

        bulletHellTimer = 0f;
    }

    private void QuadTwoFour()
    {  
        emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitterAngle));

        int x = (int) projectileAmount/2;
        for(int i = 0; i < projectileAmount; i++)
        {
            if (i<x)
            {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z - (projectileAngle * (i - projectileAmount/2)));
                bulletOne.transform.rotation = rotOne;
            }
        }

        emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitterAngle + 180f));

        x = (int) projectileAmount/2;
        for(int i = 0; i < projectileAmount; i++)
        {
            if (i<x)
            {
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z - (projectileAngle * (i - projectileAmount/2)));
                bulletOne.transform.rotation = rotOne;
            }
        }

        bulletHellTimer = 0f;
    }

    private void SetPattern(AttackTypes attackData)
    {
        bulletHellCooldown = attackData.AttackCoolDown;
        emitterAngle = attackData.EmitterAngle;
        projectileAngle = attackData.ProjectileAngle;
        projectileAmount = attackData.Projectiles;
    }
}
