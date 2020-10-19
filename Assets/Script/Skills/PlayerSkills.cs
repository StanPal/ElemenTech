using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public event System.Action onEarthSkillPerformed;
    public event System.Action onFireSkillPerformed;
    public event System.Action onAirSkillPerformed;
    public event System.Action onWaterSkillPerformed;

    private HeroActions mHeroAction;
    public HeroActions HeroAction { get { return mHeroAction; } }
    private HeroMovement mHeroMovement;
    public HeroMovement HeroMovement { get { return mHeroMovement; } }
    private bool mIsSkillActivated = false;
    public bool SkillActive { get { return mIsSkillActivated; } set { mIsSkillActivated = value; } }

    PlayerManager PlayerManager;

    void Start()
    {
        PlayerManager = FindObjectOfType<PlayerManager>();
        mHeroMovement = FindObjectOfType<HeroMovement>();
        mHeroAction = FindObjectOfType<HeroActions>();
        mHeroAction.onSkillPerformed += PerformSkill;        
    }

    void PerformSkill()
    {
        switch (mHeroAction.HeroStats.GetElement)
        {
            case Elements.ElementalAttribute.Fire:
                onFireSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Earth:
                onEarthSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Water:
                onWaterSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Air:
                onAirSkillPerformed.Invoke();
                break;
            default:
                break;
        }
    }
}
