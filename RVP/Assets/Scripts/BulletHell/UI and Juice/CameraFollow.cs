using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow
    public float smoothing = 5f; // The smoothing factor for the camera movement

    private Vector3 offset; // The offset between the camera and the target

    public ThePlayerController player;

    void Start()
    {
        // Calculate the offset between the camera and the target
        offset = transform.position - target.position;
    }

    void Update()
    {
        if(player)
        {
            transform.eulerAngles = new Vector3(0, 0, player.cameraAngle);
        }
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            // Calculate the target position for the camera
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.fixedDeltaTime);
        }

        // if(!player)
        // {
        //     transform.eulerAngles = new Vector3(0, 0, player.cameraAngle - 90);
        // }
    }

}
