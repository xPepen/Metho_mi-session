using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCircle : Spell
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        Attack(Vector2.zero);
    }
    public override void Attack(Vector2 _dir)
    {
        if (CanAttack)
        {
            var _potentialEntity = Physics2D.CircleCastAll(transform.position, base.SpellRadius, Vector2.zero, base.Max_Range, base.m_target);
            //Gizmos.DrawSphere(transform.position, SpellRadius);
            if (_potentialEntity.Length > 0)
            {
                int _currentCount = _potentialEntity.Length > base.HittableTargetCount ?  base.HittableTargetCount:_potentialEntity.Length;

                for (int i = 0; i < _currentCount; i++)
                {
                    var _isHittable = (_potentialEntity[i].transform.GetComponent< IHitable>());
                    if (_isHittable != null)
                    {
                        _isHittable.OnHit(base.SpellDamage);
                        print("I just did attak");
                    }
                }
                base.ResetAttack();
            }
        }
    }
}
