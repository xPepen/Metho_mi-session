using System.Collections;
using UnityEngine;

[System.Serializable]
public class PoolHandler<T> where T : MainBehaviour
{
    public PoolPatern<T> Pool { get; private set; }
    public int PoolSize;
    public UnityEngine.GameObject Prefab;
    public UnityEngine.GameObject ParentObj;
    public void InitPool()
    {
        Pool = new PoolPatern<T>(PoolSize, Prefab, ParentObj);
    }
}