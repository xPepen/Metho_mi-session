using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Enemy : LivingEntity
{
   
    public GameplayManager m_gameplayManager { get; private set; }
    //---------States
    
    //---------End States
    [field: SerializeField] public EnemyStatsInfo EnemyInfo { get; private set; }
    [SerializeField] protected float m_seePlayerDistance = 25f;
    
    [Header("___Attack___")]
    [SerializeField]protected Weapon m_Weapon;
    [SerializeField] protected float attackRange;
    [SerializeField] protected bool IsMelee;
    [field: SerializeField] protected PoolPatern<Enemy> poolRef; // must be initialise by children
    public bool CanAttack => Vector3.Distance(transform.position , m_gameplayManager.m_playerRef.transform.position) <= attackRange;
    public Vector2 Direction => (m_gameplayManager.m_playerRef.transform.position - transform.position).normalized;

    public bool CanSeePlayer => Vector3.Distance(transform.position, m_gameplayManager.m_playerRef.transform.position) < m_seePlayerDistance;
    protected override void Init()
    {
        base.Init();
        //Init Stats
        maxHP = EnemyInfo.MaxHP;
        currentHP = maxHP;
        speed = EnemyInfo.moveSpeed;
        //Init manager
        m_gameplayManager = GameplayManager.Instance;
        //States


    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        //base.Move(Direction);
    }
    public override void OnDead()
    {
        poolRef.ReAddItem(this);
        Heal();
        if (Random.Range(0, 10 + 1) > 6)
        {
            var _Entity = m_gameplayManager.ExperiencePool.Pool.GetNextItem();
            _Entity.transform.position = transform.position;
        }
       
    }
    public abstract void OnAttack();
}
