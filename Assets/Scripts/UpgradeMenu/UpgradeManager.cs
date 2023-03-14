using System.Collections.Generic;
using UnityEngine;


public class UpgradeManager : MainBehaviour
{
    [SerializeField] private List<UpgraderPanel> m_UgraderPanel;

    protected override void OnStart()
    {
        base.OnStart();
        m_UgraderPanel.ForEach(panel => panel.InitUpgradePanel());
    }

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