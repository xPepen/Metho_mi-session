using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using UnityEngine.Events;

public class UpgradePanel : MainBehaviour, IUpgradableMenu
{
    [SerializeField] private SerializableInterface<IUpgradeble> m_AffectedEntity; 
    [SerializeField] private UpgradeManager m_UpgradeManager; 
    
    [SerializeField]private UnityEvent OnClickEnter;
    public void OnClickConfirmed()
    {
        if (OnClickEnter.GetPersistentEventCount() < 0)
        {
            return;
            m_AffectedEntity.Value.UpgradeStat(10);
        }
        OnClickEnter.Invoke();
    }

   
}
