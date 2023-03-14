using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UpgradeMenu
{
    public class SpellUpgraderPanel : UpgraderPanel
    {
        public SpellManager spellManager;

        private Queue<UpgradeNode> m_QueueOfUpgrade;
        private IUpgradebleSpell m_SpellInterface;
        [SerializeField] private TNRD.SerializableInterface<IUpgradeblePlayerStats> m_playerInterface;

        [Header("Multiplier Spell Value")] [SerializeField]
        private float m_SpellDamageMultiplier;

        [SerializeField] private float m_SpellRadiusMultiplier;
        [SerializeField] private float m_SpellAttackRateMultiplier;
        [SerializeField] private float m_SpellTargetCountMultiplier;

        protected bool isActionCompleted;
        protected int m_CurrentUpgrade;
        protected const int MAX_UPGRADE = 5;
        protected override void OnAwake()
        {
            base.OnAwake();
            isActionCompleted = true;
            if (spellManager)
            {
                m_SpellInterface ??= spellManager.GetSpellInterface(SpellEnum.VOIDCIRCLE);
            }
            InitUpgradeNodeQueue();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            isActionCompleted = true;
            InitUpgradePanel();
        }

        private void InitUpgradeNodeQueue()
        {
            //init queue
            m_QueueOfUpgrade = new Queue<UpgradeNode>();
            //add the spell to the player
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                    m_playerInterface.Value.OnAddNewSpell(spellManager.GetSpell(SpellEnum.VOIDCIRCLE)),
                "new spell void circle around the player"));
            //upgrade # 1
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                    m_SpellInterface.OnRadiusUpgrade(20f),
                "20 % radius add to void circle spell"));
            //upgrade # 2
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                    m_SpellInterface.OnAttackRateUpgrade(30f),
                "30 % Attack Rate to void circle spell"));
            //upgrade # 3
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                    m_SpellInterface.OnDamageUpgrade(25f),
                "25 % Attack Damage to void circle spell"));
            //upgrade # 4
            m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                    m_SpellInterface.OnTargetCountUpgrade(3f),
                "3 more target hitted to void circle spell"));
        }

        private UpgradeNode GetFirstNode(out bool _ActionState)
        {
            _ActionState = false;
            if (m_QueueOfUpgrade.Count <= 0)
            {
                return null;
            }

            m_CurrentUpgrade++;
            return m_QueueOfUpgrade.Dequeue();

        }

        public override void InitUpgradePanel()
        {
            if (isActionCompleted && m_CurrentUpgrade < MAX_UPGRADE -1)
            {
                var node = GetFirstNode(out isActionCompleted);
                SetDescription(node?.UgradeDefinition);
                OnSubscribeAction(node?.UpgradeAction);
            }
        }
    }
}