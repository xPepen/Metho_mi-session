using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity, IHitable
{
    public static Player Instance { get; private set; }
    public PlayerActionsContainer ListOfActions;
    public Weapon MyWeapon;// maybe he can switch weapon?
    public Vector2 Direction;
    
    private InputReceiver mousePos;
    //private void ManagerInfo -> create scriptable manager

    protected override void Init()
    {
        base.Init();
        ListOfActions = new PlayerActionsContainer();
        mousePos = GetComponent<InputReceiver>();
    }
    public void OnHit(float _damage)
    {
        this.currentHP -= _damage;
        if (base.IsDead)
        {
            OnDead();
        }
    }
    public void OnDead()
    {
        //ui
        //try again or quit?
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        PLayerSingleton();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        base.EntityStats.Move(Direction.normalized, m_rb);
        OnShoot();
    }

    private void PLayerSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void OnShoot()
    {
        if (ListOfActions.Contains(PlayerActionsType.SHOOT))
        {
            MyWeapon.Attack(mousePos.MousePosition());
            print(mousePos.MousePosition());
            ListOfActions.ConsumeAllActions(PlayerActionsType.SHOOT);
        }
    }
}
