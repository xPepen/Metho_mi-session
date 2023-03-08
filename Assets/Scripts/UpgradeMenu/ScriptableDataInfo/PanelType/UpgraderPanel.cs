using System;
using System.Collections.Generic;
using TNRD;
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
    
    protected Action m_CurrentUpgradeAction;
    protected Action m_ActionRef;
    
    
    //Generale Click Event
    [SerializeField] private UnityEvent m_EventOnClick;
    [SerializeField] private UnityEvent m_EventOnHoverStart;
    [SerializeField] private UnityEvent m_EventOnHoverEnd;

    //test 
    protected override void OnAwake()
    {
      
        InitText();
    }

    private void InitText()
    {
        this.gameObject.name = m_UpgradeItemName;
        m_TxtUpgradeItemName.text = m_UpgradeItemName;
        m_TxtUpgradeDefinition.text = m_UpgradeItemDefinition;
    }
    protected void SetDescription(string _upgradeAction)
    {
        m_TxtUpgradeDefinition.text = $"This upgrade give you  {_upgradeAction}" ;
    } 
    protected void SetDescription(float _value, string _upgradeAction)
    {
        m_TxtUpgradeDefinition.text = $"This upgrade give you {_value} {_upgradeAction}";
    }
    public abstract void InitUpgradePanel();

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
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
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