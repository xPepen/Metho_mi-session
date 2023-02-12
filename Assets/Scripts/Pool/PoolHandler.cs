using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolHandler<T> where T : MainBehaviour
{
    public PoolPatern<T> Pool { get; private set; }
    public int PoolSize;
    public GameObject Prefab;
    public GameObject ParentObj;

    public void InitPool()
    {
        Pool = new PoolPatern<T>(PoolSize, Prefab, ParentObj);
    }
}