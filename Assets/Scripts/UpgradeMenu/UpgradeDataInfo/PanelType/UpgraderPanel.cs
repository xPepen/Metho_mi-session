using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public abstract class UpgraderPanel : MainBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected UpgradeManager m_PanelManager;

    protected string m_TitleKey;
    protected string m_DescriptionKey;


    [SerializeField] protected TMPro.TextMeshProUGUI m_TxtUpgradeItemName;

    [SerializeField] protected TMPro.TextMeshProUGUI m_TxtUpgradeDefinition;

    private const string m_preTextDef = "DefintionText";

    //Set UI color + Image
    [SerializeField] private Color m_baseColor;
    [SerializeField] private Color m_selectedColor;
    private UnityEngine.UI.Image m_PanelImage;
    protected Action m_CurrentUpgradeAction;
    protected Action m_ActionRef;

    protected Queue<UpgradeNode> m_QueueOfUpgrade;


    protected bool isActionCompleted;
    protected int m_CurrentUpgrade;
    protected const int MAX_UPGRADE = 5;

    //Generale Click Event
    [SerializeField] private UnityEvent m_EventOnClick;
    [SerializeField] private UnityEvent m_EventOnHoverStart;
    [SerializeField] private UnityEvent m_EventOnHoverEnd;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_PanelImage = GetComponentInChildren<UnityEngine.UI.Image>();
        isActionCompleted = true;
        InitUpgradeNodeQueue();
    }

    protected override void OnStart()
    {
        base.OnStart();
        InitPanelInfo();
    }
  
    public void InitUpgradePanel()
    {
        if (!isActionCompleted || m_CurrentUpgrade > MAX_UPGRADE)
        {
            return;
        }

        var node = GetFirstNode(out isActionCompleted);
        
        var correctDescription = GetCorrectLangText(m_DescriptionKey);
        
        var descriptionText = node.UseFloatValue ? $"{node.UpgradeValue} " + correctDescription
            : node.UseStringValue ? node.UpgradeDefinition : correctDescription;

        SetDescription(descriptionText);

        if (node?.UpgradeAction != null)
        {
            OnSubscribeAction(node.UpgradeAction);
        }

        if (m_CurrentUpgrade == MAX_UPGRADE)
        {
            m_PanelManager.RemovePanel(this);
            Destroy(gameObject, 2f);
        }
    }

    protected abstract void InitUpgradeNodeQueue();

    protected void SetKeys(string titleKey, string descriptionkey)
    {
        m_TitleKey = titleKey;
        m_DescriptionKey = descriptionkey;
    }

    public void SetUpgraddeManager(UpgradeManager _manager)
    {
        m_PanelManager = _manager;
    }

   

    private void InitPanelInfo()
    {
        CheckOpacityColor(ref m_baseColor, ref m_selectedColor);
        OnColorChange(m_baseColor);


        gameObject.name = m_TxtUpgradeItemName.text = GetCorrectLangText(m_TitleKey, true);
       // m_TxtUpgradeDefinition.text = GetCorrectLangText(m_DescriptionKey);
    }


    protected UpgradeNode GetFirstNode(out bool _ActionState)
    {
        _ActionState = false;
        if (m_QueueOfUpgrade.Count == 0)
        {
            return new UpgradeNode(null, "UpgradeMax");
        }

        m_CurrentUpgrade++; //add UI TEXT + show ui current upgrade
        return m_QueueOfUpgrade.Dequeue();
    }


    protected string GetCorrectLangText(ReadOnlySpan<char> key, bool isTitle = false)
    {
        return isTitle
            ? m_PanelManager.GetValueFromDictionary(key.ToString())
            : m_PanelManager.GetValueFromDictionary(key.ToString() + m_CurrentUpgrade);
    }

    protected void SetUITitleText(string text)
    {
        m_TxtUpgradeItemName.text = text;
    }

    protected void SetDescription(string _upgradeAction)
    {
        m_TxtUpgradeDefinition.text = m_PanelManager.GetValueFromDictionary(m_preTextDef)+" " + _upgradeAction;
    }

    protected void SetDescription(float _value, string _upgradeAction)
    {
         m_TxtUpgradeDefinition.text =
            m_PanelManager.GetValueFromDictionary(m_preTextDef) + $" {_value} " + _upgradeAction;
    }

    public void UpdateAllTexts()
    {
        m_TxtUpgradeItemName.text = GetCorrectLangText(m_TitleKey, true);
        m_TxtUpgradeDefinition.text = GetCorrectLangText(m_DescriptionKey);
    }


    public void CheckOpacityColor(ref Color _baseColor, ref Color _selectColor)
    {
        if (_baseColor.a <= 0)
        {
            _baseColor.a = 0.75f;
        }

        if (_selectColor.a <= 0)
        {
            _selectColor.a = 0.75f;
        }
    }

    protected virtual void OnColorChange(Color _color) => m_PanelImage.color = _color;

// ----------------------- Unity Event ------------------------------- 
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            if (m_EventOnClick is null) return;

            m_CurrentUpgradeAction.Invoke();
            OnUnSubscribeAction(m_ActionRef);
            m_EventOnClick?.Invoke();
            isActionCompleted = true;
            InitUpgradePanel();
        }
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            m_EventOnHoverStart?.Invoke();
            OnColorChange(m_selectedColor);
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            m_EventOnHoverEnd?.Invoke();
            OnColorChange(m_baseColor);
        }
    }

    public void Toggle(bool _state)
    {
        gameObject.SetActive(_state);
    }

//------------------------- Action  -----------------------------------
    protected void OnSubscribeAction(Action _currentAction, ref float _inputValue, float _increment)
    {
        m_CurrentUpgradeAction += _currentAction;
        _inputValue += _increment;
        m_ActionRef = _currentAction;
    }

    protected void OnSubscribeAction(Action _currentAction)
    {
        m_CurrentUpgradeAction += _currentAction;
        m_ActionRef = _currentAction;
    }

    protected void OnUnSubscribeAction(Action _currentAction)
    {
        m_CurrentUpgradeAction -= _currentAction;
        m_ActionRef = delegate { };
        Clear();
    }

    protected void Clear() => m_CurrentUpgradeAction = delegate { };
}