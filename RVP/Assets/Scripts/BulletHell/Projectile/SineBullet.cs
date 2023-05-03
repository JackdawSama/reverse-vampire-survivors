using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : MonoBehaviour
{
    [Header("Variables")]
    public float baseMoveSpeed;
    float moveSpeed;
    public float baseDeltaInc, baseGammaInc;
    float deltaInc, gammaInc;
    public int baseDamage;
    public int maxDamage;
    public float deathTimer;

    public bool useSine;
    public float amplitude;
    public float frequency;
    public float waveVal;

    TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();

        moveSpeed = baseMoveSpeed;
        deltaInc = baseDeltaInc;
        gammaInc = baseGammaInc;
        StartCoroutine(BufferDeath());
    }

    // Update is called once per frame
    void Update()
    {
        if(useSine)
        {
            waveVal = Mathf.Sin(Time.time * frequency);
        }
        else if(!useSine)
        {
            waveVal = Mathf.Cos(Time.time * frequency);
        }
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
        transform.localPosition += transform.right * waveVal * amplitude;
    }

    IEnumerator BufferDeath()
    {
        yield return new WaitForSecondsRealtime(deathTimer);

        if(gameObject.name == "Projectile 3 - Cos(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 3);
        }
        else if(gameObject.name == "Projectile 4 - Sine(Clone)")
        {
            PoolingManager.Instance.ReturnProjectile(gameObject, 4);
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
            //Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<ThePlayerController>().TakeDamage(Random.Range(baseDamage, maxDamage));
            if(gameObject.name == "Projectile 3 - Cos(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 3);
            }
            else if(gameObject.name == "Projectile 4 - Sine(Clone)")
            {
                PoolingManager.Instance.ReturnProjectile(gameObject, 4);
            }
            else
            {
                Destroy(gameObject);
            }
        }      
    }

    void OnEnable()
    {
        // trail.enabled = true;

        StartCoroutine(BufferDeath());
    }

    void OnDisable()
    {
        // trail.enabled = false;

        moveSpeed = baseMoveSpeed;
        deltaInc = baseDeltaInc;
        gammaInc = baseGammaInc;
        
        StopCoroutine(BufferDeath());
    }
}
