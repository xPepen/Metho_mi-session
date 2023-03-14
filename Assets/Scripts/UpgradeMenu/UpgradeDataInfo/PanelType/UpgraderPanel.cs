using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//add upgrader for each type of entity
//add interface and then add an event that change the imput value
public abstract class UpgraderPanel : MainBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    //definition by string
    [SerializeField] protected string m_UpgradeItemName;
    [SerializeField] protected string m_UpgradeItemDefinition;
    [SerializeField] protected TMPro.TextMeshProUGUI m_TxtUpgradeItemName;

    [SerializeField] protected TMPro.TextMeshProUGUI m_TxtUpgradeDefinition;

    //Set UI color + Image
    [SerializeField] private Color m_baseColor;
    [SerializeField] private Color m_selectedColor;
    private UnityEngine.UI.Image m_PanelImage;
    protected Action m_CurrentUpgradeAction;
    protected Action m_ActionRef;


    //Generale Click Event
    [SerializeField] private UnityEvent m_EventOnClick;
    [SerializeField] private UnityEvent m_EventOnHoverStart;
    [SerializeField] private UnityEvent m_EventOnHoverEnd;

    public abstract void InitUpgradePanel();

    protected override void OnAwake()
    {
        m_PanelImage = GetComponentInChildren<UnityEngine.UI.Image>();
        InitPanelInfo();
    }

    private void InitPanelInfo()
    {
        CheckOpacityColor(ref m_baseColor, ref m_selectedColor);
        OnColorChange(m_baseColor);

        gameObject.name = m_TxtUpgradeItemName.text = m_UpgradeItemName;
        m_TxtUpgradeDefinition.text = m_UpgradeItemDefinition;
    }

    protected void SetDescription(string _upgradeAction)
    {
        m_TxtUpgradeDefinition.text = $"This upgrade give you  {_upgradeAction}";
    }

    protected void SetDescription(float _value, string _upgradeAction)
    {
        m_TxtUpgradeDefinition.text = $"This upgrade give you {_value} {_upgradeAction}";
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
            m_CurrentUpgradeAction.Invoke();
            OnUnSubscribeAction(m_ActionRef);
            m_EventOnClick?.Invoke();
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