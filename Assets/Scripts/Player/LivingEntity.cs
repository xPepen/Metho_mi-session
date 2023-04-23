using System.Collections;
using UnityEngine;

public abstract class LivingEntity : BaseEntity,IHitable
{
    public float currentHP { get; protected set; }
    public float maxHP { get; protected set; }
    protected float speed;
    public bool IsDead => currentHP <= 0;
    protected Rigidbody2D m_rb;
    private AudioSource m_audio;
    private SpriteRenderer _sprite;
    private float m_HitValue;
    protected virtual void Init()
    {
        m_rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        m_audio = GetComponent < AudioSource >();
    }
    public void Move(Vector2 _direction)
    {
        Vector2 refVelo = Vector2.zero;
        var multiplier = _direction * speed * Time.deltaTime * 5f;
        
        m_rb.velocity = Vector2.SmoothDamp(m_rb.velocity, multiplier,
            ref refVelo, 0);
        
        //m_rb.velocity = multiplier;
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        Init();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (m_HitValue > 0)
        {
            m_HitValue = Mathf.Lerp(m_HitValue, 0f, Time.deltaTime * 5f);
            _sprite.material.SetFloat("_HitValue", m_HitValue);
        }
       
    }

    protected void Heal()
    {
        currentHP = maxHP;
    }
    public virtual void OnHit(float _damage)
    {
        m_HitValue = 1f;
        if (m_audio)
        {
            m_audio.Play();
        }
        this.currentHP -= _damage;
        if (IsDead )
        {
            OnDead();
        }
        
       // StartCoroutine(HitEffect());
    }
    private IEnumerator HitEffect()
    {
        
        Color transparentColor = _sprite.color;
        transparentColor.a = 0.5f;
        
        for (float i = 0; i <= 0.2f; i += 0.10f)
        {
            _sprite.color = transparentColor;
            yield return new WaitForSeconds(0.25f);
            _sprite.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.15f);
        if (IsDead )
        {
            OnDead();
        }
    }

    public abstract void OnDead();
}
