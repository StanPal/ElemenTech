using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public event System.Action onEarthSkillPerformed;
    public event System.Action onFireSkillPerformed;

    private Hero mHero;
    public Hero Hero { get { return mHero; } }
    private bool mIsSkillActivated = false;
    public bool SkillActive { get { return mIsSkillActivated; } set { mIsSkillActivated = value; } }

    private PlayerManager Player;
    private EarthSkills mEarthSkills;
    private FireSkills mFireSkills;
    [SerializeField]
    private Elements.ElementalAttribute mElementType;

    void Start()
    {
        Player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        //mHero = FindObjectOfType<Hero>().GetComponent<Hero>();
        //mHero.onSkillPerformed += PerformSkill;
        for (int i = 0; i < Player.mPlayersList.Count; i++)
        {
            Player.mPlayersList[i].onSkillPerformed += PerformSkill;
            Debug.Log(Player.mPlayersList[i].GetElement);
        }

        mEarthSkills = GetComponent<EarthSkills>();
        mFireSkills = GetComponent<FireSkills>();
    }

    private void Update()
    {
       
    }

    //void PerformSkill()
    //{
    //    switch (mHero.GetElement)
    //    {
    //        case Elements.ElementalAttribute.Fire:  
    //            onFireSkillPerformed.Invoke();
    //            break;
    //        case Elements.ElementalAttribute.Earth:
    //            onEarthSkillPerformed.Invoke();
    //            break;
    //        case Elements.ElementalAttribute.Water:
    //            break;
    //        case Elements.ElementalAttribute.Air:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    void PerformSkill()
    {
        for (int i = 0; i < Player.mPlayersList.Count; i++)
        {
            switch (Player.mPlayersList[i].GetElement)
            {
                case Elements.ElementalAttribute.Fire:
                    mHero = Player.mPlayersList[i];
                    onFireSkillPerformed.Invoke();
                    break;
                case Elements.ElementalAttribute.Earth:
                    mHero = Player.mPlayersList[i];
                    onEarthSkillPerformed.Invoke();
                    break;
                case Elements.ElementalAttribute.Water:
                    break;
                case Elements.ElementalAttribute.Air:
                    break;
                default:
                    break;
            }
        }
    }


}
