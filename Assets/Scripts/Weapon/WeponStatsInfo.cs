using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WeponStatsInfo :ScriptableObject
{
    public float Damage;
    public float Speed;
    public bool haveRecoil;
    public float HittableRadius;
    public float HittableDistance;

}
