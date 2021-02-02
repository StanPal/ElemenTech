﻿using System;
using System.Collections;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    public event Action<GameObject> onDebuffActivated;
    public event Action onDebuffDeActivated;

    private AnimationEvents _animationEvent;
    private Animator _animator;
    private Guard _guard;

    public enum TeamSetting
    {
        Team1,
        Team2,
        FFA
    };

    public TeamSetting Team = TeamSetting.FFA;

    // Basic Stats
    [SerializeField] private string _name;
    [SerializeField] private float _attack = 10f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private float _coolDown = 3f;
    private float _tempCooldDownTime;
    private bool _isCoolDownFinished;

    // Getters & Setters 
    public bool CDFinished { get => _isCoolDownFinished; set => _isCoolDownFinished = value; } 
    public float CDTime { get =>  _tempCooldDownTime; set => _tempCooldDownTime = value; }
    public float CoolDown { get => _coolDown; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; } 
    public float MaxHealth { get => _maxHealth; }
    public float AttackDamage { get => _attack; }

    //Elemental Type
    [SerializeField] private Elements.ElementalAttribute _elementalType;
    public Elements.ElementalAttribute GetElement { get => _elementalType; }

    //Buff & Debuff Effects
    [SerializeField] private StatusEffects.PositiveEffects _positiveEffects = StatusEffects.PositiveEffects.None;
    [SerializeField] private StatusEffects.NegativeEffects _negativeEffects = StatusEffects.NegativeEffects.None;
    public StatusEffects.PositiveEffects Buff { get => _positiveEffects;  set  => _positiveEffects = value; }
    public StatusEffects.NegativeEffects DeBuff { get => _negativeEffects; set => _negativeEffects = value; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animationEvent = GetComponentInChildren<AnimationEvents>();
        _currentHealth = _maxHealth;
        _tempCooldDownTime = 0;
        _guard = GetComponent<Guard>();
    }

    private void FixedUpdate()
    {
        if (_tempCooldDownTime <= 0.0f)
        {
            _tempCooldDownTime = 0.0f;
            _isCoolDownFinished = true;
        }
        if (_tempCooldDownTime > 0.0f)
        {
            _tempCooldDownTime -= Time.deltaTime;
        }

        if (_currentHealth <= 0)
        {
            HeroDie();
        }
    }

    public void TakeDamageFromProjectile(float damage)
    {
        if (!_animationEvent.DashProjectileInvincibility)
        {
            _currentHealth -= damage;
        }
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth <= 0)
        {
            HeroDie();
        }
        if (_guard.Guarding)
        {
            _currentHealth -= (damage * 0.75f);
        }
        else
        {
            _currentHealth -= damage;
        }
        switch (_negativeEffects)
        {
            case StatusEffects.NegativeEffects.OnFire:
                onDebuffActivated?.Invoke(gameObject);
                break;
            case StatusEffects.NegativeEffects.Slowed:
                onDebuffActivated?.Invoke(gameObject);
                break;
            case StatusEffects.NegativeEffects.Stunned:
                break;
            case StatusEffects.NegativeEffects.None:
                break;
            default:
                break;
        }
        _animator.SetTrigger("HurtTrigger");
    }


    public void RestoreShield(float restoreAmount, float restoreTick)
    {
        StartCoroutine(RestoreShieldOverTimeCoroutine(restoreAmount, restoreTick));
    }

    IEnumerator RestoreShieldOverTimeCoroutine(float restoreAmount, float restoreTick)
    {

        float restoreperloop = restoreAmount / restoreTick;
        while ((_guard.ShieldEnergy < _guard.ShieldMaxEnergy) && !_guard.Guarding)
        {
            _guard.ShieldEnergy += restoreperloop;
            yield return new WaitForSeconds(1f);
        }
        if (_guard.ShieldEnergy >= _guard.ShieldMaxEnergy)
        {
            _guard.IsShieldDisabled = false;
        }
    }

    public void DamageOverTime(float damageAmount, float damageDuration)
    {
        StartCoroutine(DamageOverTimeCoroutine(damageAmount, damageDuration));
    }

    IEnumerator DamageOverTimeCoroutine(float damageAmount, float duration)
    {
        float amountDamaged = 0;
        float damagePerloop = damageAmount / duration;
        while (amountDamaged < damageAmount)
        {
            _currentHealth -= damagePerloop;
            if (_currentHealth <= 0)
            {
                HeroDie();
            }
            Debug.Log(_elementalType.ToString() + "Hero Current Health" + _currentHealth);
            amountDamaged += damagePerloop;
            yield return new WaitForSeconds(1f);
        }
        _negativeEffects = StatusEffects.NegativeEffects.None;
        onDebuffDeActivated?.Invoke();
    }

    public void SlowMovement(float mSlowAmount, float mSlowDuration)
    {
        StartCoroutine(SlowEffectCoroutine(mSlowAmount, mSlowDuration));
    }

    IEnumerator SlowEffectCoroutine(float slowAmount, float duration)
    {
        HeroMovement heromovement = GetComponent<HeroMovement>();
        float maxSpeed = heromovement.Speed;
        heromovement.Speed = slowAmount;
        float slowPerLoop = slowAmount / duration;
        while (heromovement.Speed < maxSpeed)
        {
            heromovement.Speed += slowPerLoop;
            Debug.Log(_elementalType.ToString() + "Hero Current Speed" + heromovement.Speed);
            yield return new WaitForSeconds(1f);
        }
        _negativeEffects = StatusEffects.NegativeEffects.None;
        onDebuffDeActivated?.Invoke();
    }

    void HeroDie()
    {
        PlayerManager playermanager = ServiceLocator.Get<PlayerManager>();
        if (playermanager.TeamOne.Contains(gameObject))
        {
            playermanager.TeamOne.Remove(gameObject);
        }
        if (playermanager.TeamTwo.Contains(gameObject))
        {
            playermanager.TeamTwo.Remove(gameObject);
        }
        this.gameObject.SetActive(false);
    }
}
