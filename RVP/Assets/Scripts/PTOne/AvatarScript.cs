using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : MonoBehaviour
{
    //Avatar References
    AvatarClass avatar;
    Grid grid;
    //Avatar References end

    [Header("INIT Variables")]
    //Avatar Init Variables
    [SerializeField]int startLevel;
    [SerializeField]int startHP;
    [SerializeField]int startDamage;
    [SerializeField]int startAttackSpeed;
    [SerializeField]int corruptionThreshold;
    [SerializeField]float movementSpeed;
    //Avatar Init Variables end


    // Start is called before the first frame update
    void Start()
    {
        avatar = new AvatarClass(true, startLevel, startHP, startDamage, startAttackSpeed, corruptionThreshold);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Attack()
    {

    }
}
