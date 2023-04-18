using UnityEngine;

public class SceneManagerController : MainBehaviour
{
    [field: SerializeField] public SceneAssetLoader SceneManager { get; private set; }
    [field: SerializeField] public GameplayController GameController { get; private set; }
    protected override void OnStart()
    {
        base.OnStart();
        SceneManager = SceneAssetLoader.Instance;
        if (GameController == null)
        {
            GameController =  FindObjectOfType<GameplayController>();
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadLevelAdressable(string name)
    {
        SceneManager.LoadAdressable(name);
    }

    public void SetGameState(string state)
    {
        GameController.UpdateState(state);
    }

  
}