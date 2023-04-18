using UnityEngine;

public class MainBehaviour : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        /*if (D.Get<GameplayManager>() && D.Get<GameplayManager>().IsGamePause)
        {
            return;
        }*/

        OnUpdate();
    }

    private void FixedUpdate()
    {/*
        if (D.Get<GameplayManager>() && D.Get<GameplayManager>().IsGamePause)
        {
            return;
        }*/

        OnFixedUpdate();
    }


    /// <summary>
    /// Do not use Monobehaviour Awake
    /// </summary>
    protected virtual void OnAwake()
    {
    }

    /// <summary>
    /// Do not use Monobehaviour Start
    /// </summary>
    protected virtual void OnStart()
    {
    }

    /// <summary>
    /// Do not use Monobehaviour Update
    /// </summary>
    protected virtual void OnUpdate()
    {
    }

    /// <summary>
    /// Do not use Monobehaviour FixedUpadate
    /// </summary>
    protected virtual void OnFixedUpdate()
    {
    }
}