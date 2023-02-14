using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateContainerMeleePig 
{
    public Idle_PigEnemy Idle { get; protected set; }
    public Chasse_PigEnemy Chasse { get; protected set; }
    public Attack_PigEnemy Attack { get; protected set; }
    public Dead_PigEnemy Dead { get; protected set; }
    public StateContainerMeleePig(MeleePig _ref) 
    {
        Idle = new Idle_PigEnemy(_ref);
        Chasse = new Chasse_PigEnemy(_ref);
        Attack = new Attack_PigEnemy(_ref);
        Dead = new Dead_PigEnemy(_ref);
    }
}

public class Idle_PigEnemy : BaseState<MeleePig>
    {
        public Idle_PigEnemy(MeleePig _controlledEntity) : base(_controlledEntity)
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
            if(currentEntity.CanSeePlayer)
            {
                currentEntity.EnemyStateMachine.SwitchState(currentEntity.StateContainer.Chasse);
            }
        }
    }
    public class Chasse_PigEnemy : BaseState<MeleePig>
    {
        public Chasse_PigEnemy(MeleePig _controlledEntity) : base(_controlledEntity)
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
    public class Attack_PigEnemy : BaseState<MeleePig>
    {
        public Attack_PigEnemy(MeleePig _controlledEntity) : base(_controlledEntity)
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

    public class Dead_PigEnemy : BaseState<MeleePig>
    {
        public Dead_PigEnemy(MeleePig _controlledEntity) : base(_controlledEntity)
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


