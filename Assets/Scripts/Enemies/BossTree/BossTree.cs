using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTree : Enemy
{
    public StateMachine<BossTree> EnemyStateMachine { get; protected set; }
    public BossStateContainer StateContainer { get; private set; }
    private const float m_maxShield = 100;
    private  float m_OnHitShield ;
    private float m_currentShield;
    public List<BabyTree> _babyTrees;
    protected override void Init()
    {
        base.Init();
        base.poolRef = TreeFactory.Instance.Pool.Pool;
        m_currentShield = m_maxShield;
    }

    protected override void OnStart()
    {
        base.OnStart();
        StateContainer = new BossStateContainer(this);

        EnemyStateMachine = new StateMachine<BossTree>(StateContainer.Idle);
        m_OnHitShield =  m_maxShield / _babyTrees.Count;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        EnemyStateMachine.OnUpdate();
       
    }
    /*public void InitBabyTree()
    {
        var _rand = Random.Range(2, 5 + 1);
        BabyTreeFactory _babyTree = BabyTreeFactory.Instance;
            m_OnHitShield = m_maxShield / _rand;
            print(_rand);
            int i = 0;
            while (i < _rand)
            {
                print(i);
                var _copy = _babyTree.CreateEnemy2(this);
                print(_copy + " number " + i);
                _copy.transform.position = transform.position + (Vector3)Random.insideUnitCircle.normalized * 5f;
                i++;
            }
    }*/
    public void InitBabyTree()
    {
        
        BabyTreeFactory _babyTree = BabyTreeFactory.Instance;
            for(int i= 0; i < _babyTrees.Count; i ++)
            {
                var _copy = _babyTrees[i];
                _copy.Original = this;
                _copy.gameObject.SetActive(true);
                _copy.transform.position = transform.position + (Vector3)Random.insideUnitCircle.normalized * 3f;
            }
    }
    public override void OnAttack()
    {
        if (base.m_Weapon)
        {
                 (m_Weapon as IShootable).Attack(Player.Instance.MyWeapon.transform.position);
        }
    }

    public void HitShield()
    {
        print((m_currentShield));
        if (this.m_currentShield < 0)
        {
            return;
        }
        this.m_currentShield -= m_OnHitShield;
    }

    public override void OnHit(float _damage)
    {
        if (this.m_currentShield > 0)
        {
            return;
        }
        base.OnHit(_damage); 
      
    }

    public override void OnDead()
    {
        base.OnDead();
        m_currentShield = m_maxShield;
        EnemyStateMachine.SwitchState(StateContainer.Idle);
    }
    
    
}