using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : MonoBehaviour
{
    //Minion References
    AvatarScript avatarRef;
    MinionClass minion;
    //Minion References end

    //Minion Variables
    public int initHealth;
    public int initDamage;
    public int initExp;
    public int movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //avatar = GameObject.Find("Avatar").GetComponent<AvatarScript>();
        minion = new MinionClass(avatarRef.currentLevel, initHealth, initDamage, initExp);
        minion.InitStats();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Die()
    {
        //Minion death
        //Set minion to inactive
        //Add minion exp to player exp
        //reset minion stats
    }

    public void TakeDamage()
    {
        //Minion takes damage
        //Check if minion is dead
        //If minion is dead, call Die()
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, avatarRef.transform.position, movementSpeed * Time.deltaTime);
    }
}
