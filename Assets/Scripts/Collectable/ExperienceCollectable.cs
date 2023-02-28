using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceCollectable : MainBehaviour
{ 
    private const int baseXP = 2;
    private const float SMALL_XP = 4;
    private const float MEDIUM_XP = 8;
    private const float BIG_XP = 12;

    private Player m_playerRef;
    private float m_xpGiven = baseXP;
    private SpriteRenderer m_sprite;
    [SerializeField]private float m_grabRange;
    public float currentXP { get; private set; }
    //manager
    private GameplayManager m_gameRef;
    protected override void OnStart()
    {
        base.OnStart();
        m_playerRef = GameObject.Find("Player").GetComponent<Player>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_gameRef = D.Get<GameplayManager>();
       // currentXP = SetExperienceDrop(GetXpAmount()) + UnityEngine.Random.Range(0,10 + 1);
        //if value exist add 0.001 to the value 
        m_gameRef.AddXPToken(this);
        
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        OnPlayerEnter();
    }
    public void SetEntityColor(bool _mostValue = false )
    {
        var _valueColor = Color.green;
        if (_mostValue)
        {
            m_sprite.color = _valueColor;
            return;
        }
        if(m_sprite.color == _valueColor)
        {
            m_sprite.color = Color.white;
        }
    }

    private float SetExperienceDrop(float _amount)
    {
        return (((int)_amount) + (Player.Instance.Level / 100)) * baseXP;
    }
    private float GetXpAmount() //need to add a random
    {
        return m_playerRef.Level < 5 ? SMALL_XP :
               m_playerRef.Level >= 5 && m_playerRef.Level < 10 ? MEDIUM_XP : BIG_XP;
    }
    private void OnPlayerEnter()
    {
        if (Vector3.Distance(m_playerRef.transform.position, transform.position) <= m_grabRange)
        {
            currentXP = SetExperienceDrop(GetXpAmount()) + UnityEngine.Random.Range(0,10 + 1);
            m_playerRef.AddXP(currentXP);
            m_gameRef.ExperiencePool.Pool.ReAddItem(this);
            m_gameRef.SetExpBar();
            m_gameRef.RemoveXPToken(this);
        }
    }
}

public class ExperienceComparable : IComparer<ExperienceCollectable>
{
    public int Compare(ExperienceCollectable x, ExperienceCollectable y)
    {
        if (x.currentXP != y.currentXP)
        {
            return x.currentXP.CompareTo(y.currentXP);
        }
        return x.GetInstanceID().CompareTo(y.GetInstanceID());
    }
}

