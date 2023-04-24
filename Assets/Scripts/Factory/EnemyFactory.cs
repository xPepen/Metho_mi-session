public abstract class EnemyFactory <T> : AbstractFactory<Enemy> where T : EnemyFactory<T>
{
   
    public static T Instance { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        if (Instance == null)
        {
            Instance = GetComponent<T>();
        }
        else
        {
            Destroy(Instance);
        }
        this.Pool.InitPool();
    }
    public override UnityEngine.GameObject CreateEnemy()
    {
        var pooling = Pool.Pool.GetNextItem();
        
        Entity = pooling;

        return pooling.gameObject;
    }
}
