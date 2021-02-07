using System.Collections;
using UnityEngine;

public class HeroActions : MonoBehaviour
{
    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onAttackPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;

    public GameObject Sword;
    private Animator _PlayerAnimator;
    private Guard _Guard;
    public Transform PivotPoint;
    public Transform FirePoint;
    private HeroMovement _HeroMovement;
    private HeroStats _HeroStats;
    private PlayerInput _PlayerInput;
    private Rigidbody2D _Rb;

    [SerializeField] private bool _IsOnCooldown = false;
    [SerializeField] private Vector2 mLookDirection;
    [SerializeField] private float mLookAngle;
    [SerializeField] private Vector2 axispos;

    private bool _IsGuardInvoked = false;
    private float _NextFireTime;
    public bool IsCooldown { get { return _IsOnCooldown; } set { _IsOnCooldown = value; } }
    
    //Getters & Setters
    public HeroMovement HeroMovement { get { return _HeroMovement; } }
    public HeroStats HeroStats { get { return _HeroStats; } }
    public PlayerInput PlayerInput { get { return _PlayerInput; } }
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
        _HeroMovement = GetComponent<HeroMovement>();
        _HeroStats = GetComponent<HeroStats>();
        _PlayerInput = new PlayerInput();
        _Guard = GetComponent<Guard>();
    }

    private void OnEnable()
    {
        _PlayerInput.Enable();
    }
    private void OnDisable()
    {
        _PlayerInput.Disable();
    }

    private void Start()
    { 
        if (!_HeroMovement.Recovering)
        {    
            if (_HeroMovement.ControllerInput == HeroMovement.Controller.Keyboard)
            {
                _PlayerInput.KeyboardMouse.FastFall.performed += _ => FastFall();
                _PlayerInput.KeyboardMouse.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.KeyboardMouse.ElementSpecial1.performed += _ => ElementSpecial1();

                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.KeyboardMouse.Guard.performed += _ => Guard();
                }
                _PlayerInput.KeyboardMouse.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.KeyboardMouse.Pause.performed += _ => Pause();
            }

           if (_HeroMovement.ControllerInput == HeroMovement.Controller.Keyboard2)
            {
                _PlayerInput.KeyboardLayout2.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.KeyboardLayout2.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.KeyboardLayout2.Guard.performed += _ => Guard();
                }
                _PlayerInput.KeyboardLayout2.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.KeyboardLayout2.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.PS4)
            {
                _PlayerInput.PS4.FastFall.performed += _ => FastFall();
                _PlayerInput.PS4.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.PS4.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.PS4.Guard.performed += _ => Guard();
                }
                _PlayerInput.PS4.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.PS4.Pause.performed += _ => Pause();
            }

           if (HeroMovement.ControllerInput == HeroMovement.Controller.XBOX)
            {
                _PlayerInput.XBOX.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.XBOX.FastFall.performed += _ => FastFall();
                _PlayerInput.XBOX.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.XBOX.Guard.performed += _ => Guard();
                }
                _PlayerInput.XBOX.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.XBOX.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.Gamepad)
            {
                _PlayerInput.Gamepad.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.Gamepad.FastFall.performed += _ => FastFall();
                _PlayerInput.Gamepad.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _PlayerInput.Gamepad.Guard.performed += _ => Guard();
                }
                _PlayerInput.Gamepad.GuardRelease.performed += _ => GuardRelease();
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
                //axispos = _PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>();
                mLookDirection = Camera.main.ScreenToWorldPoint(_PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>()) - transform.position;
                mLookAngle = Mathf.Atan2(mLookDirection.y, mLookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.PS4:
                axispos = _PlayerInput.PS4.Aim.ReadValue<Vector2>();
                mLookDirection = _PlayerInput.PS4.Aim.ReadValue<Vector2>();
                mLookAngle = Mathf.Atan2(mLookDirection.y, mLookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.XBOX:
                axispos = _PlayerInput.XBOX.Aim.ReadValue<Vector2>();
                mLookDirection = _PlayerInput.XBOX.Aim.ReadValue<Vector2>();
                mLookAngle = Mathf.Atan2(mLookDirection.y, mLookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.Gamepad:
                axispos = _PlayerInput.Gamepad.Aim.ReadValue<Vector2>();
                mLookDirection = _PlayerInput.Gamepad.Aim.ReadValue<Vector2>();
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
        if (Time.time > _NextFireTime)
        {
            if (!_IsGuardInvoked && !_IsOnCooldown && !_HeroMovement.Dashing)
            {
                _PlayerAnimator.SetTrigger("SkillTrigger");
                _NextFireTime = Time.time + HeroStats.CoolDown;
                onSkillPerformed.Invoke(HeroStats.GetElement);
            }
        }
    }

    private void SwordSwing()
    {
        if (!_IsGuardInvoked && !_HeroMovement.Dashing)
        {
            _PlayerAnimator.SetBool("IsJumping",false);
            //_PlayerAnimator.SetTrigger("AttackTrigger");
            Sword.gameObject.SetActive(true);
            onAttackPerformed.Invoke();
        }
    }

    private void FastFall()
    {
        if (HeroStats.GetElement == Elements.ElementalAttribute.Earth)
        {
            _PlayerAnimator.SetBool("IsJumping", false);
            _PlayerAnimator.SetTrigger("FastFallTrigger");
            StartCoroutine(GravityModifier());
        }
    }

    private IEnumerator GravityModifier()
    {        
        _Rb.gravityScale = 15;
        yield return new WaitForSeconds(0.5f);
        _Rb.gravityScale = 1;
    }

    private void Pause()
    {
        Debug.Log("Called");
        onPausePeformed.Invoke();
    }
}
