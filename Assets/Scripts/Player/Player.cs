using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity, IUpgradeblePlayerStats
{
    public static Player Instance { get; private set; }
    public float CurrentXP { get; private set; }
    public float Max_XP { get; private set; }
    private float m_totalXpGain;
    public int Level;
    [SerializeField] private PlayerSerializedEvent eventPlayerSerialized;
    public PlayerActionsContainer ListOfActions { get; private set; }
    [SerializeField] private PhysicEntityInfo EntityStats;

    //Ishootable section
    [SerializeField] private bool m_IsRevivable;
    [SerializeField] private bool m_IsSpeedBoost;
    [SerializeField] private bool m_IsBonusSkin;
    public Vector2 InputDir { get; set; }

    public Weapon MyWeapon;
    public List<Spell> ListOfSpell;
    [SerializeField] private Transform m_SpellParent;

    private InputReceiver mousePos;

    private Animator m_animator;

    public bool IsAutoPlay = false;
    public bool IsGodMode = false;
    public bool IsGameplaymode { get; private set; }

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
        eventPlayerSerialized = GetComponent<PlayerSerializedEvent>();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        PLayerSingleton();
        this.Bind();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (!IsGameplaymode) return;
        OnShoot();
        if (ListOfSpell.Count > 0)
            ListOfSpell.ForEach(spell => { spell.Attack(Vector2.zero); });

        if (ListOfActions.Contains(PlayerActionsType.PAUSE))
        {
            eventPlayerSerialized.OnPauseMenu.Invoke();
            ListOfActions.RemoveAll();
        }
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (!IsGameplaymode) return;
        if (!IsAutoPlay)
        {
            Move(InputDir.normalized);
        }

        SetAnim();
    }

    public void SetGameplayMode(bool value)
    {
        if (IsGameplaymode == value) return;
        IsGameplaymode = value;
    }

    private void InitPromoCode(EPromoCode promoCode)
    {
    }

    public void ResetVelocity()
    {
        ListOfActions.RemoveAll();
        Move(Vector2.zero);
    }

    public void AddXP(float _amount)
    {
        //add const value for hast table 
        //because nneed them to get hashtable values
        Hashtable hash = new Hashtable();

        CurrentXP += _amount;
        if (CurrentXP >= Max_XP)
        {
            Level++;
            eventPlayerSerialized.OnLevelUp.Invoke();
            CurrentXP = 0;
            m_totalXpGain += CurrentXP;
            Max_XP *= 1.25f;
            // BinaryReaderWriter.Serialize(Level, nameof(Player));
            // BinaryReaderWriter.Deserialize(nameof(Player), out hash);
        }
    }


    public void SetAnim(float dirX = 0f, float dirY = 0f)
    {
        if (dirX != 0f || dirY != 0f)
        {
            m_animator.SetFloat("directionX", dirX);
            m_animator.SetFloat("directionY", dirY);
        }
        m_animator.SetFloat("directionX", InputDir.normalized.x);
        m_animator.SetFloat("directionY", InputDir.normalized.y);
    }

    public override void OnHit(float _damage)
    {
        if (IsGodMode) return;
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

    public void OnShoot(Vector2 dir)
    {
        if (MyWeapon)
        {
            (MyWeapon as IShootable).Attack(dir);
        }
    }

    public void InitPlayer(int @baseMaxXP)
    {
        Heal();
        ResetVelocity();
        Max_XP = baseMaxXP;
        CurrentXP = 0f;
        ListOfActions.RemoveAll();
        Level = 0;
    }

    public override void OnDead()
    {
        eventPlayerSerialized.OnDead.Invoke();
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
            this.currentHP = maxHP;
        }
    }

    public void OnStatSpeedUpgrade(float _amount)
    {
        if (_amount > 0f)
        {
            this.speed += ValueToPercent(_amount, speed);
        }
    }

    public void OnAddNewSpell(GameObject _entity)
    {
        if (_entity.TryGetComponent(out Spell _spell))
        {
            if (!ListOfSpell.Contains(_spell))
            {
                _entity.SetActive(true);
                ListOfSpell.Add(_spell);
                var pos = _spell.transform;
                pos.parent = m_SpellParent;
                pos.localPosition = Vector3.zero;
            }
        }
        else
        {
            Debug.Log("Spell not init or doesnt exist!!");
        }
    }

    private float ValueToPercent(float multiplier, float baseValue)
    {
        return ((multiplier / 100) * baseValue);
    }
}