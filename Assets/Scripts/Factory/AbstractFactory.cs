using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public abstract class AbstractFactory: MainBehaviour 
{
    public abstract UnityEngine.GameObject CreateEnemy();
    public PoolHandler<Enemy> Pool;

}
