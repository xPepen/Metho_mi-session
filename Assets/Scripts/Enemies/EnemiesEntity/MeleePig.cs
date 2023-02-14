using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePig : Enemy
{
    public StateMachine<MeleePig> EnemyStateMachine { get; protected set; }
    public StateContainerMeleePig StateContainer { get; private set; }
    
    protected override void Init()
    {
        base.Init();
        base.poolRef = EnemyFactoryPig.Instance.Pool.Pool;
        StateContainer = new StateContainerMeleePig(this);
        EnemyStateMachine = new StateMachine<MeleePig>(StateContainer.Idle);
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        EnemyStateMachine.OnUpdate();
    }
    public override void OnAttack()
    {
       
        if (!CanAttack)
        {
            return;
        }
        if (m_Weapon)
        {
            m_Weapon.Attack(Vector2.zero);
        }
    }
    

   
}
