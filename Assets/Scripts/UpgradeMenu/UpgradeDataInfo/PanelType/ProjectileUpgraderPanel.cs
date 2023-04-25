using System.Collections.Generic;
using UnityEngine;

public class ProjectileUpgraderPanel : UpgraderPanel
{
    [SerializeField] private RangeWeapon m_playerWeaponRef;

    private const string m_TitleKeyValue = "Projectile_Upgrade_Title";
    private const string m_DescriptionKeyValue = "Projectile_Upgrade_Description";

    protected override void OnAwake()
    {
        base.OnAwake();
        SetKeys(m_TitleKeyValue, m_DescriptionKeyValue);
    }

    protected override void InitUpgradeNodeQueue()
    {
        //init queue
        base.m_QueueOfUpgrade = new Queue<UpgradeNode>();
        //upgrade # 1
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
            m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnDamageUpgrade(30f)),30f));
        //upgrade # 2
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
            m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnProjectileSizeUpgrade(50)),50f));
        //upgrade # 3
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
            m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnSpeedUpgrade(30)),30f));
        //upgrade # 4
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
            m_playerWeaponRef.ListOfProjectiles.ForEach(x => x.OnDamageUpgrade(40)),40f));
    }
}