using System.Collections;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    public event System.Action<GameObject> onDebuffActivated;
    public event System.Action onDebuffDeActivated;
    private Guard _Guard;

    public enum TeamSetting
    {
        Team1,
        Team2,
        FFA
    };

    [SerializeField] public TeamSetting Team = TeamSetting.FFA;
    // Basic Stats
    [SerializeField] private string _Name;
    [SerializeField] private float _Attack = 10f;
    [SerializeField] private float _MaxHealth = 100f;
    [SerializeField] private float _CurrentHealth = 100f;
    [SerializeField] private float _CoolDown = 3f;
    private float _TempCoolDownTime;
    private bool _IsCoolDownFinished;

    //Status Effect
    [SerializeField]
    private StatusEffects.PositiveEffects _PositiveEffect = StatusEffects.PositiveEffects.None;
    [SerializeField]
    private StatusEffects.NegativeEffects _NegativeEffect = StatusEffects.NegativeEffects.None;

    //Elemental Type
    [SerializeField] private Elements.ElementalAttribute _ElementalType;

    //Getters / Setters
    public bool CDFinished { get { return _IsCoolDownFinished; } set { _IsCoolDownFinished = value; } }
    public float CDTime { get { return _TempCoolDownTime; } set { _TempCoolDownTime = value; } }
    public float CoolDown { get { return _CoolDown; } }
    public float CurrentHealth { get { return _CurrentHealth; } set { _CurrentHealth = value; } }
    public float MaxHealth { get { return _MaxHealth; } }
    public float AttackDamage { get { return _Attack; } }
    public StatusEffects.PositiveEffects Buff { get { return _PositiveEffect; } set { _PositiveEffect = value; } }
    public Elements.ElementalAttribute GetElement { get { return _ElementalType; } }
    public StatusEffects.NegativeEffects DeBuff { get { return _NegativeEffect; } set { _NegativeEffect = value; } }

    void Awake()
    {
        _CurrentHealth = _MaxHealth;
        _TempCoolDownTime = 0;
        _Guard = GetComponent<Guard>();
    }
    
    private void FixedUpdate()
    {
        if (_TempCoolDownTime <= 0.0f)
        {
            _TempCoolDownTime = 0.0f;
            _IsCoolDownFinished = true;
        }
        if (_TempCoolDownTime > 0.0f)
        {
            _TempCoolDownTime -= Time.deltaTime;
        }
        
        if(_CurrentHealth <= 0)
        {
            HeroDie();
        }
    }

    public void TakeDamage(float damage)
    {
        if (_CurrentHealth <= 0)
        {
            HeroDie();
        }
        if (_Guard.Guarding)
        {
            _CurrentHealth -= (damage * 0.75f);
        }
        else
        {
            _CurrentHealth -= damage;
        }
        switch (_NegativeEffect)
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


    public void RestoreShield(float restoreAmount, float restoreTick)
    {
        StartCoroutine(RestoreShieldOverTimeCoroutine(restoreAmount, restoreTick));
    }

    IEnumerator RestoreShieldOverTimeCoroutine(float restoreAmount, float restoreTick)
    {
        float restoreperloop = restoreAmount / restoreTick;
        while ((_Guard.ShieldEnergy < _Guard.ShieldMaxEnergy) && !_Guard.Guarding)
        {
            _Guard.ShieldEnergy += restoreperloop;
            yield return new WaitForSeconds(1f);
        }
        if (_Guard.ShieldEnergy >= _Guard.ShieldMaxEnergy)
        {
            _Guard.isShieldDisabled = false;
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
        while(amountDamaged < damageAmount)
        {        
            _CurrentHealth -= damagePerloop;
            if (_CurrentHealth <= 0)
            {
                HeroDie();
            }
            Debug.Log(_ElementalType.ToString() + "Hero Current Health" + _CurrentHealth);
            amountDamaged += damagePerloop;
            yield return new WaitForSeconds(1f);
        }
        _NegativeEffect = StatusEffects.NegativeEffects.None;
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
            Debug.Log(_ElementalType.ToString() + "Hero Current Speed" + heromovement.Speed);
            yield return new WaitForSeconds(1f);
        }
        _NegativeEffect = StatusEffects.NegativeEffects.None;
        onDebuffDeActivated?.Invoke();
    }

    void HeroDie()
    {        
        PlayerManager PlayerManager = ServiceLocator.Get<PlayerManager>();
        if(PlayerManager.TeamOne.Contains(gameObject))
        {
            PlayerManager.TeamOne.Remove(gameObject);
        }
        if(PlayerManager.TeamTwo.Contains(gameObject))
        {
            PlayerManager.TeamTwo.Remove(gameObject);
        }
        this.gameObject.SetActive(false);
    }
}
