using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNode
{
    public GameObject nextPanel;
    public GameObject previous;
}

public class UIPanelHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private int next;
    //public GameObject nextPanel;
    //public GameObject previous;
   
    public void OnPanelSwitch( int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (index == i)
            {
                panels[i].SetActive(true);
            }
            else if(panels[i].activeSelf == true)
            {
                panels[i].SetActive(false);
            }
        }
    }

}
