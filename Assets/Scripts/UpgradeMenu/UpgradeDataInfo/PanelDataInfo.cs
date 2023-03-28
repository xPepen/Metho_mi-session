using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PanelDataContainer
{
    public string MainKey;
    public string Description;
    private static string xy;
    private Dictionary<string, string> test;
    public PanelDataContainer(string mainKey)
    {
        MainKey = mainKey;
        test = new();
    }

    public void Add(string x, string y)
    {
        test.Add(x,y);
    }
    
}
public class PanelDataInfo : MonoBehaviour
{
    public string Key;
    public string Title;
    public string[] keyCompleter; // to complete the current key
    public Dictionary<string, PanelDataContainer> data;
    private PanelDataContainer x = new PanelDataContainer("Test");

    private void Start()
    {
        test();
    }

    public void test()
    {
        x.Add("TestEnglish","englishBlabla");
    }

}


