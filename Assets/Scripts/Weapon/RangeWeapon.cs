using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [Header("Projectile")]
    [SerializeField] private GameObject m_projectile;
    [SerializeField] private int m_poolSize;
    [SerializeField] private GameObject m_poolLocation;
    public PoolPatern<Projectile> ProjectilePool { get; private set; }
    
   
    protected override void OnAwake()
    {
        base.OnAwake();
        ProjectilePool = new PoolPatern<Projectile>(m_poolSize, m_projectile, m_poolLocation);
    }

   

    public override void Attack(Vector2 _dir)
    {
       
        if (CanAttak() )
        {
            var _currProjectile = ProjectilePool.GetNextItem();
            _currProjectile.transform.position = this.transform.position;
            _currProjectile.OnMoveProjectile(_dir);
            m_timeWatch = 0f;
           
        }
    }
    
}
