using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Upgrader : MainBehaviour, IPointerDownHandler

{
    [SerializeField] private TMPro.TextMeshPro m_text;
    public string m_UpgradeDef;
    public string UpgradeDef => m_UpgradeDef;

    public UnityEvent m_EventOnClick;

    protected override void OnStart()
    {
        base.OnStart();
        m_text.text = m_UpgradeDef;
    }

    public virtual void OnSelected() //sit on the element
    {
        // Background.color = Color.white;
        m_text.color = Color.black;
    }

    public virtual void OnDeselected() //when not on the element
    {
        //Background.color = Color.black;
        m_text.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick)
        {
            // m_EventOnClick.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("yoo");
    }
}