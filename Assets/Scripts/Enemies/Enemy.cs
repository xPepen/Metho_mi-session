using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : LivingEntity
{
    protected Player m_playerRef;
    public StateMachine<Enemy> EnemyStateMachine { get; protected set; }

    [Header("___Attack___")]
    [SerializeField]protected Weapon m_Weapon;
    [SerializeField] protected float attackRange;
    [SerializeField] protected bool IsMelee;
    protected bool CanAttack => Vector3.Distance(transform.position ,m_playerRef.transform.position) <= attackRange;

    protected override void OnStart()
    {
        base.OnStart();
        m_playerRef = Player.Instance;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        base.Move(Direction);
    }
    public override void OnDead()
    {
        base.OnDead();
        //drop xp if lucky
        //reference to pool and add himself to the pool
    }
    protected abstract void OnAttack();
    protected Vector2 Direction => ( m_playerRef.transform.position - transform.position).normalized;
}
