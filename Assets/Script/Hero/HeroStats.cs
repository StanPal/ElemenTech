using System.Collections;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    public event System.Action<GameObject> onDebuffActivated;
    public event System.Action onDebuffDeActivated;

    private AnimationEvents _animationEvent;
    private Animator _animator;
    private Guard _guard;

    public enum TeamSetting
    {
        Team1,
        Team2,
        FFA
    };

    //[SerializeField]
    public TeamSetting team = TeamSetting.FFA;

    // Basic Stats
    [SerializeField] private string _name;
    [SerializeField] private float _attack = 10f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private float _coolDown = 3f;
    public float CoolDown { get { return _coolDown; } }
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public float MaxHealth { get { return _maxHealth; } }
    public float AttackDamage { get { return _attack; } set { _attack = value; } }

    public float CDTime { get { return _tempCooldDownTime; } set { _tempCooldDownTime = value; } }
    private float _tempCooldDownTime;

    public bool CDFinished { get { return _isCDFinished; } set { _isCDFinished = value; } }
    private bool _isCDFinished;


    //Elementa Type
    [SerializeField]
    public Elements.ElementalAttribute mElementalType;
    public Elements.ElementalAttribute GetElement { get { return mElementalType; } }

    //Buff & Debuff Effects
    [SerializeField] private StatusEffects.PositiveEffects mPositiveEffect = StatusEffects.PositiveEffects.None;
    [SerializeField] private StatusEffects.NegativeEffects mNegativeEffect = StatusEffects.NegativeEffects.None;
    public StatusEffects.PositiveEffects Buff { get { return mPositiveEffect; } set { mPositiveEffect = value; } }
    public StatusEffects.NegativeEffects DeBuff { get { return mNegativeEffect; } set { mNegativeEffect = value; } }

    void Awake()
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
            _isCDFinished = true;
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
        else if (!gameObject.GetComponent<HeroMovement>().IsDashing)
        {
            _currentHealth -= damage;
        }
        switch (mNegativeEffect)
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
    }

    public void TakeDamageFromProjectile(float damage)
    {
        if (!_animationEvent.DashProjectileInvincibility)
        {
            _currentHealth -= damage;
        }
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
            Debug.Log(mElementalType.ToString() + "Hero Current Health" + _currentHealth);
            amountDamaged += damagePerloop;
            yield return new WaitForSeconds(1f);
        }
        mNegativeEffect = StatusEffects.NegativeEffects.None;
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
            Debug.Log(mElementalType.ToString() + "Hero Current Speed" + heromovement.Speed);
            yield return new WaitForSeconds(1f);
        }
        mNegativeEffect = StatusEffects.NegativeEffects.None;
        onDebuffDeActivated?.Invoke();
    }

    void HeroDie()
    {
        gameObject.SetActive(false);
        PlayerManager playermanager = ServiceLocator.Get<PlayerManager>();

        PauseUI pauseUI = FindObjectOfType<PauseUI>();
        pauseUI.PauseGame();
    }

}
