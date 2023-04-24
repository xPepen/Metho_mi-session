using System.Collections.Generic;

public static class TimerManager
{
    private static List<Timer> m_ListOfTimer = new();

    public static void Subscribe(Timer newTimer)
    {
        if (m_ListOfTimer.Contains(newTimer)) return;
        m_ListOfTimer.Add(newTimer);
    }

    public static void UnSubscribe(Timer newTimer)
    {
        if (m_ListOfTimer.Contains(newTimer)) return;
        m_ListOfTimer.Remove(newTimer);
    }

    public static bool TryStopAllTimer()
    {
        if (m_ListOfTimer.Count == 0) return false;

        m_ListOfTimer.ForEach(timer => timer.PauseTime());
        return true;
    }

    public static bool TryStopAllTimer(TimeType timer)
    {
        if (m_ListOfTimer.Count == 0) return false;
        for (int i = 0; i < m_ListOfTimer.Count; i++)
        {
            ((ITimerControl)m_ListOfTimer[i]).PauseTime(timer);
        }
        return true;
    }

    public static bool TryStartAllTimer(TimeType timer)
    {
        if (m_ListOfTimer.Count == 0) return false;
        for (int i = 0; i < m_ListOfTimer.Count; i++)
        {
            ((ITimerControl)m_ListOfTimer[i]).StartTime(timer);
        }

        return true;
    }

    public static bool TryStartAllTimer()
    {
        if (m_ListOfTimer.Count == 0) return false;

        m_ListOfTimer.ForEach(timer => timer.StartTimer());
        return true;
    }
}