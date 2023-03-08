using UnityEngine;
using UnityEngine.Serialization;

public abstract class Spell : Weapon, IUpgradebleSpell
{
    [SerializeField] protected LayerMask m_target;

    [FormerlySerializedAs("HittableTargetCount")] [Tooltip("Max enemy that can be hit")] [SerializeField]
    protected int m_HittableTargetCount;

    [SerializeField] protected float SpellDamage;

    [Tooltip("Radius around entity (Rayon)")] [SerializeField]
    protected float SpellRadius;

    [Tooltip("Distance for hit (Diametre)")] [SerializeField]
    protected float Max_Range;

    protected Collider2D[] m_collisionResult;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_collisionResult = new Collider2D[this.m_HittableTargetCount];
    }

    protected void OnTargetCountChange()
    {
        for (int i = 0; i < m_collisionResult.Length; i++)
        {
            m_collisionResult[i] = null;
        }

        m_collisionResult = null;
        m_collisionResult = new Collider2D[this.m_HittableTargetCount];
    }

    protected abstract void OnVisualScaleChange();
    public abstract void OnRadiusUpgrade(float _value);


    public abstract void OnDamageUpgrade(float _value);


    public abstract void OnAttackRateUpgrade(float _value);

    public abstract void OnTargetCountUpgrade(float _increment);
}