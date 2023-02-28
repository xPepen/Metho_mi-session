using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : Weapon
{

    [SerializeField] protected LayerMask m_target;
    [Tooltip("Max enemy that can be hit")]
    [SerializeField] protected int HittableTargetCount;
    [SerializeField] protected float SpellDamage;
    [Tooltip("Radius around entity (Rayon)")]
    [SerializeField] protected float SpellRadius;
    [Tooltip("Distance for hit (Diametre)")]
    [SerializeField] protected float Max_Range;
    protected Collider2D[] m_collisionResult;
    protected override void OnAwake()
    {
        base.OnAwake();
        m_collisionResult = new Collider2D[this.HittableTargetCount];
    }
}
