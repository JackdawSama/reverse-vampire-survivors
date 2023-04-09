using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThePlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed = 2f;
    public float angle;
    public Vector2 center;
    public float radius;

    [Header("Controls")]
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public Vector2 lookDir;
    public Vector2 mousePos;

    [Header("Player References")]
    public TheHero hero;
    public Camera cam;
    public Transform bulletSpawn;
    public GameObject projectilePrefab;
    public GameObject damageTextPrefab;

    void Start()
    {
        center = hero.transform.position;
    }

    void Update()
    {
        MovePlayer();
        MouseLook();

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }


    void MovePlayer()
    {
        center = hero.transform.position;

        if(Input.GetKey(moveLeft))
        {
            angle += moveSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(moveRight))
        {
            angle += -moveSpeed * Time.deltaTime;
        }


        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * -radius;
        transform.position = center + offset;
    }

    void MouseLook()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = (mousePos - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        bulletSpawn.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Attack()
    {
        Vector2 attackRef = bulletSpawn.position;
        GameObject bullet = Instantiate(projectilePrefab, attackRef, bulletSpawn.rotation);
        bullet.GetComponent<TheEnemyBullet>().SetReference(gameObject.GetComponent<ThePlayerController>());
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<TheDamageText>().Initialise(damage);;
        // obj.GetComponent<TheDamageText>().Initialise(damage);
        // obj.GetComponent<TextMeshPro>().color = obj.GetComponent<TheDamageText>().damageColour;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Unit Died");
            Die();
        }
    }

    private void Die()
    {
        //maybe switch off sprite and set a bool to dead later?
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(center, 0.1f);
        Gizmos.DrawLine(center, transform.position);
    }
}
