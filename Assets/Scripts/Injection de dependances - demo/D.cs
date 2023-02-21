using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class D
{
    private readonly static Dictionary<string, MonoBehaviour> m_dictionaryRef = new Dictionary<string, MonoBehaviour>();

     public static void Bind(this MonoBehaviour _instance)
     {   
        string _key = _instance.GetType().Name;
        if (!m_dictionaryRef.ContainsKey(_key))
        {
            m_dictionaryRef.Add(_key,_instance);
        }
        else
        {
            throw new ArgumentException("Key : " + _key + " already exist can't use Bind");
        }
     }
     
     public static T Get<T>() where  T : MonoBehaviour
     {
         string _key = typeof(T).Name;
         return m_dictionaryRef.ContainsKey(_key) ? (T)m_dictionaryRef[_key] : null;
     }
     public static void Clear() => m_dictionaryRef.Clear();
     public static void Remove<T>() where T : MonoBehaviour
     {
         var _key = typeof(T).Name;
         if(m_dictionaryRef.ContainsKey(_key))
         {
             m_dictionaryRef.Remove(typeof(T).Name);
         }
         else
         {
             throw new ArgumentException("Key : " + _key + " not found can't use remove");
         }
     }
}
