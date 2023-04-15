using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Type", menuName = "Combat/Attack Type")]

public class AttackTypes : ScriptableObject
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float emitterAngle;
    [SerializeField] private float projectileAngle;
    [SerializeField] private int projectiles;
    [SerializeField] private GameObject projectile;

    public float AttackCoolDown => attackCooldown;
    public float EmitterAngle => emitterAngle;
    public float ProjectileAngle => projectileAngle;
    public float Projectiles => projectiles;
    public GameObject Projectile => projectile; 
}
