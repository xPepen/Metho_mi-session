using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateContainerBasicMelee 
{
    public Idle_MeleeEnemy Idle { get; protected set; }
    public Chasse_MeleeEnemy Chasse { get; protected set; }
    public Attack_MeleeEnemy Attack { get; protected set; }
    public Dead_MeleeEnemy Dead { get; protected set; }
    public StateContainerBasicMelee(BasicMeleeEnemy _ref) 
    {
        Idle = new Idle_MeleeEnemy(_ref);
        Chasse = new Chasse_MeleeEnemy(_ref);
        Attack = new Attack_MeleeEnemy(_ref);
        Dead = new Dead_MeleeEnemy(_ref);
    }
}

public class Idle_MeleeEnemy : BaseState<BasicMeleeEnemy>
{
    private float timer = 0.5f;
    private float currentWait;
        public Idle_MeleeEnemy(BasicMeleeEnemy _controlledEntity) : base(_controlledEntity)
        {

        }

        public override void OnEnterState()
        {
            currentWait = 0;
        }

        public override void OnExitState()
        {
        }

        public override void OnUpdateState()
        {
            currentWait += Time.deltaTime;
            if(currentEntity.CanSeePlayer && currentWait >= timer)
            {
                currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainerBasic.Chasse);
            }
        }
    }
    public class Chasse_MeleeEnemy : BaseState<BasicMeleeEnemy>
    {
        public Chasse_MeleeEnemy(BasicMeleeEnemy _controlledEntity) : base(_controlledEntity)
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
                currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainerBasic.Attack);
             }
        }
    }
    public class Attack_MeleeEnemy : BaseState<BasicMeleeEnemy>
    {
        public Attack_MeleeEnemy(BasicMeleeEnemy _controlledEntity) : base(_controlledEntity)
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
            currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainerBasic.Chasse);
            }
        }
    }

    public class Dead_MeleeEnemy : BaseState<BasicMeleeEnemy>
    {
        public Dead_MeleeEnemy(BasicMeleeEnemy _controlledEntity) : base(_controlledEntity)
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
                currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainerBasic.Idle);
            }
        }
    }


