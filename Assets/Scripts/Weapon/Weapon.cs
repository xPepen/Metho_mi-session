using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MainBehaviour, IShootable
{
    [Header("Projectile")]
    [SerializeField] private GameObject m_projectile;
    [SerializeField] private int m_poolSize;
    [SerializeField] private GameObject m_poolLocation;
    [SerializeField] private float m_fireRate;
    private float m_timeWatch;
    public PoolPatern<Projectile> ProjectilePool { get; private set; }

    private bool CanShoot => m_timeWatch >= m_fireRate;
    protected override void OnAwake()
    {
        base.OnAwake();
        ProjectilePool = new PoolPatern<Projectile>(m_poolSize, m_projectile, m_poolLocation);
    }

    public void Attack(Vector2 _dir)
    {
        if (CanShoot)
        {
            var _currProjectile = ProjectilePool.GetNextItem();
            _currProjectile.transform.position = this.transform.position;
            _currProjectile.OnMoveProjectile(_dir);
            m_timeWatch = 0f;
        }
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        m_timeWatch += Time.deltaTime;
    }
}
