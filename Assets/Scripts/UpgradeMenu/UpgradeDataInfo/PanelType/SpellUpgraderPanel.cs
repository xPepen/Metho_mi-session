using System.Collections.Generic;
using UnityEngine;


namespace UpgradeMenu
{
    public class SpellUpgraderPanel : UpgraderPanel
    {
        public SpellManager spellManager;

        private IUpgradebleSpell m_SpellInterface;
        [SerializeField] private TNRD.SerializableInterface<IUpgradeblePlayerStats> m_playerInterface;

        [Header("Multiplier Spell Value")] [SerializeField]
        private float m_SpellDamageMultiplier;

        [SerializeField] private float m_SpellRadiusMultiplier;
        [SerializeField] private float m_SpellAttackRateMultiplier;
        [SerializeField] private float m_SpellTargetCountMultiplier;
        private const string m_TitleKeyValue = "Void_Circle_Title";
        private const string m_DescriptionKeyValue = "Void_Circle_Description";


        protected override void OnAwake()
        {
            base.OnAwake();
            if (spellManager)
            {
                m_SpellInterface = spellManager.GetSpellInterface(SpellEnum.VOIDCIRCLE);
            }

            SetKeys(m_TitleKeyValue, m_DescriptionKeyValue);
        }


        protected override void InitUpgradeNodeQueue()
        {
            //init queue
            base.m_QueueOfUpgrade = new Queue<UpgradeNode>();
            //add the spell to the player
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_playerInterface.Value.OnAddNewSpell(spellManager.GetSpell(SpellEnum.VOIDCIRCLE))));
            //upgrade # 1
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_SpellInterface.OnRadiusUpgrade(20f), 20));
            //upgrade # 2
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_SpellInterface.OnAttackRateUpgrade(30f), 30));
            //upgrade # 3
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_SpellInterface.OnDamageUpgrade(25f), 25));
            //upgrade # 4
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_SpellInterface.OnTargetCountUpgrade(3f), 3));
        }
    }
}