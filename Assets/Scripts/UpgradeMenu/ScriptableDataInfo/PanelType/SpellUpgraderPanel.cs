using System;
using TNRD;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace UpgradeMenu
{
    public class SpellUpgraderPanel : UpgraderPanel
    {
        [SerializeField] private SerializableInterface<IUpgradebleSpell> m_SpellInterface;

        [Header("Multiplier Spell Value")] [SerializeField]
        private float m_SpellDamageMultiplier;

        [SerializeField] private float m_SpellRadiusMultiplier;
        [SerializeField] private float m_SpellAttackRateMultiplier;
        [SerializeField] private float m_SpellTargetCountMultiplier;

        public override void InitUpgradePanel()
        {
            var _statValue = 0.00f;
            switch (Random.Range(0, 4))
            {
                case 0:
                     _statValue = m_SpellDamageMultiplier;
                    SetDescription(_statValue, "Damage");
                    OnSubscribeAction(() => m_SpellInterface.Value.OnDamageUpgrade(_statValue),
                        ref m_SpellDamageMultiplier, 2);
                    return;

                case 1:
                    _statValue = m_SpellRadiusMultiplier;
                    SetDescription(_statValue, "Radius");
                    OnSubscribeAction(() => m_SpellInterface.Value.OnRadiusUpgrade(_statValue),
                        ref m_SpellRadiusMultiplier, 2);
                    return;
                case 2:
                    _statValue = m_SpellAttackRateMultiplier;
                    SetDescription(_statValue, "Attack Rate");
                    OnSubscribeAction(() => m_SpellInterface.Value.OnAttackRateUpgrade(_statValue),
                        ref m_SpellAttackRateMultiplier, 2);
                    return;
                case 3:
                    _statValue = m_SpellTargetCountMultiplier;
                    SetDescription(_statValue, "Target Count");
                    OnSubscribeAction(() => m_SpellInterface.Value.OnTargetCountUpgrade(_statValue),
                        ref m_SpellTargetCountMultiplier, 1);
                    return;
            }

            
        }
    }
}