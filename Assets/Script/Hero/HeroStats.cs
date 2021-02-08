using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    public event System.Action<GameObject> onDebuffActivated;
    public event System.Action onDebuffDeActivated;

    private Guard guard;

    public enum TeamSetting
    {
        Team1,
        Team2,
        FFA
    };

    [SerializeField]
    public TeamSetting team = TeamSetting.FFA;

    // Basic Stats
    [SerializeField]
    private string mName;
    [SerializeField]
    private float mAttack = 10f;
    [SerializeField]
    private float mMaxHealth = 100f;
    [SerializeField]
    private float mCurrentHealth = 100f;
    [SerializeField]
    private float mCoolDown = 3f;
    private float mTempCDTime;   

    private bool isCDFinished;    
    public bool CDFinished { get { return isCDFinished; } set { isCDFinished = value; } }
    public float CDTime { get { return mTempCDTime; } set { mTempCDTime = value; } }
    public float CoolDown { get { return mCoolDown; } }
    public float CurrentHealth { get { return mCurrentHealth; } set { mCurrentHealth = value; } }
    public float MaxHealth { get { return mMaxHealth; } }
    public float AttackDamage { get { return mAttack; } }

    //Elementa Type
    [SerializeField]
    private Elements.ElementalAttribute mElementalType;
    public Elements.ElementalAttribute GetElement { get { return mElementalType; } }

    //Buff & Debuff Effects
    [SerializeField]
    private StatusEffects.PositiveEffects mPositiveEffect = StatusEffects.PositiveEffects.None;
    [SerializeField]
    private StatusEffects.NegativeEffects mNegativeEffect = StatusEffects.NegativeEffects.None;
    public StatusEffects.PositiveEffects Buff { get { return mPositiveEffect; } set { mPositiveEffect = value; } }
    public StatusEffects.NegativeEffects DeBuff { get { return mNegativeEffect; } set { mNegativeEffect = value; } }

    //Hero particle
    [SerializeField]
    private GameObject _deadParticle;
    private GameObject _hitParticle;
    public GameObject HitParticle { get { return _hitParticle; } set { _hitParticle = value; } }


    private void Awake()
    {
        mCurrentHealth = mMaxHealth;
        mTempCDTime = 0;
        guard = GetComponent<Guard>();
    }
    
    private void FixedUpdate()
    {
        if (mTempCDTime <= 0.0f)
        {
            mTempCDTime = 0.0f;
            isCDFinished = true;
        }
        if (mTempCDTime > 0.0f)
        {
            mTempCDTime -= Time.deltaTime;
        }
        
        if(mCurrentHealth <= 0)
        {
            HeroDie();
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(_hitParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
        if (mCurrentHealth <= 0)
        {
            HeroDie();
        }
        if (guard.Guarding)
        {
            mCurrentHealth -= (damage * 0.75f);
        }
        else
        {
            mCurrentHealth -= damage;
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


    public void RestoreShield(float restoreAmount, float restoreTick)
    {
        StartCoroutine(RestoreShieldOverTimeCoroutine(restoreAmount, restoreTick));
    }

    IEnumerator RestoreShieldOverTimeCoroutine(float restoreAmount, float restoreTick)
    {

        float restoreperloop = restoreAmount / restoreTick;
        while ((guard.ShieldEnergy < guard.ShieldMaxEnergy) && !guard.Guarding)
        {
            guard.ShieldEnergy += restoreperloop;
            yield return new WaitForSeconds(1f);
        }
        if (guard.ShieldEnergy >= guard.ShieldMaxEnergy)
        {
            guard.isShieldDisabled = false;
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
            mCurrentHealth -= damagePerloop;
            if (mCurrentHealth <= 0)
            {
                HeroDie();
            }
            Debug.Log(mElementalType.ToString() + "Hero Current Health" + mCurrentHealth);
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

    public void HeroDie()
    {        
        PlayerManager playermanager = ServiceLocator.Get<PlayerManager>();
        Instantiate(_deadParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
        if(playermanager.TeamOne.Contains(gameObject))
        {
            playermanager.TeamOne.Remove(gameObject);
        }
        if(playermanager.TeamTwo.Contains(gameObject))
        {
            playermanager.TeamTwo.Remove(gameObject);
        }
        this.gameObject.SetActive(false);
    }

}
