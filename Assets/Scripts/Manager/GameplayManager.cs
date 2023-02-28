using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MainBehaviour
{
   
    //[field: SerializeField] public PoolHandler<ExperienceCollectable> Potion { get; private set; } // heal player for x value

    [SerializeField] private UnityEngine.UI.Image[] PlayerInfoImageArray;// [0] XP - [1] Life
    public Player m_playerRef { get; private set; }
    public Action OnGamePause;
    public GameObject GameMenu;
    public bool IsGamePause { get; private set; }
    //----------Experience Collectable -------------
    [field: SerializeField] public PoolHandler<ExperienceCollectable> ExperiencePool { get; private set; }
    private SortedSet<ExperienceCollectable> m_xpSet;
    //EXPERIENCE

    protected override void OnAwake()
    {
        base.OnAwake();
        this.Bind();
    }

    protected override void OnStart()
    {
        base.OnStart();
        m_playerRef = D.Get<Player>();

        ExperiencePool.InitPool();
        SetHPBar(false);
        SetExpBar(false);
        m_xpSet = new SortedSet<ExperienceCollectable>(new ExperienceComparable());
        IsGamePause = false;
        CheckTimeScale(IsGamePause);
        OnGamePause = () =>
        {
            IsGamePause = !IsGamePause;
            GameMenu.SetActive( !GameMenu.activeSelf);
            CheckTimeScale(IsGamePause);

        };
    }

    private void CheckTimeScale(bool _isStop)
    {
        if (_isStop)
        {
            Time.timeScale = 0.00f;
            return;
        }
        Time.timeScale = 1.00f;
    }

    public void RestartLevel(int _LevelIndex)
    {
        D.Clear();
        SceneManager.LoadScene(_LevelIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
    //public void AddXpToken(ExperienceCollectable _XP)
    //{
    //    XP_LIST.Add(_XP.currentXP,_XP);
    //   var x = XP_LIST.ElementAt(0);
    //   x.Value.GetComponent<SpriteRenderer>().color = Color.black;
    //    //XP_given[0].GetComponent<SpriteRenderer>().color = Color.black;
    //    print(XP_LIST.Count);
    //}
    //public void RemoveXPKey(ExperienceCollectable _XP)
    //{
    //    if (XP_LIST.ContainsKey(_XP.currentXP))
    //    {
    //        XP_LIST.Remove(_XP.currentXP);
    //        var x = XP_LIST.ElementAt(0);
    //        x.Value.GetComponent<SpriteRenderer>().color = Color.black;
    //    }
    //}
    public void AddXPToken(ExperienceCollectable _XP)
    {
        ExperienceCollectable tempLast = m_xpSet.Count > 0 ? m_xpSet.Last() : null;

        m_xpSet.Add(_XP);
        if(_XP == m_xpSet.Last())
        {
            m_xpSet.ElementAt(m_xpSet.Count - 1).SetEntityColor(true);
            if(tempLast != null)
            {
                tempLast.SetEntityColor(false);
            }
        }
        
    }
    public void RemoveXPToken(ExperienceCollectable _XP)
    {
        if (m_xpSet.Count >= 2)
        {

            if (_XP == m_xpSet.Last())
            {
                m_xpSet.ElementAt(m_xpSet.Count - 2).SetEntityColor(true);
            }
        }
        _XP.SetEntityColor(false);
        m_xpSet.Remove(_XP);

    }

    public void SetHPBar(bool _isInit = true)
    {
        if (!_isInit)
        {
            SetFillBar(1, m_playerRef.maxHP / m_playerRef.maxHP);
        }
        if (PlayerInfoImageArray[1].fillAmount > 0)
        {
            SetFillBar(1 ,m_playerRef.currentHP / m_playerRef.maxHP);
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
        SetFillBar(0, m_playerRef.CurrentXP / m_playerRef.Max_XP);

        if (m_playerRef.CurrentXP >= m_playerRef.Max_XP)
        {
            SetFillBar(0, 0);
            //add ui too  chose an upgrade
        }
      
    }

    private void SetFillBar(int _index, float _currAmount)
    {
        PlayerInfoImageArray[_index].fillAmount = _currAmount;
    }
  
}
