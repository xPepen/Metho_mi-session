using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : BaseEntity
{
    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxHP;
    [SerializeField] protected float speed;
    [SerializeField] protected PhysicEntityInfo EntityStats;

    protected bool IsDead => currentHP <= 0;
    protected Rigidbody2D m_rb;
    protected virtual void Init()
    {
        maxHP = EntityStats.maxHP;
        currentHP = maxHP;

        m_rb = GetComponent<Rigidbody2D>();
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        Init();
    }
}
