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
    [SerializeField] private Transform Barrel;

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
        var x =  _dir - (Vector2)transform.position;
        //m_rb.velocity = _dir.normalized * m_speed ;
        //m_rb.AddForce(  _dir.normalized * m_speed * Time.deltaTime,ForceMode2D.Impulse);
        m_rb.AddForce(  x.normalized * m_speed * Time.fixedDeltaTime ,ForceMode2D.Impulse);
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
        GetOlder();
        if (IsTooOld)
        {
            OnDisableProjectile();
        }
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
         DectectEntity();
    }
    protected void DectectEntity()
    {
        var _potentialEntity = Physics2D.CircleCast(transform.position, HittableRadius, Vector2.zero);
        LivingEntity _hitable = null;
        if (_potentialEntity)
        {
             _hitable = _potentialEntity.transform.gameObject.GetComponent<LivingEntity>();
        }
       
        if (_hitable != null && _hitable.Type == Target)
        {
            if (Vector3.Distance(this.transform.position, _potentialEntity.transform.position) <= HittableDistance)
            {
                (_hitable as IHitable).OnHit(m_damage); // to be verify ...
                print("I hit you with a projectile");
                OnDisableProjectile();
            }
        }
        
    }
}
