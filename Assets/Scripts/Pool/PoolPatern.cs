using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolPatern<T> where T : Component
{
    private Queue<T> m_pool;
    private UnityEngine.GameObject m_prefab;
    private UnityEngine.GameObject m_parentObj;
    private int size;
    public PoolPatern(int size, UnityEngine.GameObject prefab, UnityEngine.GameObject Parent)
    {
        BasicException(size);
        m_pool = new Queue<T>();
        this.size = size;
        m_prefab = prefab;
        m_parentObj = Parent;
        if (m_prefab.TryGetComponent(out IPooler<T> _))
        {
            InstantiatePool(this.size);
            return;
        }
        throw new UnityException($"Prefab name {m_prefab.name} inject into the pool didn't containt 'IPooler<T> interface'");
    }

    public List<T> PoolObjectList()
    {
        return m_pool.ToList();
    }

    public List<E> ConvertListTo<E>()
    {
        return m_pool.Cast<E>().ToList();
    }

    private void InstantiatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var _obj = UnityEngine.GameObject.Instantiate(m_prefab, m_parentObj.transform);

            _obj.GetComponent<IPooler<T>>().RePoolItem = ReAddItem;
            _obj.gameObject.SetActive(false);
            m_pool.Enqueue(_obj.GetComponent<T>());
        }
    }
    private void BasicException(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentException("Pool size must be a positive number and supperior to 0");
        }
    }
    public T GetNextItem()
    {
        if (m_pool.Count == 0)
        {
            InstantiatePool(size);
        }

        var _obj = m_pool.Dequeue();

        _obj.gameObject.SetActive(true);
        return _obj;
    }

    public void ReAddItem(T _obj)
    {
        m_pool.Enqueue(_obj);
        _obj.gameObject.SetActive(false);
    }
}