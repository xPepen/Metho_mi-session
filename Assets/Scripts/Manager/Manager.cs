
public class Manager<T> : MainBehaviour where T: Manager<T>
{
    public static T Instance { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        if(Instance == null)
        {
            Instance = GetComponent<T>();
        }
        else
        {
            Destroy(Instance);
        }

    }
}
