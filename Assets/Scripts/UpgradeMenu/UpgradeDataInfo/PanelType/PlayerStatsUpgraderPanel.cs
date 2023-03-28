using System.Collections.Generic;
using TNRD;
using UnityEngine;

public class PlayerStatsUpgraderPanel : UpgraderPanel
{
    [SerializeField] private SerializableInterface<IUpgradeblePlayerStats> m_PlayerInterface;


    [Header("Health")] [SerializeField] private float m_HealthUpgradeValue;
    [SerializeField] private float m_HealthUpgradeMultiplier;
    [Header("Speed")] [SerializeField] private float m_SpeedUpgradeValue;
    [SerializeField] private float m_SpellTargetCountMultiplier;

    private const string m_TitleKey = "Player_Stats_Title";
    private const string m_DescriptionKey = "Player_Stats_Description";

    protected override void OnAwake()
    {
        base.OnAwake();
        SetKeys(m_TitleKey,m_DescriptionKey);
    }

    protected override void InitUpgradeNodeQueue()
    {
        //init queue
        base.m_QueueOfUpgrade = new Queue<UpgradeNode>();
        //upgrade # 1
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_PlayerInterface.Value.OnStatHealthUpgrade(50),
            50));
        //upgrade # 2
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_PlayerInterface.Value.OnStatSpeedUpgrade(20),
            20));
        //upgrade # 3
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_PlayerInterface.Value.OnStatHealthUpgrade(100),
            100));
        //upgrade # 4
        m_QueueOfUpgrade.Enqueue(new UpgradeNode(() =>
                m_PlayerInterface.Value.OnStatSpeedUpgrade(30),
            30));
    }

   
}