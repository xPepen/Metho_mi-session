using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UpgradeDataInfo
{
    //public T instance;
    [SerializeField] private string m_upgradeName;
    public int CurrentUpgrade;
    [SerializeField] private UpgradeScriptableDescription m_UpgradeDescription2;
    [TextArea] [SerializeField] private string m_UpgradeDescription;
    [field:SerializeField] public UnityEvent UpgradeAction { get;  set; }
    public string UpgradeDescription => m_UpgradeDescription;
   
    public UpgradeScriptableDescription UpgradeDescription2 => m_UpgradeDescription2;
}