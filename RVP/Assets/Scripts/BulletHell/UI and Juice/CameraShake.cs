using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public bool shaketrue = false;

    Vector3 originalPos;
    float originalShakeDuration; //<--add this

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        originalShakeDuration = shakeDuration; //<--add this
    }

    void Update()
    {
        if (shaketrue)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition,originalPos + Random.insideUnitSphere * shakeAmount,Time.deltaTime * 3);

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = originalShakeDuration; //<--add this
                camTransform.localPosition = originalPos;
                shaketrue = false;
            }
        }
    }

    public void ShakeCamera(float _shakeDuration, float _shakeAmount)
    {
        shaketrue = true;
        shakeDuration = _shakeDuration;
        shakeAmount = _shakeAmount;
    }
}
