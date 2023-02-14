using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : Manager<GameplayManager>
{
    [field: SerializeField] public PoolHandler<ExperienceCollectable> ExperiencePool { get; private set; }
    //[field: SerializeField] public PoolHandler<ExperienceCollectable> Potion { get; private set; } // heal player for x value

    [SerializeField] private UnityEngine.UI.Image[] PlayerInfoImageArray;// [0] XP - [1] Life
    public Player m_playerRef { get; private set; }
    private float m_maxHP;

    //EXPERIENCE
    private float m_maxExp;

    protected override void OnStart()
    {
        base.OnStart();
        InitPlayer();
        ExperiencePool.InitPool();
        m_maxHP = m_playerRef.maxHP;
        m_maxExp = 100f;
        SetHPBar(false);
        SetExpBar(false);
    }

    public void SetHPBar(bool _isInit = true)
    {
        if (!_isInit)
        {
            SetFillBar(1, m_maxHP / m_maxHP);
        }
        if(m_playerRef.currentHP > 0)
        {
            SetFillBar(1 ,m_playerRef.currentHP / m_maxHP);
        }
        else
        {
            //gameover
        }
    }
    public void SetExpBar(bool _isInit = true)
    {
        if (!_isInit)
        {
            SetFillBar(0, 0);
        }
        SetFillBar(0, m_playerRef.CurrentXp / m_maxExp);

        if (m_playerRef.CurrentXp >=  m_maxExp)
        {
            m_playerRef.Level++;
            SetFillBar(0, 0);
            //add ui too  chose an upgrade
        }
      
    }

    private void SetFillBar(int _index, float _currAmount)
    {
        PlayerInfoImageArray[_index].fillAmount = _currAmount;
    }
    private void InitPlayer()
    {
        if (!GameObject.Find("Player"))
        {
            throw new Exception("The Player GameObject isnt in the scene");
        }
        else
        {
            m_playerRef = GameObject.Find("Player").GetComponent<Player>();

        }
    }
}
