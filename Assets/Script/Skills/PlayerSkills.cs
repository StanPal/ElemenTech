using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public event System.Action onEarthSkillPerformed;
    public event System.Action onFireSkillPerformed;
    public event System.Action onAirSkillPerformed;
    public event System.Action onWaterSkillPerformed;

    public GameObject FireHero;
    public GameObject WaterHero;
    public GameObject AirHero;
    public GameObject EarthHero;

    private HeroActions mHeroAction;
    public HeroActions HeroAction { get { return mHeroAction; } }
    private HeroMovement mHeroMovement;
    public HeroMovement HeroMovement { get { return mHeroMovement; } }
    private bool mIsSkillActivated = false;
    public bool SkillActive { get { return mIsSkillActivated; } set { mIsSkillActivated = value; } }

  

    PlayerManager PlayerManager;

    private void Awake()
    {
        AirHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        FireHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        EarthHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        WaterHero.GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
    }

    void Start()
    {
   
        //PlayerManager = FindObjectOfType<PlayerManager>();
        //mHeroMovement = FindObjectOfType<HeroMovement>();
        //mHeroAction = FindObjectOfType<HeroActions>();
        //mHeroAction.onSkillPerformed += PerformSkill;        
    }

    void PerformSkill(Elements.ElementalAttribute elementalAttribute)
    {
        switch (elementalAttribute)
        {
            case Elements.ElementalAttribute.Fire:
                mHeroAction = FireHero.GetComponent<HeroActions>();
                mHeroMovement = FireHero.GetComponent<HeroMovement>();
                onFireSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Earth:
                mHeroAction = EarthHero.GetComponent<HeroActions>();
                mHeroMovement = EarthHero.GetComponent<HeroMovement>();
                onEarthSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Water:
                mHeroAction = WaterHero.GetComponent<HeroActions>();
                mHeroMovement = WaterHero.GetComponent<HeroMovement>();
                onWaterSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Air:
                mHeroAction = AirHero.GetComponent<HeroActions>();
                mHeroMovement = AirHero.GetComponent<HeroMovement>();
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
