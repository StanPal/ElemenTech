using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkills : MonoBehaviour
{
    // FireSkills 
    public GameObject mFireBall;

    [SerializeField]
    private float mSpeed = 10.0f;
    public float Speed { get { return mSpeed; } set { mSpeed = value; } }
    [SerializeField]
    private float mDamage = 10.0f;
    public float Damage { get { return mDamage; } }
    [SerializeField]
    private float mDotDuration = 5.0f;
    public float DotDuration { get { return mDotDuration; } }

    private Elements.ElementalAttribute _Element = Elements.ElementalAttribute.Fire;
    public Elements.ElementalAttribute GetElement { get { return _Element; } }
    PlayerSkills mHeroSkills;
    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }

    private void Start()
    {
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onFireSkillPerformed += FireBall;
    }

    void FireBall()
    {
       Instantiate(mFireBall, mHeroSkills.HeroAction.FirePoint.transform.position, Quaternion.Euler(0, 0, mHeroSkills.HeroAction.GetLookAngle));
    }

}
