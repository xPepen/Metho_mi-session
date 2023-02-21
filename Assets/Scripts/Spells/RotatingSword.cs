using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSwordHandler : Spell
{
    [SerializeField] private int SwordCount;
    //private List<RotatingSword>
    public override void Attack(Vector2 _dir)
    {
    }
}


public class RotatingSword : Spell
{
    public override void Attack(Vector2 _dir)
    {
    }
}
