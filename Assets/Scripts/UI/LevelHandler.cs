using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public int LevelToLoad;
    //[SerializeField] private SceneAsset m_sceneGame;

    public void LoadLevel()
    {
        if(!(LevelToLoad > SceneManager.sceneCount))
        {
            SceneManager.LoadScene(LevelToLoad);
            D.Clear();
            return;
        }
        throw new System.Exception("Invalid scene index");
    }
}
