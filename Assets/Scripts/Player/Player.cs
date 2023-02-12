using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    public float CurrentXp;
    public float Level;
    public static Player Instance { get; private set; }
    public PlayerActionsContainer ListOfActions{get; private set;}
    [SerializeField] private PhysicEntityInfo EntityStats;
    public Weapon MyWeapon;// maybe he can switch weapon?
    public Vector2 Direction { get; set; }
    
    private InputReceiver mousePos;
    //private void ManagerInfo -> create scriptable manager

    protected override void Init()
    {
        base.Init();
        maxHP = EntityStats.maxHP;
        currentHP = maxHP;
        speed = EntityStats.moveSpeed;

        ListOfActions = new PlayerActionsContainer();
        mousePos = GetComponent<InputReceiver>();
    }
    //public void OnHit(float _damage)
    //{
    //    this.currentHP -= _damage;
    //    if (base.IsDead)
    //    {
    //        OnDead();
    //    }
    //}
    //public void OnDead()
    //{
    //    //ui
    //    //try again or quit?
    //}

    protected override void OnAwake()
    {
        base.OnAwake();
        PLayerSingleton();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        Move(Direction.normalized);
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
            if (MyWeapon)
            {
                (MyWeapon as IShootable).Attack(mousePos.MousePosition());
            }
            //MyWeapon.Attack(mousePos.MousePosition());
        }
    }

    public override void OnDead()
    {
        throw new System.NotImplementedException();
    }
}
