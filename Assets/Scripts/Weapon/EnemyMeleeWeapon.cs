using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : EnemyWeapon
{
    protected override void OnStart()
    {
        base.OnStart();
        m_timeWatch = m_attackRate;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void Attack(Vector2 _dir)
    {
        if (CanAttack)
        {

            var _isplayerClose = Vector3.Distance(Player.Instance.transform.position, transform.position) < attackRange;
            if (_isplayerClose)
            {
                (Player.Instance as IHitable).OnHit(Damage);
            }
            m_timeWatch = 0.00f;

        }
    }
    
}
