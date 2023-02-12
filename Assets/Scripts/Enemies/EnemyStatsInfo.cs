using UnityEngine;

[CreateAssetMenu]
public class EnemyStatsInfo : ScriptableObject
{
    public float MaxHP;
    public float moveSpeed;
    public PoolPatern<Enemy> EnemypoolRef;
}
