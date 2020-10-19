﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkills : MonoBehaviour
{
    public GameObject mWaterGun;

    [SerializeField]
    private float mSpeed = 10;
    public float Speed { get { return mSpeed; } }
    [SerializeField]
    private float mDamage = 10;
    public float Damage { get { return mDamage; } }
    [SerializeField]
    //After a certain duration of time destroy gameobject if it is active
    private float mExitTime;
    public float ExitTime { get { return mExitTime; } }
    PlayerSkills mHeroSkills;
    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }

    private void Start()
    {
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onWaterSkillPerformed += WaterGun;
    }

    void WaterGun()
    {
        Instantiate(mWaterGun, mHeroSkills.HeroMovement.transform.position, Quaternion.identity);
    }

}
