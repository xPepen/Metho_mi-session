using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : LivingEntity, IUpgradeblePlayerStats
{
    public static Player Instance { get; private set; }

    public float CurrentXP { get; private set; }
    public float Max_XP { get; private set; }
    private float m_totalXpGain;
    public int Level;
    public UnityEvent OnLevelUp;
    public PlayerActionsContainer ListOfActions { get; private set; }
    [SerializeField] private PhysicEntityInfo EntityStats;

    //Ishootable section
    public Vector2 InputDir { get; set; }

    public Weapon MyWeapon;
    public List<Spell> ListOfSpell;
    [SerializeField] private Transform m_SpellParent;

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
        ListOfSpell = new List<Spell>();
        Max_XP = 100f;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        PLayerSingleton();
        this.Bind();
    }

    public void AddXP(float _amount)
    {
        CurrentXP += _amount;
        if (CurrentXP >= Max_XP)
        {
            Level++;
            OnLevelUp.Invoke();
            CurrentXP = 0;
            m_totalXpGain += CurrentXP;
            Max_XP *= 1.25f;
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        // print(mousePos.MousePosition());
        OnShoot();
        // if(ListOfSpell.Count > 0)
        //ListOfSpell.ForEach(spell => { spell.Attack(Vector2.zero); });
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Move(InputDir.normalized);
        SetAnim();
    }

    private void SetAnim()
    {
        m_animator.SetFloat("directionX", InputDir.normalized.x);
        m_animator.SetFloat("directionY", InputDir.normalized.y);
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
        D.Get<GameplayManager>().RestartLevel(1);
    }

    /// <summary>
    /// Interface 
    /// </summary>
    /// <param name="_amount"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnStatHealthUpgrade(float _amount)
    {
        if (_amount > 0f)
        {
            this.maxHP += _amount;
            // this.currentHP = maxHP;
        }
    }

    public void OnStatSpeedUpgrade(float _amount)
    {
        if (_amount > 0f)
        {
            this.speed += _amount;
        }
    }

    public void OnAddNewSpell(GameObject _entity)
    {
        if (_entity.TryGetComponent(out Spell _spell))
        {
            if (!ListOfSpell.Contains(_spell))
            {
                ListOfSpell.Add(_spell);
                _spell.transform.parent = m_SpellParent;
            }
        }
        else
        {
            Debug.Log("Spell not init or doesnt exist!!");
        }
    }
}