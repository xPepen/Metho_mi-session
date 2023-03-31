using System;
using UnityEngine;
using UnityEngine.EventSystems;

interface IClickObserver
{
    public void OnClick();
}
public class SimpleButton : MonoBehaviour,IPointerClickHandler
{
    private Action onClick;
    /*public Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }*/

    internal void OnSubscribe(Action _OnClick) => this.onClick += _OnClick;
    internal void OnUnSubscribe(Action _OnClick) => this.onClick -= _OnClick;

    public void OnPointerClick(PointerEventData eventData) => onClick?.Invoke();

    public void OnClick()
    {
        
    }
}
