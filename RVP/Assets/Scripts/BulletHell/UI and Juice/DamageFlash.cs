using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] Material flashMat;
    [SerializeField] float flashDuration, gamma;
    [SerializeField] SpriteRenderer rend;
    Material originalMat;
    Coroutine flashRoutine;
    [SerializeField] Image flash; 
    private void Start() 
    {
        rend = GetComponent<SpriteRenderer>();
        originalMat = rend.material;    
    }
    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    void FixedUpdate()
    {
        if (flash)
        {
            if (flash.color.a > 0)
            {
                flash.color = new Color(flash.color.r,flash.color.g,flash.color.b, flash.color.a - gamma);
            }
        }
    }

    IEnumerator FlashRoutine()
    {
        
        if (flash)
        {
            flash.color = new Color(flash.color.r,flash.color.g,flash.color.b, 1);
        }

        rend.material = flashMat;
        yield return new WaitForSeconds(flashDuration);
        rend.material = originalMat;

        flashRoutine = null;
    }
}
