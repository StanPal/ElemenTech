﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActions : MonoBehaviour
{
    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onAttackPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;

    public GameObject sword;

    private HeroMovement mHeroMovement;
    public HeroMovement HeroMovement { get { return mHeroMovement; } }
    private HeroStats mHeroStats;
    public HeroStats HeroStats { get { return mHeroStats; } }
    private PlayerInput mPlayerInput;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        mHeroMovement = GetComponent<HeroMovement>();
        mHeroStats = GetComponent<HeroStats>();
        mPlayerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        mPlayerInput.Enable();
    }
    private void OnDisable()
    {
        mPlayerInput.Disable();
    }

    private void Start()
    {
 
        if (!mHeroMovement.Recovering)
        {
            if (mHeroMovement.controllerInput == HeroMovement.Controller.Keyboard)
            {
                mPlayerInput.KeyboardMouse.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.KeyboardMouse.ElementSpecial1.performed += _ => ElementSpecial1();
                mPlayerInput.KeyboardMouse.Guard.performed += _ => Guard();
                mPlayerInput.KeyboardMouse.GuardRelease.performed += _ => GuardRelease();
                mPlayerInput.KeyboardMouse.Pause.performed += _ => Pause();
            }

           else if (mHeroMovement.controllerInput == HeroMovement.Controller.Keyboard2)
            {
                mPlayerInput.KeyboardLayout2.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.KeyboardLayout2.ElementSpecial1.performed += _ => ElementSpecial1();
                mPlayerInput.KeyboardLayout2.Guard.performed += _ => Guard();
                mPlayerInput.KeyboardLayout2.GuardRelease.performed += _ => GuardRelease();
                mPlayerInput.KeyboardLayout2.Pause.performed += _ => Pause();
            }

           else if (HeroMovement.controllerInput == HeroMovement.Controller.PS4)
            {
                mPlayerInput.PS4.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.PS4.ElementSpecial1.performed += _ => ElementSpecial1();
                mPlayerInput.PS4.Guard.performed += _ => Guard();
                mPlayerInput.PS4.GuardRelease.performed += _ => GuardRelease();
                mPlayerInput.PS4.Pause.performed += _ => Pause();
            }

           else if (HeroMovement.controllerInput == HeroMovement.Controller.XBOX)
            {
                mPlayerInput.XBOX.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.XBOX.ElementSpecial1.performed += _ => ElementSpecial1();
                mPlayerInput.XBOX.Guard.performed += _ => Guard();
                mPlayerInput.XBOX.GuardRelease.performed += _ => GuardRelease();
            }
        }
    }



    private void Guard()
    {
        onGuardPerformed.Invoke();
    }

    private void GuardRelease()
    {
        onGuardExit.Invoke();
    }
    
    private void ElementSpecial1()
    {
       onSkillPerformed.Invoke(HeroStats.GetElement);

    }

    private void SwordSwing()
    {
        sword.gameObject.SetActive(true);
        onAttackPerformed.Invoke();
    }

    private void Pause()
    {
        Debug.Log("Called");
        onPausePeformed.Invoke();
    }
 
}
