using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Upgrader : MainBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string m_UpgradeName;
    [SerializeField] private TMPro.TextMeshProUGUI m_UpgradeItemName;
    [SerializeField] private TMPro.TextMeshProUGUI m_UpgradeDefinition;

    [SerializeField] private int m_CurrentUpgrade;
    [SerializeField] private List<UpgradeDataInfo> m_UpgradeDataInfo;

    [SerializeField] private UnityEvent m_EventOnClick;
    [SerializeField] private UnityEvent m_EventOnHoverStart;
    [SerializeField] private UnityEvent m_EventOnHoverEnd;

    protected override void OnAwake()
    {
        InitUpgraderPanel();
    }

    private void InitUpgraderPanel()
    {
        if (m_UpgradeName != null)
        {
            this.gameObject.name = m_UpgradeName;
        }

        m_CurrentUpgrade = 0;
        m_UpgradeItemName.text = m_UpgradeName;
        m_UpgradeDefinition.text = m_UpgradeDataInfo[m_CurrentUpgrade].UpgradeDescription;
    }

    public virtual void OnSelected() //sit on the element
    {
        // Background.color = Color.white;
        //m_text.color = Color.black;
    }

    public virtual void OnDeselected() //when not on the element
    {
        //Background.color = Color.black;
        //m_text.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            m_UpgradeDataInfo[m_CurrentUpgrade].UpgradeAction?.Invoke();
            m_EventOnClick?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hello");
        if (eventData.pointerEnter)
        {
            m_EventOnHoverStart?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            m_EventOnHoverEnd?.Invoke();
        }
    }

    public void Toggle(bool _state)
    {
        gameObject.SetActive(_state);
    }
}

[Serializable]
public class UpgradeDataInfo
{
    //public T instance;
    [TextArea] [SerializeField] private string m_UpgradeDescription;
    [SerializeField] private UnityEvent m_UpgradeAction;
    public string UpgradeDescription => m_UpgradeDescription;
    public UnityEvent UpgradeAction => m_UpgradeAction;
}