using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemy : Enemy
{
    public StateMachine<BasicMeleeEnemy> EnemyStateMachine { get; protected set; }
    public StateContainerMeleePig StateContainer { get; protected set; }
    
    protected override void Init()
    {
        base.Init();
        base.poolRef = EnemyFactoryPig.Instance.Pool.Pool;
        StateContainer = new StateContainerMeleePig(this);
        EnemyStateMachine = new StateMachine<BasicMeleeEnemy>(StateContainer.Idle);
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
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
