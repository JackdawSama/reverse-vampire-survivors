using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheDamageText : MonoBehaviour
{
   [SerializeField] private float destroyTime;
   [SerializeField] private Vector3 offset;
   [SerializeField] public Color damageColour;

   [SerializeField]private TextMeshPro damageText;

   private void Start()
   {
        damageText = GetComponent<TextMeshPro>();
        transform.localPosition += offset;
        Destroy(gameObject, destroyTime);
   }

   public void Initialise(float damage)
   {
        damageText.text = damage.ToString();
   }
}
