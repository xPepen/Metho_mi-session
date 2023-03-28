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


    protected override void InitUpgradeNodeQueue()
    {
        //init queue
        base.m_QueueOfUpgrade = new Queue<UpgradeNode>();
        //upgrade # 1
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnDamageUpgrade(30)),
            "30% more damage on projectile"));
        //upgrade # 2
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnProjectileSizeUpgrade(50)),
            "30% more radius on projectile"));
        //upgrade # 3
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnSpeedUpgrade(30)),
            "30% more speed on projectile"));
        //upgrade # 4
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnDamageUpgrade(40)),
            "40% more damage on projectile"));
    }
}