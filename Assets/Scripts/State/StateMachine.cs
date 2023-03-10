using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public BaseState<T> CurrentState;

    public StateMachine(BaseState<T> _startingState)
    {
        this.CurrentState = _startingState;
        this.CurrentState.OnEnterState();
    }
    public void OnUpdate()
    {
        CurrentState.OnUpdateState();
    }
    public void SwitchState(BaseState<T> _newstate)
    {
        CurrentState.OnExitState();
        CurrentState = _newstate;
        CurrentState.OnEnterState();
    }

}
