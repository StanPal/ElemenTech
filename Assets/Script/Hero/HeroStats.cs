using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    
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
    private float mCoolDown;
    private float mTempCDTime;
    [SerializeField]

    private bool isCDFinished;    
    public bool CDFinished { get { return isCDFinished; } set { isCDFinished = value; } }
    public float CDTime { get { return mTempCDTime; } set { mTempCDTime = value; } }
    public float CoolDown { get { return mCoolDown; } }
    public float CurrentHealth { get { return mCurrentHealth; } }
    public float MaxHealth { get { return mMaxHealth; } }

    //Elementa Type
    [SerializeField]
    private Elements.ElementalAttribute mElementalType;
    public Elements.ElementalAttribute GetElement { get { return mElementalType; } }

    //Buff & Debuff Effects
    [SerializeField]
    StatusEffects.PositiveEffects mPositiveEffect = StatusEffects.PositiveEffects.None;
    [SerializeField]
    StatusEffects.NegativeEffects mNegativeEffect = StatusEffects.NegativeEffects.None;
    public StatusEffects.PositiveEffects Buff { get { return mPositiveEffect; } set { mPositiveEffect = value; } }
    public StatusEffects.NegativeEffects DeBuff { get { return mNegativeEffect; } set { mNegativeEffect = value; } }
    [SerializeField]
    private float mDOTDuration = 5f;



    void Awake()
    {
        mCurrentHealth = mMaxHealth;
        mTempCDTime = 0;
    }

    private void Update()
    {
        if (mTempCDTime <= 0.0f)
        {
            mTempCDTime = 0.0f;
            isCDFinished = true;
        }
        if (mTempCDTime > 0.0f)
            mTempCDTime -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        switch (mNegativeEffect)
        {
            case StatusEffects.NegativeEffects.OnFire:
                mCurrentHealth -= damage;
                DamageOverTime(damage, mDOTDuration);
                break;
            case StatusEffects.NegativeEffects.Slowed:
                mCurrentHealth -= damage;
                HeroMovement heroMovement = GetComponent<HeroMovement>();
                break;
            case StatusEffects.NegativeEffects.Stunned:
                mCurrentHealth -= damage;
                break;
            case StatusEffects.NegativeEffects.None:
                mCurrentHealth -= damage;
                break;
            default:
                break;
        }
        
        if (mCurrentHealth <= 0)
            HeroDie();
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
            Debug.Log(mCurrentHealth.ToString());
            amountDamaged += damagePerloop;
            yield return new WaitForSeconds(1f);
        }
        mNegativeEffect = StatusEffects.NegativeEffects.None;
    }

    void HeroDie()
    {
        this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
