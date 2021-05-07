using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSkills : MonoBehaviour
{
    public GameObject mAirJet;

    [SerializeField] private float mDamage = 2.0f;
    [SerializeField] private float mSpeed = 10.0f;
    [SerializeField] private float mExitTime = 10.0f;
    [SerializeField] private float _knockBackLength = 0.5f;
    [SerializeField] private float _knockBackMultiplier = 2f;
    //[SerializeField]
    //Vector3 mScale;
    //public Vector3 Scale { get { return mScale; } }
    //[SerializeField]
    //float mScaleSpeed; 
    //public float ScaleSpeed { get { return mScaleSpeed; } }
    PlayerSkills mHeroSkills;
    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }
    public float Damage { get { return mDamage; } set => mDamage = value; }
    public float ExitTime { get { return mExitTime; } }
    public float Speed { get { return mSpeed; } }
    public float KnockBackLength { get => _knockBackLength; } 
    public float KnockBackMulitplier { get => _knockBackMultiplier; }

    private void Start()
    {
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onAirSkillPerformed += AirJet;
    }

    void AirJet()
    {
        Instantiate(mAirJet, mHeroSkills.HeroAction.FirePoint.transform.position, Quaternion.Euler(0, 0, mHeroSkills.HeroAction.GetLookAngle));
    }
}
