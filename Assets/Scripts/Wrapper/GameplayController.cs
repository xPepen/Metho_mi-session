using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayController : MainBehaviour
{
    private GameplayState m_currentState;

    [Header("UnityEvent")] [SerializeField]
    private UnityEvent MainMenuEvent;

    [SerializeField] private UnityEvent GameplayEvent;
    [SerializeField] private UnityEvent UpgradeEvent;
    [SerializeField] private UnityEvent DeadMenuEvent;
    [SerializeField] private UnityEvent GameOptionEvent;

    protected override void OnAwake()
    {
        base.OnAwake();
        UpdateState("MainMenu");
    }

    private bool TrySetState(out GameplayState currentState, GameplayState newState)
    {
        if (m_currentState == newState)
        {
            currentState = m_currentState;
            return false;
        }

        currentState = newState;
        return true;
    }


    public void UpdateState(string stateName)
    {
        
        if ((Enum.TryParse(stateName, out GameplayState code) && !TrySetState(out m_currentState,code )))
        {
            return;
        }
        //Debug.LogError(m_currentState);
        switch (m_currentState)
        {
            case GameplayState.MainMenu:
                MainMenuEvent.Invoke();
                break;
            case GameplayState.Gameplay:
                GameplayEvent.Invoke();
                break;
            case GameplayState.Upgrade:
                UpgradeEvent.Invoke();
                break;
            case GameplayState.DeadMenu:
                DeadMenuEvent.Invoke();
                break;
            case GameplayState.GameOption:
                GameOptionEvent.Invoke();
                break;
            default:
                Debug.LogError("NO STATE SET");
                break;
        }
    }
}