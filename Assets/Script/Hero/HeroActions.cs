using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActions : MonoBehaviour
{
    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onAttackPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;

    HeroMovement mHeroMovement;
    private PlayerInput mPlayerInput;

    private void Awake()
    {
        mHeroMovement = GetComponent<HeroMovement>();
        mPlayerInput = new PlayerInput();
        if (mHeroMovement.controllerInput == HeroMovement.Controller.Keyboard)
        {
            mPlayerInput.KeyboardMouse.SwordSwing.performed += _ => SwordSwing();
        }
    }

    private void OnEnable()
    {
        mPlayerInput.Enable();
    }
    private void OnDisable()
    {
        mPlayerInput.Disable();
    }

    private void SwordSwing()
    {
        onAttackPerformed.Invoke();
    }
 
}
