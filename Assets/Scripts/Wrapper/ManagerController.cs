using UnityEngine;

public class ManagerController : MainBehaviour
{
    [field: SerializeField] public SceneAssetLoader SceneManager { get; private set; }

    protected override void OnStart()
    {
        base.OnStart();
        SceneManager = SceneAssetLoader.Instance;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadLevelAdressable(string name)
    {
        SceneManager.LoadAdressable(name);
    }

  
}