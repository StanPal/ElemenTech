using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkills : MonoBehaviour
{
    // FireSkills 
    public GameObject mFireBall;

    [SerializeField]
    private float mSpeed = 10.0f;
    public float Speed { get { return mSpeed; } }
    [SerializeField]
    private float mDamage = 10.0f;
    public float Damage { get { return mDamage; } }

    PlayerSkills mHeroSkills;
    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }

    private void Start()
    {
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onFireSkillPerformed += FireBall;
    }

    void FireBall()
    {
        Instantiate(mFireBall, mHeroSkills.Hero.transform);
    }

}
