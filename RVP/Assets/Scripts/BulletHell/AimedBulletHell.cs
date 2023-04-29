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
        Inactive,
        ModeOne,
        ModeTwo,
        ModeThree,
        ModeFour,
        Chaos,
        Chaos2
    }

    [Header("References")]
    public TheHero hero;
    public Transform target;

    [Header("Timers")]
    [SerializeField] float bulletHellTimer; 
    [SerializeField] float bulletHellCooldown;
    [SerializeField] float modeTimer;
    [SerializeField] float modeCooldown = 6f;

    [Header("Emitter Data")]
    public Transform[] emitters;
    public GameObject[] projectilePrefab;
    public List<AttackTypes> attackTypes;
    float emitterAngle, projectileAngle, projectileAmount;

    // Start is called before the first frame update
    void Start()
    {
        hero = GetComponent<TheHero>();

        bulletHell = AimedBulletHellSys.Chaos;
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

            case AimedBulletHellSys.Chaos:
            {
                bulletHellRefState = AimedBulletHellSys.Chaos;
                SetPattern(attackTypes[0]);

                Chaos();

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
            Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation).transform;
            Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle * i));
            bulletOne.transform.rotation = rotOne;
    
            }

            if (i == x)
            {
                // reset
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation).transform;
                Quaternion rotOne = Quaternion.Euler(0, 0, emitters[0].eulerAngles.z + (projectileAngle));
                bulletOne.transform.rotation = rotOne;
            }

            if (i > x)
            {
                Transform bulletOne = Instantiate(projectilePrefab[0], emitters[0].position, emitters[0].rotation).transform;
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
