using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomButton :  MainBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler
{
    [SerializeField] private Image m_Image; 
    
    public Color DefaultColor = new Color(0.3f, 1, 1);
    public Color HoverColor = new Color(0.3f, 1, 1);
    public Color ClickColor = new Color(0.3f, 1, 1);
    public Color ReleaseColor = new Color(0.3f, 1, 1);
   
    public UnityEvent OnEventEnter;
    public UnityEvent OnEventDown;
    public UnityEvent OnEventUp;
    public UnityEvent OnEventExit;

    [SerializeField] private bool m_Interactable;

    private void OnValidate()
    {
        if (!m_Image) m_Image = GetComponent<Image>();
        m_Image.color = DefaultColor;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        m_Image = GetComponent<Image>();
    }

    private bool Enable()
    {
        return this.enabled;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!Enable() || !m_Interactable)
            return;
        OnEventEnter?.Invoke();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!Enable() || !m_Interactable)
            return;
        OnEventDown?.Invoke();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (!Enable() || !m_Interactable)
            return;
        OnEventUp?.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!Enable() || !m_Interactable)
            return;
        OnEventExit?.Invoke();
    }
}