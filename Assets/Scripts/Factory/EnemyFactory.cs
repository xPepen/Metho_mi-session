public abstract class EnemyFactory <T> : AbstractFactory where T : EnemyFactory<T>
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
        var _copy = Pool.Pool.GetNextItem().gameObject;
        if (_copy.TryGetComponent(out Enemy _enemy) && _enemy.m_RePool == null)
        {
            _enemy.m_RePool = () => Pool.Pool.ReAddItem(_enemy);
        }

        return _copy;
    }
}
