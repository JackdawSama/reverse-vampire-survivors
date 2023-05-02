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
        AttackTwo,
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
    public float bulletHellTimer; 
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
        bulletHellTimer = 0;

        bulletHell = AimedBulletHellSys.AttackOne;
        SetPattern(attackTypes[0]);
    }

    // Update is called once per frame
    void Update()
    {
        bulletHellTimer += Time.deltaTime;
        
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

                Aimed();

                break;
            }

            case AimedBulletHellSys.AttackTwo:
            {
                bulletHellRefState = AimedBulletHellSys.AttackTwo;
                SetPattern(attackTypes[1]);

                Aimed("Speed");

                break;
            }

            case AimedBulletHellSys.QuadOne:
            {
                emitters[0].rotation = Quaternion.Euler(0, 0, 0);
                bulletHellRefState = AimedBulletHellSys.QuadOne;
                SetPattern(attackTypes[2]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadTwo:
            {
                emitters[0].rotation = Quaternion.Euler(0, 0, 0);
                bulletHellRefState = AimedBulletHellSys.QuadTwo;
                SetPattern(attackTypes[3]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadThree:
            {
                emitters[0].rotation = Quaternion.Euler(0, 0, 0);
                bulletHellRefState = AimedBulletHellSys.QuadThree;
                SetPattern(attackTypes[4]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadFour:
            {
                emitters[0].rotation = Quaternion.Euler(0, 0, 0);
                bulletHellRefState = AimedBulletHellSys.QuadFour;
                SetPattern(attackTypes[5]);

                QuadSingle();

                break;
            }

            case AimedBulletHellSys.QuadOneThree:
            {
                emitters[0].rotation = Quaternion.Euler(0, 0, 0);
                bulletHellRefState = AimedBulletHellSys.QuadOneThree;
                SetPattern(attackTypes[3]);
                QuadDouble();

                break;
            }

            case AimedBulletHellSys.QuadTwoFour:
            {
                emitters[0].rotation = Quaternion.Euler(0, 0, 0);
                bulletHellRefState = AimedBulletHellSys.QuadTwoFour;
                SetPattern(attackTypes[4]);
                QuadDouble();

                break;
            }

            default:
                break;
        }
    }

    private void Aimed()
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
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }
                
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }
                
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z - (projectileAngle * (i - projectileAmount/2)));
                bulletOne.transform.rotation = rotOne;
            }
        }

        bulletHellTimer = 0f;
    }

    private void Aimed(string letters)
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
                // Transform bulletOne = Instantiate(projectilePrefab[1], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(8);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
            }

            if (i == x)
            {
                // reset
                // Transform bulletOne = Instantiate(projectilePrefab[1], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(8);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                // Transform bulletOne = Instantiate(projectilePrefab[1], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(8);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
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
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
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
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
            }
        }

        emitters[0].rotation = Quaternion.Euler(emitters[0].eulerAngles.x, emitters[0].eulerAngles.y, (emitterAngle + 180f));

        x = (int) projectileAmount/2;
        for(int i = 0; i < projectileAmount; i++)
        {
            if (i<x)
            {
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                // Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation, hero.transform).transform;
                
                GameObject bulletOne = PoolingManager.Instance.GetProjectile(7);
                if(bulletOne != null)
                {
                bulletOne.transform.position = emitters[0].position;
                bulletOne.transform.rotation = emitters[0].rotation;
                }

                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
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
