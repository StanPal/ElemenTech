using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public event System.Action onEarthSkillPerformed;

    public event System.Action onWaterSkillPerformed;

    private Hero mHero;
    public Hero Hero { get { return mHero; } }
    private bool mIsSkillActivated = false;
    public bool SkillActive { get { return mIsSkillActivated; } set { mIsSkillActivated = value; } }

    private EarthSkills mEarthSkills;
    private WaterSkills mWaterSkills;

    [SerializeField]
    private Elements.ElementalAttribute mElementType;
    void Start()
    {
        mHero = FindObjectOfType<Hero>().GetComponent<Hero>();
        mEarthSkills = GetComponent<EarthSkills>();
        mWaterSkills = GetComponent<WaterSkills>();
        mElementType = mHero.GetElement;
        mHero.onSkillPerformed += PerformSkill;
    }

    void PerformSkill()
    {
        switch (mElementType)
        {
            case Elements.ElementalAttribute.Fire:
                break;
            case Elements.ElementalAttribute.Earth:
                onEarthSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Water:
                onWaterSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Air:
                break;
            default:
                break;
        }  
    }


}
