using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [field: SerializeField] public PoolHandler<Projectile> ProjectilePool { get; private set; }
    private List<IUpgradebleProjectile> m_ListOfProjectiles;
    public List<IUpgradebleProjectile> ListOfProjectiles => m_ListOfProjectiles;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_ListOfProjectiles = new List<IUpgradebleProjectile>();
        ProjectilePool.InitPool();
        m_ListOfProjectiles = ProjectilePool.Pool.ConvertListTo<IUpgradebleProjectile>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        m_ListOfProjectiles = ProjectilePool.Pool.ConvertListTo<IUpgradebleProjectile>();
    }

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


    public override void Attack(Vector2 _dir)
    {
        if (CanAttak())
        {
            var _currProjectile = ProjectilePool.Pool.GetNextItem();
            _currProjectile.transform.position = transform.position;
            _currProjectile.OnMoveProjectile(_dir);
            m_timeWatch = 0f;
        }
    }
}