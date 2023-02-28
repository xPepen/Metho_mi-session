using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPigFactory3 : EnemyFactory<EnemyPigFactory3>
{
    public override GameObject CreateEnemy()
    {
        return Pool.Pool.GetNextItem().gameObject;
    }
}
