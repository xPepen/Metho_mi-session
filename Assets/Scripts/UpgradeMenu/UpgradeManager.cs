using System;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeManager : MainBehaviour, ITranslateBehaviour
{
    [SerializeField] private List<UpgraderPanel> m_UgraderPanel;
    private FileReader_TSV m_FileReader;
    [SerializeField] private string m_Filename;
    [SerializeField] private TranslatorManager m_TranslatorManager;

    protected override void OnAwake()
    {
        base.OnAwake();
        SetNewTextReader();
    }

    protected override void OnStart()
    {
        base.OnStart();
        InitListOfPanels();
        ToggleAllPanel(false);
    }

    public void InitListOfPanels()
    {
        for (int i = 0; i < m_UgraderPanel.Count; i++)
        {
            m_UgraderPanel[i].InitUpgradePanel();
            m_UgraderPanel[i].SetUpgraddeManager(this);
            
        }
    }

    private void UpdateLangPanel()
    {
        if (m_UgraderPanel.Count == 0)
        {
            print("List is empty cannot init Panel Manager");
            return;
        }

        for (int i = 0; i < m_UgraderPanel.Count; i++)
        {
            //add update text function
        }
    }

    public void ToggleAllPanel(bool _state)
    {
        for (int i = 0; i < m_UgraderPanel.Count; i++)
        {
            m_UgraderPanel[i].Toggle(_state);
        }
    }

    public void RemovePanel(UpgraderPanel _panel) => m_UgraderPanel.Remove(_panel);

    public void SetNewTextReader()
    {
        m_FileReader = new FileReader_TSV(m_Filename, m_TranslatorManager.GetCurrentLanguage());
        m_TranslatorManager.Subscribe(this);
    }

    public string GetValueFromDictionary(ReadOnlySpan<char> key)
    {
        var currentLang = m_TranslatorManager.GetCurrentLanguage();
        return m_FileReader.GetValueWithCurrentKey(key.ToString() + currentLang);
    }

    public void UpdateText()
    {
        for (int i = 0; i < m_UgraderPanel.Count; i++)
        {
            m_UgraderPanel[i].UpdateAllTexts();
        }
    }
}