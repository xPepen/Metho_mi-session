using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneAssetLoader : MainBehaviour
{
    [SerializeField] private string m_SceneName;

    public void LoadAdressable(string sceneName)
    {
        string label = "Level" + sceneName.Last();
        Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string>() { label },
            x => { }, Addressables.MergeMode.Union).Completed += SceneLoader_Completed;
    }
    private void SceneLoader_Completed(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        Addressables.LoadSceneAsync(m_SceneName, LoadSceneMode.Single);
    }
    public void Back()
    {
        Addressables.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}