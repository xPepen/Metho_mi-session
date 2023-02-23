using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleProjectileWeapon : RangeWeapon
{
    [SerializeField] protected int MaxProjectilePerAttack = 1;
    private int CurrentMaxProjectile;
    private bool IsMaxProjectile => CurrentMaxProjectile > MaxProjectilePerAttack;
    
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (CanAttack)
        {
            CurrentMaxProjectile = 0;
        }
    }
    public override void Attack(Vector2 _dir)
    {
        float angleToUpdate = 180f / MaxProjectilePerAttack;
        float radius = 5f;
        float angle = 0f;
       

        for (int i = 0; i < MaxProjectilePerAttack; i++)
        {
            float _xDir = _dir.x  + Mathf.Sin((angle * Mathf.PI) / 90f) * radius;
            float _yDir = _dir.y  + Mathf.Cos((angle * Mathf.PI) / 90f) * radius;
            base.Attack(new Vector2(_xDir, _yDir));
            angle += 25f;
            CurrentMaxProjectile++;
        }
            
    }

    public override bool CanAttak()
    {
        return m_timeWatch >= m_attackRate || CurrentMaxProjectile < MaxProjectilePerAttack;
    }
}
