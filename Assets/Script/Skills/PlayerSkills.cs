using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public event System.Action onEarthSkillPerformed;
    public event System.Action onFireSkillPerformed;
    public event System.Action onAirSkillPerformed;
    public event System.Action onWaterSkillPerformed;

    private Hero mHero;
    public Hero Hero { get { return mHero; } }
    private bool mIsSkillActivated = false;
    public bool SkillActive { get { return mIsSkillActivated; } set { mIsSkillActivated = value; } }

    public GameObject FireHero;
    public GameObject EarthHero;
    public GameObject WaterHero;
    public GameObject AirHero;

    private PlayerManager Player;
    private EarthSkills mEarthSkills;
    private FireSkills mFireSkills;
    [SerializeField]
    private Elements.ElementalAttribute mElementType;

    void Start()
    {
        //BRUTE FORCE CODE 
        EarthHero.GetComponent<Hero>().onSkillPerformed += PerformSkill;
        FireHero.GetComponent<Hero>().onSkillPerformed += PerformSkill;
        AirHero.GetComponent<Hero>().onSkillPerformed += PerformSkill;
        WaterHero.GetComponent<Hero>().onSkillPerformed += PerformSkill;

        //mHero = FindObjectOfType<Hero>().GetComponent<Hero>();
        /* Transition to this later on
        //Player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        //for (int i = 0; i < Player.mPlayersList.Count; i++)
        //{
        //    Player.mPlayersList[i].onSkillPerformed += PerformSkill;
        //    Debug.Log(Player.mPlayersList[i].GetElement);
        //}
        */
        mEarthSkills = GetComponent<EarthSkills>();
        mFireSkills = GetComponent<FireSkills>();
    }

    private void Update()
    {
       
    }

    void PerformSkill(Elements.ElementalAttribute elementType)
    {
        switch (elementType)
        {
            case Elements.ElementalAttribute.Fire:
                mHero = FireHero.GetComponent<Hero>();
                onFireSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Earth:
                mHero = EarthHero.GetComponent<Hero>();
                onEarthSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Water:
                mHero = WaterHero.GetComponent<Hero>();
                onWaterSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Air:
                mHero = AirHero.GetComponent<Hero>();
                onAirSkillPerformed.Invoke();
                break;
            default:
                break;
        }
    }

    //void PerformSkill()
    //{
    //    for (int i = 0; i < Player.mPlayersList.Count; i++)
    //    {
    //        switch (Player.mPlayersList[i].GetElement)
    //        {
    //            case Elements.ElementalAttribute.Fire:
    //                mHero = Player.mPlayersList[i];
    //                onFireSkillPerformed.Invoke();
    //                break;
    //            case Elements.ElementalAttribute.Earth:
    //                mHero = Player.mPlayersList[i];
    //                onEarthSkillPerformed.Invoke();
    //                break;
    //            case Elements.ElementalAttribute.Water:
    //                break;
    //            case Elements.ElementalAttribute.Air:
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}


}
