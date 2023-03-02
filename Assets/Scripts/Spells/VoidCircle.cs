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
             Physics2D.OverlapCircleNonAlloc(transform.position, base.SpellRadius,m_collisionResult,  base.m_target);
            if (m_collisionResult.Length > 0 && m_collisionResult[0] != null)
            {
                //int _currentCount = m_collisionResult.Length > base.HittableTargetCount ?  base.HittableTargetCount:m_collisionResult.Length;
                for (int i = 0; i < m_collisionResult.Length; i++)
                {
                    if (m_collisionResult[i] == null)
                    {
                        break;
                    }
                    var _isHittable = (m_collisionResult[i].GetComponent<IHitable>());
                    if (_isHittable != null)
                    {
                        _isHittable.OnHit(base.SpellDamage);
                    }

                    m_collisionResult[i] = null;
                }
                base.ResetAttack();
            }
        }
    }

    public void UpgradeRange(int _value)
    {
        base.Max_Range += _value;
        base.m_UpgradeName = "Void Circle : RangeUpgrade";
    }
    public void UpgradeDamage(int _value)
    {
        base.SpellDamage += _value;
        base.m_UpgradeName = "Void Circle : RangeUpgrade";
    }

    public void UpgradeTargetCount(int _value)
    {
        base.m_UpgradeName = "Void Circle : RangeUpgrade";
        m_HittableTargetCount += _value;
        m_collisionResult = null;
        m_collisionResult = new Collider2D[this.m_HittableTargetCount];
    }

    public override void InitUpgrade()
    {
        
    }

    public override void UpgradeStat(int _value)
    {
        
    }

    public override string UpgradeName()
    {
        return m_UpgradeName;
    }
}
