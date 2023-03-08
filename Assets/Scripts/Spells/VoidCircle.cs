using UnityEngine;

public class VoidCircle : Spell
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        Attack(Vector2.zero); // add to player update instead
    }


    public override void Attack(Vector2 _dir)
    {
        if (CanAttack)
        {
            Physics2D.OverlapCircleNonAlloc(transform.position, base.SpellRadius, m_collisionResult, base.m_target);
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

    protected override void OnVisualScaleChange()
    {
        var _newRadius = SpellRadius * 2f;
        transform.localScale = new Vector3(_newRadius, _newRadius);
    }

    public override void OnRadiusUpgrade(float _value)
    {
        if (_value > 0)
        {
            base.SpellRadius += _value;
            OnVisualScaleChange();
        }
    }

    public override void OnDamageUpgrade(float _value)
    {
        if (_value > 0)
        {
            base.SpellDamage += _value;
        }
    }

    public override void OnAttackRateUpgrade(float _value)
    {
        if (_value > 0)
        {
            base.m_attackRate += _value;
        }
    }

    public override void OnTargetCountUpgrade(float _increment)
    {
        if (_increment > 0)
        {
            base.m_HittableTargetCount += (int)_increment;
            base.OnTargetCountChange();
        }
    }
}