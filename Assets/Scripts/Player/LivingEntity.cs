using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : BaseEntity,IHitable
{
    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxHP;
    [SerializeField] protected float speed;
   

    protected bool IsDead => currentHP <= 0;
    protected Rigidbody2D m_rb;
    protected virtual void Init()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 _direction)
    {
        var multiplier = _direction * speed * Time.deltaTime;
        m_rb.velocity = multiplier;
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        Init();
    }

    public virtual void OnHit(float _damage)
    {
        this.currentHP -= _damage;
        if (IsDead)
        {
            OnDead();
        }
    }

    public abstract void OnDead();
}
