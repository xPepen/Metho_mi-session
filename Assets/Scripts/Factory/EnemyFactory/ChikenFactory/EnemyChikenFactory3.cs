using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChikenFactory3 : EnemyFactory<EnemyChikenFactory3>
{
   
    public override GameObject CreateEnemy()
    {
        return base.Pool.Pool.GetNextItem().gameObject;
    }
}
