using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Enemy : LivingEntity
{
    protected Player m_playerRef;
    public StateMachine<Enemy> EnemyStateMachine { get; protected set; }
    [field: SerializeField] public EnemyStatsInfo EnemyInfo { get; private set; }
    [Header("___Attack___")]
    [SerializeField]protected Weapon m_Weapon;
    [SerializeField] protected float attackRange;
    [SerializeField] protected bool IsMelee;
    [field: SerializeField] protected PoolPatern<Enemy> poolRef; // must be initialise by children
    protected bool CanAttack => Vector3.Distance(transform.position ,m_playerRef.transform.position) <= attackRange;


    protected override void OnStart()
    {
        base.OnStart();
        m_playerRef = Player.Instance;
        maxHP = EnemyInfo.MaxHP;
        currentHP = maxHP;
        speed = EnemyInfo.moveSpeed;

    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        base.Move(Direction);
    }
    public override void OnDead()
    {
        poolRef.ReAddItem(this);
        //var _isDropXP = Random.Range(0, 10) >;
        //drop xp if lucky
        //reference to pool and add himself to the pool
    }
    protected abstract void OnAttack();
    protected Vector2 Direction => ( m_playerRef.transform.position - transform.position).normalized;
}
