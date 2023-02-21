using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    public float CurrentXP { get; private set; }
    public float Max_XP { get; private set; }
    private float m_totalXpGain;
    public float Level;
    public static Player Instance { get; private set; }
    public PlayerActionsContainer ListOfActions{get; private set;}
    [SerializeField] private PhysicEntityInfo EntityStats;
    public Weapon MyWeapon;
    public Vector2 Direction { get; set; }
    
    private InputReceiver mousePos;
    private Animator m_animator;

    public List<Spell> ListOfSpell;
    protected override void Init()
    {
        base.Init();
        maxHP = EntityStats.maxHP;
        currentHP = maxHP;
        speed = EntityStats.moveSpeed;

        ListOfActions = new PlayerActionsContainer();
        mousePos = GetComponent<InputReceiver>();
        m_animator = GetComponent<Animator>();
        ListOfSpell = new List<Spell>();
        Max_XP = 100f;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        PLayerSingleton();
        this.Bind();
        print(this.GetHashCode());
    }
    public void AddXP(float _amount)
    {
        CurrentXP += _amount;
        if(CurrentXP >= Max_XP)
        {
            Level++;
            CurrentXP = 0;
            m_totalXpGain += CurrentXP;
            Max_XP *= 1.25f;
        }
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        Move(Direction.normalized);
        OnShoot();
        SetAnim();
        if(ListOfSpell.Count > 0)
        ListOfSpell.ForEach(spell => { spell.Attack(Vector2.zero); });
    }
    private void SetAnim()
    {
        m_animator.SetFloat("directionX", Direction.normalized.x);
        m_animator.SetFloat("directionY", Direction.normalized.y);
    }
    public override void OnHit(float _damage)
    {
        base.OnHit(_damage);
        D.Get<GameplayManager>().SetHPBar();
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
