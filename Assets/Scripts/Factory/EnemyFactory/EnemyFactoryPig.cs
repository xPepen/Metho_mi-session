using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryPig : EnemyFactory<EnemyFactoryPig>
{
    
    protected override void OnStart()
    {
        base.OnStart();
        
        base.Pool.InitPool();
    }
    public override GameObject CreateEnemy()
    {
        var _enemy = base.Pool.Pool.GetNextItem();
        //if (_enemy.poolRef == null)
        //{
        //    _enemy.poolRef = base.Pool.Pool;
        //}
        return _enemy.gameObject;
    }
}
