using UnityEngine;

public class Projectile : BaseEntity, IUpgradebleProjectile
{
    [field:SerializeField]public RangeWeapon m_RangeWeaponRef { get; set; }
    [SerializeField] private float m_damage;
    [SerializeField] private float HittableRadius;
    [SerializeField] private float HittableDistance;
    [SerializeField] private float m_speed;
    [SerializeField] private EnumEntityType Target;
    private Rigidbody2D m_rb;

    [Header("Projectile")] [SerializeField]
    private float m_lifeTime;

    private float m_currentLifetime;
    protected Vector2 m_velocity;

    private Collider2D[] m_collResult;
    private bool IsTooOld => m_currentLifetime >= m_lifeTime;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_rb = GetComponent<Rigidbody2D>();
        m_currentLifetime = 0f;
        m_collResult = new Collider2D[1];
        //m_RangeWeaponRef = GetComponentInParent<RangeWeapon>(true);
        // m_RangeWeaponRef.SubscribeProjectile(this);
    }

    public void OnMoveProjectile(Vector2 _dir)
    {
        m_velocity = (_dir - (Vector2)transform.position).normalized * (m_speed * Time.fixedDeltaTime);
        m_rb.velocity = m_velocity;
    }

    private void OnDisableProjectile()
    {
        m_rb.velocity = Vector2.zero;
        transform.position = Vector3.one * -100f;
        m_RangeWeaponRef.ProjectilePool.Pool.ReAddItem(this);
        m_currentLifetime = 0f;
    }

    private void GetOlder()
    {
        m_currentLifetime += Time.deltaTime;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        DectectEntity();
        GetOlder();
        if (IsTooOld)
        {
            OnDisableProjectile();
        }
    }

    protected void DectectEntity()
    {
        //var _potentialEntity = Physics2D.CircleCast(transform.position, HittableRadius, Vector2.zero);
        Physics2D.OverlapCircleNonAlloc(transform.position, HittableRadius, m_collResult);
        // LivingEntity _hitable = m_collResult[0].transform.TryGetComponent(out LivingEntity livingEntity);
        if (m_collResult[0] == null || m_collResult[0].transform == null) return;
        if (m_collResult[0].transform.TryGetComponent(out LivingEntity _hitable))
        {
            if (_hitable.Type == Target)
            {
                if (Vector3.Distance(this.transform.position, m_collResult[0].transform.position) <= HittableDistance)
                {
                    (_hitable as IHitable).OnHit(m_damage);
                    OnDisableProjectile();
                }
            }
        }
    }

//interface
    public void OnDamageUpgrade(float _value)
    {
        if (_value > 0)
        {
            this.m_damage += ValueToPercent(_value, m_damage);
        }
    }

    public void OnProjectileSizeUpgrade(float _value)
    {
        HittableRadius += ValueToPercent(_value, HittableRadius);
        HittableDistance += ValueToPercent(_value, HittableDistance);
        OnVisualScaleChange();
    }


    public void OnSpeedUpgrade(float _value)
    {
        if (_value > 0)
        {
            this.m_speed += ValueToPercent(_value, m_speed);
        }
    }

    private float ValueToPercent(float multiplier, float baseValue)
    {
        return ((multiplier / 100) * baseValue);
    }

    private void OnVisualScaleChange()
    {
        transform.localScale = new Vector3(HittableRadius, HittableRadius);
    }
}