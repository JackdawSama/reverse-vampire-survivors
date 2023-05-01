using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeroBullet : MonoBehaviour
{
    [Header("Variables")]
    public float baseMoveSpeed;
    float moveSpeed;
    public float baseDeltaInc, baseGammaInc;
    float deltaInc, gammaInc;
    public int baseDamage;
    public int maxDamage;
    public float deathTimer = 6f;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = baseMoveSpeed;
        deltaInc = baseDeltaInc;
        gammaInc = baseGammaInc;
        StartCoroutine(BufferDeath());
    }

    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        deltaInc = deltaInc * gammaInc;
        moveSpeed += deltaInc;
    }

    void Move()
    {
        transform.position += transform.up * moveSpeed * Time.fixedDeltaTime; 
    }

    IEnumerator BufferDeath()
    {
        yield return new WaitForSecondsRealtime(deathTimer);
        // Destroy(gameObject);
        if(gameObject.name == "Projectile 1(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 1);
        }
        else if(gameObject.name == "Projectile 2(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 2);
        }
        else if(gameObject.name == "Projectile 5(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 5);
        }
        else if(gameObject.name == "Projectile 6(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 6);
        }
        else if(gameObject.name == "Projectile 7(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 7);
        }
        else if(gameObject.name == "Projectile 8(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 8);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInChildren<TheEnemy>().TakeDamage(Random.Range(baseDamage, maxDamage));
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<ThePlayerController>().TakeDamage(Random.Range(baseDamage, maxDamage));
            
            if(gameObject.name == "Projectile 1(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 1);
            }
            else if(gameObject.name == "Projectile 2(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 2);
            }
            else if(gameObject.name == "Projectile 5(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 5);
            }
            else if(gameObject.name == "Projectile 6(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 6);
            }
            else if(gameObject.name == "Projectile 7(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 7);
            }
            else if(gameObject.name == "Projectile 8(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 8);
            }
            else
            {
                Destroy(gameObject);
            }
        }      
    }

    void OnEnable()
    {
        StartCoroutine(BufferDeath());
    }

    void OnDisable()
    {
        moveSpeed = baseMoveSpeed;
        deltaInc = baseDeltaInc;
        gammaInc = baseGammaInc;
        StopCoroutine(BufferDeath());
    }
}
