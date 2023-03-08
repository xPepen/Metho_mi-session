using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

public class ProjectileUpgraderPanel : UpgraderPanel
{
    [SerializeField] private RangeWeapon m_playerWeaponRef;
    [Header("Multiplier Spell Value")] [SerializeField]
    private float m_SpellDamageMultiplier;

    [SerializeField] private float m_SpellRadiusMultiplier;
    [SerializeField] private float m_SpellAttackRateMultiplier;
    [SerializeField] private float m_SpellTargetCountMultiplier;

    public override void InitUpgradePanel()
    {
        var _statValue = 0.00f;
        switch (0)
        {
            case 0:
            
                _statValue = m_SpellDamageMultiplier;
                SetDescription(_statValue, "Damage");
                OnSubscribeAction(() => m_playerWeaponRef.ListOfProjectiles.ForEach(_projectile => _projectile.OnDamageUpgrade(5f)),
                    ref m_SpellRadiusMultiplier, 2);
                return;

           
        }

            
    }

}
