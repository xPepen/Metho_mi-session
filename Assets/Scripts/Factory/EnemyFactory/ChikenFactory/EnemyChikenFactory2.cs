using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChikenFactory2 : EnemyFactory<EnemyChikenFactory2>
{
    public override GameObject CreateEnemy()
    {
        return base.Pool.Pool.GetNextItem().gameObject;
    }
}
