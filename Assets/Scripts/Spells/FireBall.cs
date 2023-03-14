using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell
{
    public override void Attack(Vector2 _dir)
    {
    }

    protected override void OnVisualScaleChange()
    {
    }

    public override void OnRadiusUpgrade(float _value)
    {
    }

    public override void OnDamageUpgrade(float _value)
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackRateUpgrade(float _value)
    {
    }

    public override void OnTargetCountUpgrade(float _increment)
    {
    }
}
