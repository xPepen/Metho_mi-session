using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public class Idle_Enemy : BaseState<Enemy>
    {
        public Idle_Enemy(Enemy _controlledEntity) : base(_controlledEntity)
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
        }
    }
    public class Move_Enemy : BaseState<Enemy>
    {
        public Move_Enemy(Enemy _controlledEntity) : base(_controlledEntity)
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
        }
    }
    public class Attack_Enemy : BaseState<Enemy>
    {
        public Attack_Enemy(Enemy _controlledEntity) : base(_controlledEntity)
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
        }
    }
    public class Dead_Enemy : BaseState<Enemy>
    {
        public Dead_Enemy(Enemy _controlledEntity) : base(_controlledEntity)
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
        }
    }

}
