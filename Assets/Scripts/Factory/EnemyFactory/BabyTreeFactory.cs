using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTreeFactory : EnemyFactory<BabyTreeFactory>
{
    public override UnityEngine.GameObject CreateEnemy()
    {
        return base.Pool.Pool.GetNextItem().gameObject;
    }
    public UnityEngine.GameObject CreateEnemy2(BossTree _ref)
    {
        var _copy = base.Pool.Pool.GetNextItem();
      
            (_copy as BabyTree).Original = _ref;

        
        return base.Pool.Pool.GetNextItem().gameObject;
    }
}
