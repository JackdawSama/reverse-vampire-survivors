using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Type", menuName = "Combat/Attack Type")]

public class AttackTypes : ScriptableObject
{
    [SerializeField] private float attackCooldown = 1f;
    [Range(-180.0f, 180.0f)] [SerializeField] private float emitterAngle;
    [Range(0.0f, 120.0f)] [SerializeField] private float projectileAngle;
    [SerializeField] private int projectiles = 1;

    public float AttackCoolDown => attackCooldown;
    public float EmitterAngle => emitterAngle;
    public float ProjectileAngle => projectileAngle;
    public float Projectiles => projectiles;
}
