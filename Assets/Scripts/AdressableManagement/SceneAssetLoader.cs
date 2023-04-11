using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneAssetLoader : Manager<SceneAssetLoader>
{
    [SerializeField] private string m_SceneName;
    public SceneInstance currentScene;

    private AsyncOperationHandle<SceneInstance> m_sceneHandle;
    [SerializeField] private UnityEvent FirstSceneToLoad;
    public void LoadAdressable(string sceneName)
    {
        string label = "Level" + sceneName.Last();
        Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string>() { label },
            x => { }, Addressables.MergeMode.Union);
    }

    protected override void OnStart()
    {
        base.OnStart();
        FirstSceneToLoad.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        UnloadSceneAsync(sceneName);
        LoadSceneAsync(sceneName);
    }

    private void LoadSceneAsync(string name)
    {
        Addressables.LoadSceneAsync(name, UnityEngine.SceneManagement.LoadSceneMode.Additive)
            .Completed += SceneLoader_Completed;
    }
    private void UnloadSceneAsync(string name)
    {
        if (m_sceneHandle.IsValid())
        {
            Addressables.UnloadSceneAsync(m_sceneHandle, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects)
                .Completed+=SceneUnLoader_Completed;
        }
        else
        {
            print("cant unload scene");
        }
    }

    private void SceneLoader_Completed(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        Addressables.LoadSceneAsync(m_SceneName, LoadSceneMode.Additive);
    }

    private void SceneLoader_Completed(AsyncOperationHandle<SceneInstance> handle)
    {
        m_sceneHandle = handle;
        //Addressables.LoadSceneAsync(m_SceneName, LoadSceneMode.Additive);
    }

    private void SceneUnLoader_Completed(AsyncOperationHandle<SceneInstance> obj)
    {
        m_sceneHandle = default;
        //Addressables.UnloadSceneAsync(m_sceneHandle,UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }
    public void MainMenu()
    {
        Addressables.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

}