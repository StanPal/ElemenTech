using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkills : MonoBehaviour
{
    public GameObject mWaterGun;

    PlayerSkills mHeroSkills;
    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }

    private void Start()
    {
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onWaterSkillPerformed += WaterGun;
    }

    void WaterGun()
    {
        Instantiate(mWaterGun, mHeroSkills.Hero.transform.position, Quaternion.identity);
    }

}
