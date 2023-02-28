using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPigFactory2 : EnemyFactory<EnemyPigFactory2>
{
    public override GameObject CreateEnemy()
    {
        return Pool.Pool.GetNextItem().gameObject;
    }
}
