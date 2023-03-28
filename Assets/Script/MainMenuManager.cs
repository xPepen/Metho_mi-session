using System;
using UnityEngine;

public static class Key_playerPrefs
{
    public static readonly string IsConnect = "IsPlayerLogIn";
}
[Serializable]
public class node
{
    private GameObject prev;
    private GameObject next;
    
}
//controller setting menu
public class MainMenuManager : Manager<MainMenuManager>
{
//laod scene
//option volume
//set language
//quit game
//switch panel
    private void OnQuitGame()
    {
        PlayerPrefs.DeleteKey(Key_playerPrefs.IsConnect);
        Application.Quit();
    }
   
}
