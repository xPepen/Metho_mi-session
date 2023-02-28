using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTree : BasicMeleeEnemy
{
  
    public BossTree Original;
    //public StateMachine<BabyTree> EnemyStateMachine { get; protected set; }
   // public StateContainerMeleePig StateContainer { get; protected set; }
    
    protected override void Init()
    {
      base.Init();
     // base.poolRef = BabyTreeFactory.Instance.Pool.Pool;
     // StateContainer = new StateContainerMeleePig(this);
     // EnemyStateMachine = new StateMachine<BasicMeleeEnemy>(StateContainer.Idle);
    }
    protected override void OnUpdate()
    {
      base.OnUpdate();
     // EnemyStateMachine.OnUpdate();
    }
    /*public override void OnAttack()
    {
      if (!CanAttack)
      {
        return;
      }
      if (m_Weapon)
      {
        m_Weapon.Attack(Vector2.zero);
      }
    }*/
    public override void OnDead()
    {
        Heal();
        if (Random.Range(0, 10 + 1) > 4)
        {
            var _Entity = m_gameplayManager.ExperiencePool.Pool.GetNextItem();
            _Entity.transform.position = transform.position;
        }
        gameObject.SetActive(!gameObject.activeSelf);
            Original.HitShield();
   }
}
