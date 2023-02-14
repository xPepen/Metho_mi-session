using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T>
{
    protected T currentEntity;
    public BaseState(T _controlledEntity)
    {
        this.currentEntity = _controlledEntity;
    }

    //quest ce qui est excuter quand je suis dans mon state
    public abstract void OnUpdateState();


    //quest ce qui est excuter quand je suis dans mon state
    public abstract void OnEnterState();
    public abstract void OnExitState();



}
