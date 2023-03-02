using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Spell : Weapon, IUpgradeble
{

    [SerializeField] protected LayerMask m_target;
    [FormerlySerializedAs("HittableTargetCount")]
    [Tooltip("Max enemy that can be hit")]
    [SerializeField] protected int m_HittableTargetCount;
    [SerializeField] protected float SpellDamage;
    [Tooltip("Radius around entity (Rayon)")]
    [SerializeField] protected float SpellRadius;
    [Tooltip("Distance for hit (Diametre)")]
    [SerializeField] protected float Max_Range;
    protected Collider2D[] m_collisionResult;
    protected string m_UpgradeName;
    protected override void OnAwake()
    {
        base.OnAwake();
        m_collisionResult = new Collider2D[this.m_HittableTargetCount];
    }

    public abstract void UpgradeStat(int _value);
    public abstract string UpgradeName();
    public abstract void InitUpgrade();
}
