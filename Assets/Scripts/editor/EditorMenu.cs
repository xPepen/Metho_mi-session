#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorMenu : MonoBehaviour
{
    [MenuItem("Tool/Create Scene Assets")]
    static void CreateSceneAssets()
    {
        foreach(var sceneAsset in GetSceneAssets())
        {
            var sceneData = ScriptableObject.CreateInstance<SceneData>();
            sceneData.SceneAsset = sceneAsset;
            string assetPath = Path.Combine("Assets", "ScriptableObjects", sceneData.SceneName + ".asset");
            AssetDatabase.CreateAsset(sceneData, assetPath);
        }
    }
    [MenuItem("Tool/UpdateSceneAssets")]
    static void UpdateSceneAssets()
    {
        foreach(var sceneData in GetSceneData())
        {
            //var sceneData = ScriptableObject.
            if (sceneData.IsSceneNameChange)
            {
                sceneData.SceneName = sceneData.SceneAsset.name;
                EditorUtility.SetDirty(sceneData);
                var path = AssetDatabase.GetAssetPath(sceneData);
                AssetDatabase.RenameAsset(path, sceneData.SceneName);
                AssetDatabase.SaveAssets();
            }
            //string assetPath = Path.Combine("Assets", "ScriptableObjects", sceneData.SceneName + ".asset");
            //AssetDatabase.CreateAsset(sceneData, assetPath);
        }
    }
    static string[] searchScriptable = new[] { "Assets/ScriptableObjects/" };
    static string[] searchInFolders = new[] { "Assets/Scenes/" };

    static List<SceneAsset> GetSceneAssets()
    {
        string[] sceneGuids = AssetDatabase.FindAssets("t:SceneAsset", searchInFolders);
        var sceneAssets = new List<SceneAsset>();
        foreach (var sceneGuid in sceneGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath));
        }
        return sceneAssets;
    }
    static List<SceneData> GetSceneData() 
    { 
        string[] sceneGuids = AssetDatabase.FindAssets("t:SceneData", searchScriptable); 
        var sceneAssets = new List<SceneData>(); 
        foreach (var sceneGuid in sceneGuids) 
        { 
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGuid); 
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<SceneData>(assetPath)); 
        } 
        return sceneAssets; 
    }
}
#endif
