using UnityEngine;

public abstract class Weapon : MainBehaviour, IShootable
{
    protected float m_timeWatch;
    [SerializeField] protected float m_attackRate;
    protected bool CanAttack => m_timeWatch >= m_attackRate ;
    protected void ResetAttack() => m_timeWatch = 0;
    public abstract void Attack(Vector2 _dir);

    public virtual bool CanAttak()
    {
        return m_timeWatch >= m_attackRate;
    }
    protected override void OnStart()
    {
        base.OnStart();
        m_timeWatch = m_attackRate;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (!CanAttack)
        {
            m_timeWatch += Time.deltaTime; 
        }
    }
}
