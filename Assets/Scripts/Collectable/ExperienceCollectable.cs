using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceCollectable : MainBehaviour
{
    [SerializeField] private const int baseXP = 2;
    private const float SMALL_XP = 2;
    private const float MEDIUM_XP = 2;
    private const float BIG_XP = 2;

    private Player m_playerRef;
    private float m_xpGiven = baseXP;
    private SpriteRenderer m_color;
    [SerializeField]private float m_grabRange;
    protected override void OnStart()
    {
        base.OnStart();
        m_playerRef = GameObject.Find("Player").GetComponent<Player>();
        m_color = GetComponent<SpriteRenderer>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        OnPlayerEnter();
    }
    public void SetEntity(float _xp, Color _color)
    {
        if(_xp != m_xpGiven || _color != m_color.color)
        {
             m_xpGiven = _xp;
             m_color.color = _color;
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
            m_playerRef.CurrentXp += SetExperienceDrop(GetXpAmount());
            print(m_playerRef.CurrentXp);
            GameplayManager.Instance.ExperiencePool.Pool.ReAddItem(this);
        }
    }
}

