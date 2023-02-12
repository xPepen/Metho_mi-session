using UnityEngine;

public class SceneData : ScriptableObject
{
    public string SceneName;

    public bool IsSceneNameChange => SceneAsset.name != SceneName;

#if UNITY_EDITOR
    [SerializeField] UnityEditor.SceneAsset sceneAsset;

    public UnityEditor.SceneAsset SceneAsset
    {
        get => sceneAsset;
        set
        {
            sceneAsset = value;
            SceneName = sceneAsset.name;
        }
    }
#endif


}
