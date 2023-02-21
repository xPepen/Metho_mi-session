using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTree : Enemy
{
    
    public StateMachine<BossTree> EnemyStateMachine { get; protected set; }
    public BossStateContainer StateContainer { get; private set; }
    
    [SerializeField]private float m_maxShield;
    private float m_currentShield;
    protected override void Init()
    {
        base.Init();
        //need a boss pool
        base.poolRef = EnemyFactoryPig.Instance.Pool.Pool;
        m_currentShield = m_maxShield;
        StateContainer = new BossStateContainer(this);
        // EnemyStateMachine = new StateMachine<BossTree>();
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        EnemyStateMachine.OnUpdate();
    }
    public override void OnAttack()
    {
        //shoot multiple projectile
    }

    public void HitShield(float _dmgOnShield)
    {
        if (this.m_currentShield < 0)
        {
            return;
        }
        this.m_currentShield -= _dmgOnShield;
    }

    public override void OnHit(float _damage)
    {
        //not calling base cause boss cannot be hit if he have shield
        //base.OnHit(_damage); 
        if (this.m_currentShield > 0)
        {
            return;
        }
        this.currentHP -= _damage;
        /*if (_HitEffect)
        {
            //add pool
            //place effect 
            //play
        }*/
        if (IsDead)
        {
            OnDead();
        }
    }

    public override void OnDead()
    {
        base.OnDead();
        //EnemyStateMachine.SwitchState(idle);
    }
    
    
}
