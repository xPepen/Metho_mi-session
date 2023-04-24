using System;
using UnityEngine;

public abstract class Enemy : LivingEntity,IPooler <Enemy>
{
   
    public GameplayManager m_gameplayManager { get; private set; }
    protected Player m_playerRef;
    
    [field: SerializeField] public EnemyStatsInfo EnemyInfo { get; private set; }
    [SerializeField] protected float m_seePlayerDistance = 25f;
    
    [Header("___Attack___")]
    [SerializeField]protected Weapon m_Weapon;
    [SerializeField] protected float attackRange;

    public Action<Enemy> RePoolItem { get; set; }
    public bool CanAttack => Vector3.Distance(transform.position ,m_playerRef.transform.position) <= attackRange;
    public Vector2 Direction => (m_playerRef.transform.position - transform.position).normalized;

    public bool CanSeePlayer => Vector3.Distance(transform.position, m_playerRef.transform.position) < m_seePlayerDistance;

    protected override void Init()
    {
        base.Init();
        //Init Stats
        maxHP = EnemyInfo.MaxHP;
        currentHP = maxHP;
        speed = EnemyInfo.moveSpeed;
        //Init manager
        m_gameplayManager = D.Get<GameplayManager>();
        //m_playerRef = D.Get<Player>();
         m_playerRef = Player.Instance;
    }
    public override void OnDead()
    {
        RePoolItem.Invoke(this);
        Heal();
        if (UnityEngine.Random.Range(0, 10 + 1) > 8)
        {
            var _Entity = m_gameplayManager.ExperiencePool.Pool.GetNextItem();
            _Entity.transform.position = transform.position;
            m_gameplayManager.ListOfExperience.Add(_Entity);
        }
       
    }
    public abstract void OnAttack();
}
