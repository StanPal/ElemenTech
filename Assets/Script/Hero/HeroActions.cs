using System.Collections;
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

    private Guard _Guard;
    private HeroMovement _HeroMovement;
    private HeroStats _HeroStats;
    private PlayerInput _PlayerInput;
    private bool _IsGuardInvoked = false;
    private float mNextFireTime;

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
                    _PlayerInput.KeyboardMouse.Guard.performed += _ => Guard();
                }
                _PlayerInput.KeyboardMouse.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.KeyboardMouse.Pause.performed += _ => Pause();
            }

            if (_HeroMovement.ControllerInput == HeroMovement.Controller.Keyboard2)
            {
                mPlayerInput.KeyboardLayout2.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.KeyboardLayout2.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.KeyboardLayout2.Guard.performed += _ => Guard();
                }
                _PlayerInput.KeyboardLayout2.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.KeyboardLayout2.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.PS4)
            {
                mPlayerInput.PS4.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.PS4.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.PS4.Guard.performed += _ => Guard();
                }
                _PlayerInput.PS4.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.PS4.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.XBOX)
            {
                mPlayerInput.XBOX.SwordSwing.performed += _ => SwordSwing();
                mPlayerInput.XBOX.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.XBOX.Guard.performed += _ => Guard();
                }
                _PlayerInput.XBOX.GuardRelease.performed += _ => GuardRelease();
            }
        }
    }

    private void Update()
    {
        switch (HeroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                //axispos = mPlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>();
                mLookDirection = Camera.main.ScreenToWorldPoint(mPlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>()) - transform.position;
                mLookAngle = Mathf.Atan2(mLookDirection.y, mLookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.PS4:
                _AxisPos = _PlayerInput.PS4.Aim.ReadValue<Vector2>();
                _LookDirection = _PlayerInput.PS4.Aim.ReadValue<Vector2>();
                _LookAngle = Mathf.Atan2(_LookDirection.y, _LookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.XBOX:
                _AxisPos = _PlayerInput.XBOX.Aim.ReadValue<Vector2>();
                _LookDirection = _PlayerInput.XBOX.Aim.ReadValue<Vector2>();
                _LookAngle = Mathf.Atan2(_LookDirection.y, _LookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(_HeroStats.CoolDown);
        _IsOnCooldown = false;
    }

    private void Guard()
    {
        _IsGuardInvoked = true;
        Sword.gameObject.SetActive(false);
         onGuardPerformed.Invoke();
    }

    private void GuardRelease()
    {
        _IsGuardInvoked = false;
        onGuardExit.Invoke();
        _HeroStats.RestoreShield(_Guard.ShieldRecoveryAmount, _Guard.ShieldRecoveryTick);
    }
    
    private void ElementSpecial1()
    {
        if (Time.time > mNextFireTime)
        {
            if (!_IsGuardInvoked && !_IsOnCooldown)
            {
                _PlayerAnimator.SetTrigger("SkillTrigger");
                mNextFireTime = Time.time + HeroStats.CoolDown;
                onSkillPerformed.Invoke(HeroStats.GetElement);
            }
        }
    }

    private void SwordSwing()
    {
        if (!_IsGuardInvoked)
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
