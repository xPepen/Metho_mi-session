using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateContainer 
{
    public Idle_BossTree Idle { get; protected set; }
    public Chasse_BossTree Chasse { get; protected set; }
    public Attack_BossTree Attack { get; protected set; }
    public Dead_BossTree Dead { get; protected set; }
    public BossStateContainer(BossTree _ref) 
    {
        Idle = new Idle_BossTree(_ref);
        Chasse = new Chasse_BossTree(_ref);
        Attack = new Attack_BossTree(_ref);
        Dead = new Dead_BossTree(_ref);
    }
}
public class Idle_BossTree : BaseState<BossTree>
{
    private float timer = 1.25f;
    private float currentWait;
        public Idle_BossTree(BossTree _controlledEntity) : base(_controlledEntity)
        {

        }

        public override void OnEnterState()
        {
            currentWait = 0;
            currentEntity.InitBabyTree();

        }

        public override void OnExitState()
        {
        }

        public override void OnUpdateState()
        {
            currentWait += Time.deltaTime;
            if(currentEntity.CanSeePlayer && currentWait >= timer)
            {

                 currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainer.Chasse);
            }
        }
    }
    public class Chasse_BossTree : BaseState<BossTree>
    {
        public Chasse_BossTree(BossTree _controlledEntity) : base(_controlledEntity)
        {
        }

        public override void OnEnterState()
        {
        }

        public override void OnExitState()
        {
        }

        public override void OnUpdateState()
        {
            currentEntity.Move(currentEntity.Direction);

             if (currentEntity.CanAttack)
             {
                 currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainer.Attack);
             }
        }
    }
    public class Attack_BossTree : BaseState<BossTree>
    {
        public Attack_BossTree(BossTree _controlledEntity) : base(_controlledEntity)
        {
        }
        public override void OnEnterState()
        {
        }
        public override void OnExitState()
        {
        }
        public override void OnUpdateState()
        {
            currentEntity.Move(Vector2.zero);
            currentEntity.OnAttack();

            if (!currentEntity.CanAttack)
            {
              currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainer.Chasse);
            }
        }
    }

    public class Dead_BossTree : BaseState<BossTree>
    {
        public Dead_BossTree(BossTree _controlledEntity) : base(_controlledEntity)
        {
        }

        public override void OnEnterState()
        {
        }

        public override void OnExitState()
        {
        }

        public override void OnUpdateState()
        {
            if(currentEntity.currentHP > 0)
            {
                currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainer.Idle);
            }
        }
    }




