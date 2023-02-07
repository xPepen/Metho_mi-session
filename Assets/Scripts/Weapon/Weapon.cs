using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MainBehaviour, IShootable
{
    protected float m_timeWatch;
    [SerializeField] private float m_attackRate;
    public bool CanAttack => m_timeWatch >= m_attackRate;

    public abstract void Attack(Vector2 _dir);
    
    protected override void OnUpdate()
    {
        base.OnUpdate();
        m_timeWatch += Time.deltaTime;
    }
}
