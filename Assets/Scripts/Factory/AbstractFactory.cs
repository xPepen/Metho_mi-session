public abstract class AbstractFactory<T> : MainBehaviour
{
    public T Entity;
    public abstract UnityEngine.GameObject CreateEnemy();
    public PoolHandler<Enemy> Pool;
}