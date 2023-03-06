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

    public void UpgradeRange(int _value)
    {
        base.Max_Range += _value;
    }

    public void UpgradeDamage(int _value)
    {
        base.SpellDamage += _value;
    }

    public void UpgradeTargetCount(int _value)
    {
        m_HittableTargetCount += _value;
        m_collisionResult = null;
        m_collisionResult = new Collider2D[this.m_HittableTargetCount];
    }

    public override void OnRadiusUpgrade(float _value)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDamageUpgrade(float _value)
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackRateUpgrade(float _value)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTargetCountUpgrade(int _increment)
    {
        throw new System.NotImplementedException();
    }
}