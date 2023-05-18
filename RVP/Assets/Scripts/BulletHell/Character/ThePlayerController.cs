using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DamageFlash))]

public class ThePlayerController : MonoBehaviour
{
    [Header("Cursor Variables")]
    public float mouseX;
    public float mouseSens = 100f;

    [Header("Player Variables")]
    public int maxHealth = 5;
    public float currentHealth;

    public int healthCounter;
    public float moveSpeed = 2f, attackMoveMod, idleMoveMod;
    float moveMod = 1;
    public float  cameraAngle;
    public float angle;
    public Vector2 center;
    public float radius;

    public int distanceTravelled;

    [Header("Units Imbue")]
    public Collider2D[] unitsList;
    public float imbueRadius;
    public LayerMask imbueLayer;

    [Header("Player Checks")]
    public bool isInvincible;
    public bool isActive;

    [Header("Attack Cooldown")]
    public float attackTimer = 0;
    public float attackCooldown = 0.2f;

    [Header("i-Frames Data")]
    public float iDuration;
    public float iDeltaTime;

    [Header("Controls")]
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public Vector2 lookDir;

    [Header("Player References")]
    public TheHero hero;
    public Transform bulletSpawn;
    public GameObject projectilePrefab;
    public GameObject hitBox;
    public SpriteRenderer rend;
    public DamageFlash damageFeedback;
    public ParticleSystem particle;
    public AudioSource audioSource;
    public AudioClip attackSound;

    void Start()
    {
        center = hero.transform.position;
        rend = GetComponent<SpriteRenderer>();

        damageFeedback = GetComponent<DamageFlash>();

        particle = GetComponent<ParticleSystem>();

        audioSource = GetComponent<AudioSource>();

        healthCounter = maxHealth;

        moveMod = idleMoveMod;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isActive = true;
    }

    void Update()
    {
        MovePlayer();
        MouseLook();

        attackTimer += Time.deltaTime;

        if(Input.GetKey(KeyCode.Z) && attackTimer > attackCooldown)
        {
            Attack();

            // audioSource.PlayOneShot(attackSound);

            attackTimer = 0;
        }

        if(Input.GetKey(KeyCode.Z))
        {
            moveMod = attackMoveMod;
        } else
        {
            moveMod = idleMoveMod;
        }
        

        if(Input.GetKeyDown(KeyCode.X))
        {
            //Imbues Units
            ImbueUnits();
            particle.Emit(1);
        }
    }

    void MovePlayer()
    {
        if(hero)
        {
            center = hero.transform.position;
        }

        mouseX = Input.GetAxisRaw("Mouse X") * mouseSens;

        // if(!isActive)
        // {
        //     return;         //turns off controls if player is dead
        // }

        if(Input.GetAxisRaw("Mouse X") > 0)
        {
            angle += moveSpeed * Time.deltaTime * moveMod * mouseX;

            distanceTravelled = distanceTravelled + Mathf.FloorToInt(Mathf.Abs(angle));
        }
        else if(Input.GetAxisRaw("Mouse X") < 0)
        {
            angle += moveSpeed * Time.deltaTime * moveMod  * mouseX;

            distanceTravelled = distanceTravelled + Mathf.FloorToInt(Mathf.Abs(angle));
        }


        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * -radius;
        transform.position = center + offset;
    }

    void MouseLook()
    {
        if(!hero)
        {
            return;
        }
        lookDir = (hero.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        bulletSpawn.transform.eulerAngles = new Vector3(0, 0, angle);

        cameraAngle = angle;
    }

    private void Attack()
    {
        Vector2 attackRef = bulletSpawn.position;
        GameObject bullet = Instantiate(projectilePrefab, attackRef, bulletSpawn.rotation);
        bullet.GetComponent<TheEnemyBullet>().SetReference(gameObject.GetComponent<ThePlayerController>());
    }

    private void ImbueUnits()
    {
        //gets an array of Units and Imbues them shields damage 
        unitsList = Physics2D.OverlapCircleAll(transform.position, imbueRadius, imbueLayer);

        foreach(Collider2D unit in unitsList)
        {
            unit.GetComponent<TheEnemy>().isImbued = true;                  //sets imbue to true which deals damage to shields
            unit.GetComponent<TheEnemy>().Imbued();
        }
    }

    public int CalculateDistanceTravelled()
    {
        return distanceTravelled/100;
    }

    public void TakeDamage(float damage)
    {
        if(isInvincible)
        {
            //if iFrames active doesn't take damage
            return;
        }

        healthCounter--;

        if(healthCounter <= 0)
        {
            healthCounter = 0;
            Die();
            return;
        }

        StartCoroutine(IFrame());                       //Coroutine for i-Frames
    }

    private void Die()
    {
        //Destroy(gameObject);

        //TODO: Set isAlive to false and switch sprite to broken Moon

        if(isActive)
        {
            isActive = false;
            damageFeedback.enabled = false;
            hitBox.SetActive(false);


            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator IFrame()
    {
        //coroutine for running an iFrame for set duration
        isInvincible = true;
        for(float i = 0; i < iDuration; i++)
        {
            // Damage Blink Function Call
            damageFeedback.Flash();
            yield return new WaitForSeconds(iDeltaTime);
        }

        isInvincible = false;
    }

    private IEnumerator FadeOut()
    {
        if(!isActive && !damageFeedback.enabled)
        {
            while(rend.color.a > 0)
            {
                Color newColor = rend.color;
                newColor.a -= Time.deltaTime;
                rend.color = newColor;
            }

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene("Highscore");

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(center, transform.position);
        Gizmos.DrawWireSphere(transform.position, imbueRadius);
    }
}
