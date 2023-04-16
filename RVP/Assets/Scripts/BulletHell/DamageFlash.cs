using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] Material flashMat;
    [SerializeField] float flashDuration;
    [SerializeField] SpriteRenderer rend;
    Material originalMat;
    Coroutine flashRoutine;

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

    IEnumerator FlashRoutine()
    {
        rend.material = flashMat;
        yield return new WaitForSeconds(flashDuration);
        rend.material = originalMat;

        flashRoutine = null;
    }
}
