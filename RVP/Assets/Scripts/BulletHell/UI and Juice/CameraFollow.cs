using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // float mouseX, xRot;
    // public float mouseSens = 100f;

    public ThePlayerController player;
    public Transform target; // The target to follow
    private Vector3 offset; // The offset between the camera and the target


    void Start()
    {
        // Calculate the offset between the camera and the target
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if(target)
        {
            // Calculate the target position for the camera
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            transform.position = targetPosition;
            // Vector3.Lerp(transform.position, targetPosition, smoothing * Time.fixedDeltaTime);
        }

        if(player)
        {
            transform.eulerAngles = new Vector3(0, 0, player.cameraAngle);
        }

    }

    // void MouseLook()
    // {
    //     if(!target)
    //     {
    //         return;
    //     }
        
    //     mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
    //     xRot =+ mouseX;


    //     transform.eulerAngles = new Vector3(0, 0, xRot);
    // }

}
