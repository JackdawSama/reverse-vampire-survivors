using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Type", menuName = "Combat/Attack Type")]

public class AttackTypes : ScriptableObject
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackAngle;
    [SerializeField] private float attackAngleChange;
    [SerializeField] private int projectiles;

    public float AttackCoolDown => attackCooldown;
    public float AttackAngle => attackAngle;
    public float AttackAngleChange => attackAngleChange;
    public float Projectiles => projectiles; 
}
