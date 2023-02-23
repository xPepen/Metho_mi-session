using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory <T> : AbstractFactory where T : EnemyFactory<T>
{
    public PoolHandler<Enemy> Pool;
    public static T Instance { get; private set; }
    protected override void OnAwake()
    {
        base.OnAwake();
        if (Instance == null)
        {
            Instance = GetComponent<T>();
        }
        else
        {
            Destroy(Instance);
        }
        this.Pool.InitPool();
    }
}
