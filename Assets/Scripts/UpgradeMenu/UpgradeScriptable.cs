using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class UpgradeScriptable : ScriptableObject
{
    public GameObject _objReference;
    [SerializeField] private String m_UpgradeDescription;
    public UnityEvent y;
    
    public void x (int current)
    {
        Debug.Log(_objReference.transform.position);
    }
}
