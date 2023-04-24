using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomateUser : MainBehaviour
{
    public Player PlayerRef;
    public List<UpgraderPanel> UIUpgrade;
    public Button NextLevel;
    public GameplayState currentState;
    public bool AutoPlay;
    public GameplayController Controller;

    private Timer m_Timer;

    private Enemy m_currentTarget;

    public float enemyDectectionRadius = 10f;

    public bool Killable;

    public Vector2 Direction = Vector2.zero;

    protected override void OnAwake()
    {
        base.OnAwake();
        if (!AutoPlay) return;
        m_Timer = new Timer(CalculateNextDirection, 3.5f , TimeType.Delta);
        TimerManager.TryStopAllTimer(TimeType.Delta);
           
        PlayerRef.IsAutoPlay = AutoPlay;
        PlayerRef.IsGodMode = Killable;
    }

    protected override void OnUpdate()
    {
        if (!AutoPlay) return;
        base.OnUpdate();
        if (!AutoPlay) return;
        SetTimer();
        //shoot , move
        if (GameState() == GameplayState.Gameplay)
        {
            PlayerRef.OnShoot(CheckForEnemy());
            PlayerRef.Move(Direction);
        }
        //choose a upgrade
        else if (GameState() == GameplayState.Upgrade)
        {
            if (UIUpgrade.Count == 0)
            {
                Controller.UpdateState("Gameplay");
            }
            var index = Random.Range(0, UIUpgrade.Count);
            if (UIUpgrade[index] == null)
            {
                UIUpgrade.RemoveAt(index);
            }

            index = Random.Range(0, UIUpgrade.Count);
            UIUpgrade[index].OnPointerDown();
        }
        //next level 
        else if (GameState() == GameplayState.NextLevel)
        {
            NextLevel.onClick.Invoke();
        }

        //dead
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (GameState() == GameplayState.Gameplay)
        {
            PlayerRef.Move(Direction);
        }
    }

    private void SetTimer()
    {
        if (GameState() != GameplayState.Gameplay)
        {
            m_Timer.PauseTime();
        }
        else
        {
            m_Timer.StartTimer();
        }

        m_Timer.OnUpdate();
    }

    private void CalculateNextDirection()
    {
        var hit = Physics2D.CircleCast(transform.position, 15f, Vector2.zero,
            LayerMask.NameToLayer("Experience"));
        if (hit)
        {
            Direction = (hit.transform.position - transform.position).normalized;

            return;
        }

        var dirX = Random.Range(-1f, 1.000001f);
        var dirY = Random.Range(-1f, 1.000001f);
        var dir = new Vector2(dirX, dirY);
        this.Direction = dir.normalized;
    }

    private Vector2 CheckForEnemy()
    {
        if (m_currentTarget != null && m_currentTarget.gameObject.activeSelf) return m_currentTarget.transform.position;

        var hit = Physics2D.CircleCast(transform.position, enemyDectectionRadius, Vector2.zero,
            LayerMask.NameToLayer("Enemy"));
        if (hit.transform.TryGetComponent(out Enemy enemy))
        {
            if (m_currentTarget == null || !m_currentTarget.gameObject.activeSelf)
            {
                m_currentTarget = enemy;
            }

            return m_currentTarget.transform.position;
        }

        return Vector2.zero;
    }

    private GameplayState GameState() => Controller.GetState();
}