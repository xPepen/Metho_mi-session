using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameplayManager : Manager<GameplayManager>
{
    [field: SerializeField] public PoolHandler<ExperienceCollectable> ExperiencePool { get; private set; }

    private Player m_playerRef;
    protected override void OnStart()
    {
        base.OnStart();
        InitPlayer();
        ExperiencePool.InitPool();
    }



    private void InitPlayer()
    {
        if (!GameObject.Find("Player"))
        {
            throw new Exception("The Player GameObject isnt in the scene");
        }
        else
        {
            m_playerRef = GameObject.Find("Player").GetComponent<Player>();

        }
    }
}
