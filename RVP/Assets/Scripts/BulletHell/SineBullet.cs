using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed;
    public float deltaInc, gammaInc;
    public int baseDamage;
    public int maxDamage;
    public float deathTimer;

    public bool useSine;
    public float amplitude;
    public float frequency;
    public float waveVal;
    // Start is called before the first frame update
    void Start()
    {
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
        Destroy(gameObject);
    }

    // private void OnCollisionEnter2D(Collision2D other) 
    // {
    //     float damage = Random.Range(baseDamage, maxDamage);

    //     if(other.gameObject.CompareTag("Player"))
    //     {
    //         other.gameObject.GetComponent<TheHero>().TakeDamage(damage);
    //         Destroy(gameObject);
    //     }
    // }

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
            Destroy(gameObject);
        }      
    }
}
