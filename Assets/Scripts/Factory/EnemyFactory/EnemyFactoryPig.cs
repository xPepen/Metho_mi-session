using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryPig : EnemyFactory<EnemyFactoryPig>
{
    
   
    public override GameObject CreateEnemy()
    {
        return base.Pool.Pool.GetNextItem().gameObject;
    }
}
