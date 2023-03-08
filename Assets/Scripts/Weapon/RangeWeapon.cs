using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [field: SerializeField] public PoolHandler<Projectile> ProjectilePool { get; private set; }
    private List<IUpgradebleProjectile> m_ListOfProjectiles;
    public List<IUpgradebleProjectile> ListOfProjectiles => m_ListOfProjectiles;

    public void SubscribeProjectile(IUpgradebleProjectile _ref)
    {
        if (!m_ListOfProjectiles.Contains(_ref))
        {
            m_ListOfProjectiles.Add(_ref);
        }
        
    }

    public void UnSubscribeProjectile(IUpgradebleProjectile _ref)
    {
        if (!m_ListOfProjectiles.Contains(_ref))
        {
            m_ListOfProjectiles.Remove(_ref);
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        ProjectilePool.InitPool();
        m_ListOfProjectiles = new List<IUpgradebleProjectile>();
       var  x = GetComponentsInChildren<IUpgradebleProjectile>(true);
       foreach (var y in x)
       {
           m_ListOfProjectiles.Add(y);
       }
           
    }

    public override void Attack(Vector2 _dir)
    {
        if (CanAttak())
        {
            var _currProjectile = ProjectilePool.Pool.GetNextItem();
            _currProjectile.transform.position = this.transform.position;
            _currProjectile.OnMoveProjectile(_dir);
            m_timeWatch = 0f;
        }
    }
}