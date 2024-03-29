using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeapon : Weapon
{
    [SerializeField] protected float attackRange;
    [SerializeField] protected float Damage;
    protected Player m_playerRef;
    protected override void OnStart()
    {
        base.OnStart();
        if (AttackRate == 0)
        {
            AttackRate = 1f;
        }
        m_playerRef = Player.Instance;
    }
}
