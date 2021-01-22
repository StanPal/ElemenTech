﻿using System.Collections;
using UnityEngine;

public class HeroActions : MonoBehaviour
{
    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onAttackPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;

    private Animator _PlayerAnimator;

    private Guard guard;
    public GameObject sword;
    public Transform PivotPoint;
    public Transform FirePoint;

    private HeroMovement mHeroMovement;
    public HeroMovement HeroMovement { get { return mHeroMovement; } }
    private HeroStats mHeroStats;
    public HeroStats HeroStats { get { return mHeroStats; } }
    private PlayerInput mPlayerInput;
    public PlayerInput PlayerInput { get { return mPlayerInput; } }

    private bool isGuardInvoked = false;
    [SerializeField]
    private bool isOnCooldown = false;
    private float mNextFireTime;
    public bool IsCooldown { get { return isOnCooldown; } set { isOnCooldown = value; } }

    [SerializeField]
    private Vector2 mLookDirection;
    [SerializeField]
    private float mLookAngle;
    [SerializeField]
    private Vector2 axispos;

    Rigidbody2D _Rb;
    public Vector2 GetLookDir { get { return mLookDirection; } }
    public float GetLookAngle { get { return mLookAngle; } }

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _Rb = GetComponent<Rigidbody2D>();
        _PlayerAnimator = GetComponentInChildren<Animator>();
        mHeroMovement = GetComponent<HeroMovement>();
        mHeroStats = GetComponent<HeroStats>();
        mPlayerInput = new PlayerInput();
        guard = GetComponent<Guard>();
     
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

                mPlayerInput.KeyboardMouse.FastFall.performed += _ => FastFall();

                mPlayerInput.KeyboardMouse.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.KeyboardMouse.ElementSpecial1.performed += _ => ElementSpecial1();

                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    mPlayerInput.KeyboardMouse.Guard.performed += _ => Guard();
                }
                mPlayerInput.KeyboardMouse.GuardRelease.performed += _ => GuardRelease();
                mPlayerInput.KeyboardMouse.Pause.performed += _ => Pause();
            }

           if (mHeroMovement.controllerInput == HeroMovement.Controller.Keyboard2)
            {
                mPlayerInput.KeyboardLayout2.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.KeyboardLayout2.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    mPlayerInput.KeyboardLayout2.Guard.performed += _ => Guard();
                }
                mPlayerInput.KeyboardLayout2.GuardRelease.performed += _ => GuardRelease();
                mPlayerInput.KeyboardLayout2.Pause.performed += _ => Pause();
            }

            if (HeroMovement.controllerInput == HeroMovement.Controller.PS4)
            {
                mPlayerInput.PS4.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.PS4.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    mPlayerInput.PS4.Guard.performed += _ => Guard();
                }
                mPlayerInput.PS4.GuardRelease.performed += _ => GuardRelease();
                mPlayerInput.PS4.Pause.performed += _ => Pause();
            }

           if (HeroMovement.controllerInput == HeroMovement.Controller.XBOX)
            {
                mPlayerInput.XBOX.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.XBOX.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    mPlayerInput.XBOX.Guard.performed += _ => Guard();
                }
                mPlayerInput.XBOX.GuardRelease.performed += _ => GuardRelease();
            }
        }
    }

    private void Update()
    {
        switch (HeroMovement.controllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                //axispos = mPlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>();
                mLookDirection = Camera.main.ScreenToWorldPoint(mPlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>()) - transform.position;
                mLookAngle = Mathf.Atan2(mLookDirection.y, mLookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.PS4:
                axispos = mPlayerInput.PS4.Aim.ReadValue<Vector2>();
                mLookDirection = mPlayerInput.PS4.Aim.ReadValue<Vector2>();
                mLookAngle = Mathf.Atan2(mLookDirection.y, mLookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.XBOX:
                axispos = mPlayerInput.XBOX.Aim.ReadValue<Vector2>();
                mLookDirection = mPlayerInput.XBOX.Aim.ReadValue<Vector2>();
                mLookAngle = Mathf.Atan2(mLookDirection.y, mLookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }
    }

    
    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(mHeroStats.CoolDown);
        isOnCooldown = false;
    }

    private void Guard()
    {
        isGuardInvoked = true;
        sword.gameObject.SetActive(false);
         onGuardPerformed.Invoke();
    }

    private void GuardRelease()
    {
        isGuardInvoked = false;
        onGuardExit.Invoke();
        mHeroStats.RestoreShield(guard.ShieldRecoveryAmount, guard.ShieldRecoveryTick);
    }
    
    private void ElementSpecial1()
    {
        if (Time.time > mNextFireTime)
        {
            if (!isGuardInvoked && !isOnCooldown)
            {
                _PlayerAnimator.SetTrigger("SkillTrigger");
                mNextFireTime = Time.time + HeroStats.CoolDown;
                onSkillPerformed.Invoke(HeroStats.GetElement);
            }
        }
    }

    private void SwordSwing()
    {
        if (!isGuardInvoked)
        {
            _PlayerAnimator.SetBool("IsJumping",false);
            _PlayerAnimator.SetTrigger("AttackTrigger");
            sword.gameObject.SetActive(true);
            onAttackPerformed.Invoke();
        }
    }

    private void FastFall()
    {
        if (HeroStats.GetElement == Elements.ElementalAttribute.Earth)
        {
            _PlayerAnimator.SetTrigger("FastFallTrigger");
            StartCoroutine(GravityModifier());
        }
    }

    private IEnumerator GravityModifier()
    {
        _Rb.gravityScale = 10;
        yield return new WaitForSeconds(0.5f);
        _Rb.gravityScale = 1;
    }

    private void Pause()
    {
        Debug.Log("Called");
        onPausePeformed.Invoke();
    }
 
}
