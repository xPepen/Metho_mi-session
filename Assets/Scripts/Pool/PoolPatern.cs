using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPatern<T> where T: Component
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
        InstantiatePool( this.size);
    }
    private void InstantiatePool( int size)
    {

        for (int i = 0; i < size; i++)
        {
            var _obj = UnityEngine.GameObject.Instantiate(m_prefab);
            _obj.transform.parent = m_parentObj.transform;
            _obj.gameObject.SetActive(false);
            var _newNode = _obj.GetComponent<T>();
            m_pool.Enqueue(_newNode);
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
        if(m_pool.Count == 0)
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