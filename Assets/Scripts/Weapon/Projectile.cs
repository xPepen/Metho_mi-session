using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseEntity
{
    [SerializeField] private float m_damage;
    [SerializeField] private float HittableRadius;
    [SerializeField] private float HittableDistance;
    [SerializeField] private float m_speed;
    private PoolPatern<Projectile> m_poolProjectile;
    [SerializeField] private EnumEntityType Target;
    private Rigidbody2D m_rb;
    [Header("Projectile")]
    [SerializeField] private float m_lifeTime;
    private float m_currentLifetime;
    protected Vector2 m_velocity;
    private bool IsTooOld => m_currentLifetime >= m_lifeTime;
    protected override void OnAwake()
    {
        base.OnAwake();
        m_rb= GetComponent<Rigidbody2D>();
        m_currentLifetime = 0f;
    }
    protected override void OnStart()
    {
        base.OnStart();
        m_poolProjectile = GetComponentInParent<RangeWeapon>().ProjectilePool;
    }
    public void OnMoveProjectile(Vector2 _dir)
    {
        m_velocity =  (_dir - (Vector2)transform.position).normalized * m_speed * Time.fixedDeltaTime;
        m_rb.velocity = m_velocity;
        //m_rb.AddForce(  _dir.normalized * m_speed * Time.deltaTime,ForceMode2D.Impulse);
        //m_rb.AddForce(  velocity.normalized * m_speed * Time.fixedDeltaTime ,ForceMode2D.Impulse);
    }

    private void OnDisableProjectile()
    {
        m_rb.velocity = Vector2.zero;
        transform.position = Vector3.one * -100f;
        m_poolProjectile.ReAddItem(this);
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

        if (D.Get<GameplayManager>().IsGamePause)
        {
            m_rb.velocity = Vector2.zero;
        }
        else
        {
            if (m_rb.velocity != m_velocity)
            {
                m_velocity = m_rb.velocity;
            }
        }
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
    protected void DectectEntity()
    {
        var _potentialEntity = Physics2D.CircleCast(transform.position, HittableRadius, Vector2.zero);
        LivingEntity _hitable = null;
        if (!_potentialEntity)
        {
            return;
        }
             _hitable = _potentialEntity.transform.GetComponent<LivingEntity>();

        if (_hitable != null && _hitable.Type == Target)
        {
            if (Vector3.Distance(this.transform.position, _potentialEntity.transform.position) <= HittableDistance)
            {
                (_hitable as IHitable).OnHit(m_damage); // to be verify ...
                OnDisableProjectile();
            }
        }
        
    }
}
