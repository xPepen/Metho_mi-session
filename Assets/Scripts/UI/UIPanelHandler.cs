using UnityEngine;
using UnityEngine.Events;


public class UIPanelHandler : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject[] panels;
    [SerializeField] private int next;
    [SerializeField] private UnityEvent OnQuit;
    public void OnPanelSwitch( int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (index == i)
            {
                ChangeGameObjState(true, panels[i]);
            }
            else if(panels[i].activeSelf == true)
            {
                ChangeGameObjState(false, panels[i]);
            }
        }
    }

    private void ChangeGameObjState(bool _state, UnityEngine.GameObject _obj)
    {
        _obj.SetActive(_state);
    }
    public void OnQuitUIMenu()
    {
        ChangeGameObjState(false, panels[0]);
    }

    public void Quit()
    {
        OnQuit.Invoke();
        Application.Quit();
    }

}
