using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

public class PlayerStatsUpgraderPanel : UpgraderPanel
{
    [SerializeField] private SerializableInterface<IUpgradeblePlayerStats> m_SpellInterface;

   
    [Header("Health")] 
    [SerializeField] private float m_HealthUpgradeValue;
    [SerializeField] private float m_HealthUpgradeMultiplier;
    [Header("Speed")] 
    [SerializeField] private float m_SpeedUpgradeValue;
    [SerializeField] private float m_SpellTargetCountMultiplier;
    [Header("New Spell")] [SerializeField] private List<GameObject> m_ListOfSpell;

    
    public override void InitUpgradePanel()
    {
        var _statValue = 0.00f;
        switch (Random.Range(0, 2))
        {
            case 0:
                _statValue = m_HealthUpgradeValue;
                SetDescription(_statValue, "Health Upgrade");
                OnSubscribeAction(() => m_SpellInterface.Value.OnStatHealthUpgrade(_statValue),
                    ref m_HealthUpgradeValue, m_HealthUpgradeMultiplier);
                return;

            case 1:
                _statValue = m_SpeedUpgradeValue;
                SetDescription(_statValue, "Radius");
                OnSubscribeAction(() => m_SpellInterface.Value.OnStatSpeedUpgrade(_statValue),
                    ref m_SpeedUpgradeValue, m_SpeedUpgradeValue);
                return;
            /*case 2:
                var _spell = m_ListOfSpell[Random.Range(0, m_ListOfSpell.Count)];
                SetDescription( "Add new spell "+_spell.name);
                OnSubscribeAction(() => m_SpellInterface.Value.OnAddNewSpell(_spell));
                OnSubscribeAction(() => m_ListOfSpell.Remove(_spell));
                return;*/
           
        }

            
    }

   
}
