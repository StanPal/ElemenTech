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
 
    private PlayerManager playerManager;

    private void Awake()
    {

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.AirHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        playerManager.FireHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        playerManager.EarthHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        playerManager.WaterHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
    }

    void PerformSkill(Elements.ElementalAttribute elementalAttribute)
    {
        switch (elementalAttribute)
        {
            case Elements.ElementalAttribute.Fire:
                mHeroAction = playerManager.FireHero.GetComponent<HeroActions>();
                mHeroMovement = playerManager.FireHero.GetComponent<HeroMovement>();
                onFireSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Earth:
                mHeroAction = playerManager.EarthHero.GetComponent<HeroActions>();
                mHeroMovement = playerManager.EarthHero.GetComponent<HeroMovement>();
                onEarthSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Water:
                mHeroAction = playerManager.WaterHero.GetComponent<HeroActions>();
                mHeroMovement = playerManager.WaterHero.GetComponent<HeroMovement>();
                onWaterSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Air:
                mHeroAction = playerManager.AirHero.GetComponent<HeroActions>();
                mHeroMovement = playerManager.AirHero.GetComponent<HeroMovement>();
                onAirSkillPerformed.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        
    }
}
