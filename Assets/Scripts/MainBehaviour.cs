using System.Collections;
using System.Collections.Generic;
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
        OnUpdate();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }


    /// <summary>
    /// Do not use Monobehaviour Awake
    /// </summary>
    protected virtual void OnAwake() { }
    /// <summary>
    /// Do not use Monobehaviour Start
    /// </summary>
    protected virtual void OnStart() { }
    /// <summary>
    /// Do not use Monobehaviour Update
    /// </summary>
    protected virtual void OnUpdate() { }
    /// <summary>
    /// Do not use Monobehaviour FixedUpadate
    /// </summary>
    protected virtual void OnFixedUpdate() { }
}
