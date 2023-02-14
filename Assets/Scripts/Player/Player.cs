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
    public Weapon MyWeapon;
    public Vector2 Direction { get; set; }
    
    private InputReceiver mousePos;
    private Animator m_animator;
    protected override void Init()
    {
        base.Init();
        maxHP = EntityStats.maxHP;
        currentHP = maxHP;
        speed = EntityStats.moveSpeed;

        ListOfActions = new PlayerActionsContainer();
        mousePos = GetComponent<InputReceiver>();
        m_animator = GetComponent<Animator>();
    }

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
        SetAnim();
    }
    private void SetAnim()
    {
        m_animator.SetFloat("directionX", Direction.normalized.x);
        m_animator.SetFloat("directionY", Direction.normalized.y);
    }
    public override void OnHit(float _damage)
    {
        base.OnHit(_damage);
        GameplayManager.Instance.SetHPBar();
        print(currentHP);
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
        }
    }

    public override void OnDead()
    {
    }
}
