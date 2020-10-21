using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    
    [SerializeField]
    string mName;
    [SerializeField]
    float mAttack = 10f;
    [SerializeField]
    float mMaxHealth = 100f;
    float mCurrentHealth = 100f;
    [SerializeField]
    float mCoolDown;
    float mTempCDTime;
    [SerializeField]
    bool isCDFinished;

    public bool CDFinished { get { return isCDFinished; } set { isCDFinished = value; } }
    public float CDTime { get { return mTempCDTime; } set { mTempCDTime = value; } }
    public float CoolDown { get { return mCoolDown; } }
    public float CurrentHealth { get { return mCurrentHealth; } }
    public float MaxHealth { get { return mMaxHealth; } }

    [SerializeField]
    Elements.ElementalAttribute mElementalType;
    public Elements.ElementalAttribute GetElement { get { return mElementalType; } }

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
        mCurrentHealth -= damage;
        if (mCurrentHealth <= 0)
            HeroDie();
    }

    void HeroDie()
    {
        this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
