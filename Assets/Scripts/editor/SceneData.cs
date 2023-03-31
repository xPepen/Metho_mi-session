using UnityEngine;

public class SceneData : ScriptableObject
{
    public string SceneName;


    #if UNITY_EDITOR
    [SerializeField] UnityEditor.SceneAsset sceneAsset;
    public bool IsSceneNameChange => sceneAsset.name != SceneName;

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
