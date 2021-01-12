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
 
    private PlayerManager PlayerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        PlayerManager = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        if (PlayerManager.PlayersList[0].gameObject != null)
        {
            PlayerManager.PlayersList[0].GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        }
        if (PlayerManager.PlayersList[1].gameObject != null)
        {
            PlayerManager.PlayersList[1].GetComponentInChildren<HeroActions>().onSkillPerformed += PerformSkill;
        }
        if (PlayerManager.PlayersList[2].gameObject != null)
        {
            PlayerManager.PlayersList[2].GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        }
        if (PlayerManager.PlayersList[3].gameObject != null)
        {
            PlayerManager.PlayersList[3].GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        }
    }

    void PerformSkill(Elements.ElementalAttribute elementalAttribute)
    {
        switch (elementalAttribute)
        {
            case Elements.ElementalAttribute.Fire:
                mHeroAction = PlayerManager.PlayersList[0].GetComponent<HeroActions>();
                mHeroMovement = PlayerManager.PlayersList[0].GetComponent<HeroMovement>();
                onFireSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Earth:
                mHeroAction = PlayerManager.PlayersList[3].GetComponent<HeroActions>();
                mHeroMovement = PlayerManager.PlayersList[3].GetComponent<HeroMovement>();
                onEarthSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Water:
                mHeroAction = PlayerManager.PlayersList[1].GetComponent<HeroActions>();
                mHeroMovement = PlayerManager.PlayersList[1].GetComponent<HeroMovement>();
                onWaterSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Air:
                mHeroAction = PlayerManager.PlayersList[2].GetComponent<HeroActions>();
                mHeroMovement = PlayerManager.PlayersList[2].GetComponent<HeroMovement>();
                onAirSkillPerformed.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        if (PlayerManager.PlayersList[0].gameObject != null)
        {
            PlayerManager.PlayersList[0].GetComponent<HeroActions>().onSkillPerformed -= PerformSkill;
        }
        if (PlayerManager.PlayersList[1].gameObject != null)
        {
            PlayerManager.PlayersList[1].GetComponentInChildren<HeroActions>().onSkillPerformed -= PerformSkill;
        }
        if (PlayerManager.PlayersList[2].gameObject != null)
        {
            PlayerManager.PlayersList[2].GetComponent<HeroActions>().onSkillPerformed -= PerformSkill;
        }
        if (PlayerManager.PlayersList[3].gameObject != null)
        {
            PlayerManager.PlayersList[3].GetComponent<HeroActions>().onSkillPerformed -= PerformSkill;
        }

    }
}
