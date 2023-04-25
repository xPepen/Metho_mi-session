using System;
using UnityEngine;

public class Timer : IDisposable, ITimerControl
{
    public delegate void TimerEvent();
    private event TimerEvent m_TimerEvent;

   // private Action m_OnTimerCompleted;
    private float m_TimeForNextAction;
    private float m_TimeWatch;

    private bool m_CanUpdate;
    private bool m_IsTimerFinish;
    private bool m_IsTimerModifiable;

    private TimeType m_TimeType;

    public float GetCurrentDuration => m_TimeForNextAction;
    public float CurrentTime => m_TimeWatch;

    public TimeType TimeType => m_TimeType;

    public Timer(TimerEvent onTimerEnd, float duration, TimeType timeType, bool durationModifiable = false)
    {
        m_TimerEvent += onTimerEnd;
        m_TimeWatch = 0f;
        m_TimeForNextAction = duration;
        m_TimeType = timeType;
        m_CanUpdate = true;
        m_IsTimerModifiable = durationModifiable;
        TimerManager.Subscribe(this);
    }
    public void OnUpdate()
    {
        if (!m_CanUpdate) return;
        m_TimeWatch += GetTimeScaler();
        if (m_TimeWatch >= m_TimeForNextAction)
        {
            m_IsTimerFinish = true;
            if (m_TimerEvent == null) return;
            m_TimerEvent?.Invoke();
            ResetTimer();
        }
    }

    public void SubscribeEvent(TimerEvent newAction, bool isOneShotAction)
    {
        if (newAction == null) return;
        if (isOneShotAction)
        {
            m_TimerEvent += ()=>
            {
                newAction.Invoke();
                m_TimerEvent -= newAction;
            };
        }
        m_TimerEvent += newAction;
    }
    public void UnscribeEvent(TimerEvent newAction)
    {
        if (newAction == null) return;
        m_TimerEvent -= newAction;
    }

    public void OverrideAllEvent(TimerEvent newAction)
    {
        ClearAllEvent();
        m_TimerEvent += newAction;

    }
    public void ClearAllEvent()
    {
        if (m_TimerEvent == null) return;
        m_TimerEvent  = null;
    }

    private float GetTimeScaler() => m_TimeType == TimeType.Delta ? Time.deltaTime : Time.fixedDeltaTime;

    public void ChangeDuration(float newTime)
    {
        if (!m_IsTimerModifiable || Math.Abs(newTime - m_TimeForNextAction) == 0) return;

        m_TimeForNextAction = newTime;
    }

    public void ToggleTimer(bool isActive)
    {
        if (m_CanUpdate == isActive) return;
        m_CanUpdate = isActive;
    }

    public void PauseTime()
    {
        m_CanUpdate = false;
    }
    
    public void StartTimer()
    {
        m_CanUpdate = true;
    }
    
    public bool IsTimerFinish()
    {
        if (m_IsTimerFinish)
        {
            ResetTimer();
            return true;
        }
        return false;
    }

    private void ResetTimer()
    {
        m_IsTimerFinish = false;
        m_TimeWatch = 0.00f;
    }
    
    void ITimerControl.PauseTime(TimeType type)
    {
        if (m_TimeType != type) return;
        m_CanUpdate = false;
    }

    void ITimerControl.StartTime(TimeType type)
    {
        if (m_TimeType != type) return;
        m_CanUpdate = true;
    }
    public void Dispose()
    {
        TimerManager.UnSubscribe(this);
       ClearAllEvent();
    }
}