using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemyController : MonoBehaviour
{
    Vector2 lookDir;
    Vector2 mousePos;

    public Camera cam;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

}
