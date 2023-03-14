using UnityEngine;

public abstract class Weapon : MainBehaviour, IShootable
{
    protected float m_timeWatch;
    [field: SerializeField] public float AttackRate { get; set; }
    protected bool CanAttack => m_timeWatch >= AttackRate ;
    protected void ResetAttack() => m_timeWatch = 0;
    public abstract void Attack(Vector2 _dir);

    public virtual bool CanAttak()
    {
        return m_timeWatch >= AttackRate;
    }
    protected override void OnStart()
    {
        base.OnStart();
        m_timeWatch = AttackRate;
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
