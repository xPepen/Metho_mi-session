using System.Collections.Generic;
using UnityEngine;


public class UpgradeManager : MainBehaviour
{
    [SerializeField] private List<Upgrader> m_UgraderPanel;

    public void ToggleAllPanel(bool _state)
    {
        if (m_UgraderPanel.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < m_UgraderPanel.Count; i++)
        {
            m_UgraderPanel[i].Toggle(_state);
        }
    }
}