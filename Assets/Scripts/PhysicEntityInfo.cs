using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PhysicEntityInfo : ScriptableObject
{
    public float currentHP;
    public float maxHP;
    public float moveSpeed;

    public void Move(Vector2 _direction, Rigidbody2D _rb)
    {
        var multiplier = _direction * moveSpeed * Time.deltaTime;
        _rb.velocity = multiplier;
    }

    
}
