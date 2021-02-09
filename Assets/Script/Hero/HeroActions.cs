using System.Collections;
using UnityEngine;

public class HeroActions : MonoBehaviour
{
    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onAttackPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;

    public Transform PivotPoint;
    public Transform FirePoint;
    private Animator _playerAnimator;
    private Guard _guard;
    private HeroMovement _heroMovement;
    private HeroStats _heroStats;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidBody;

    [SerializeField] private GameObject _sword;
    [SerializeField] private bool _isOnCooldown = false;
    [SerializeField] private Vector2 _lookDirection;
    [SerializeField] private float _lookAngle;
    [SerializeField] private Vector2 _axisPos;

    private bool _isGuardInvoked = false;
    private float _nextFireTime;

    //Getters & Setters
    public bool IsCooldown { get => _isOnCooldown; set => _isOnCooldown = value; } 
    public HeroMovement HeroMovement { get => _heroMovement; }
    public HeroStats HeroStats { get => _heroStats; }
    public PlayerInput PlayerInput { get => _playerInput; }
    public Vector2 GetLookDir { get => _lookDirection; }
    public float GetLookAngle { get => _lookAngle; }

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _heroMovement = GetComponent<HeroMovement>();
        _heroStats = GetComponent<HeroStats>();
        _playerInput = new PlayerInput();
        _guard = GetComponent<Guard>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    { 
        if (!_heroMovement.Recovering)
        {    
            if (_heroMovement.ControllerInput == HeroMovement.Controller.Keyboard)
            {
                _playerInput.KeyboardMouse.FastFall.performed += _ => FastFall();
                _playerInput.KeyboardMouse.SwordSwing.performed += _ => SwordSwing();
                _playerInput.KeyboardMouse.ElementSpecial1.performed += _ => ElementSpecial1();

                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _playerInput.KeyboardMouse.Guard.performed += _ => Guard();
                }
                _playerInput.KeyboardMouse.GuardRelease.performed += _ => GuardRelease();
                _playerInput.KeyboardMouse.Pause.performed += _ => Pause();
            }

           if (_heroMovement.ControllerInput == HeroMovement.Controller.Keyboard2)
            {
                _playerInput.KeyboardLayout2.SwordSwing.performed += _ => SwordSwing();
                _playerInput.KeyboardLayout2.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _playerInput.KeyboardLayout2.Guard.performed += _ => Guard();
                }
                _playerInput.KeyboardLayout2.GuardRelease.performed += _ => GuardRelease();
                _playerInput.KeyboardLayout2.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.PS4)
            {
                _playerInput.PS4.FastFall.performed += _ => FastFall();
                _playerInput.PS4.SwordSwing.performed += _ => SwordSwing();
                _playerInput.PS4.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _playerInput.PS4.Guard.performed += _ => Guard();
                }
                _playerInput.PS4.GuardRelease.performed += _ => GuardRelease();
                _playerInput.PS4.Pause.performed += _ => Pause();
            }

           if (HeroMovement.ControllerInput == HeroMovement.Controller.XBOX)
            {
                _playerInput.XBOX.SwordSwing.performed += _ => SwordSwing();
                _playerInput.XBOX.FastFall.performed += _ => FastFall();
                _playerInput.XBOX.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _playerInput.XBOX.Guard.performed += _ => Guard();
                }
                _playerInput.XBOX.GuardRelease.performed += _ => GuardRelease();
                _playerInput.XBOX.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.Gamepad)
            {
                _playerInput.Gamepad.SwordSwing.performed += _ => SwordSwing();
                _playerInput.Gamepad.FastFall.performed += _ => FastFall();
                _playerInput.Gamepad.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _playerInput.Gamepad.Guard.performed += _ => Guard();
                }
                _playerInput.Gamepad.GuardRelease.performed += _ => GuardRelease();
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
                //_axisPos = _playerInput.KeyboardMouse.Aim.ReadValue<Vector2>();
                _lookDirection = Camera.main.ScreenToWorldPoint(_playerInput.KeyboardMouse.Aim.ReadValue<Vector2>()) - transform.position;
                _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.PS4:
                _axisPos = _playerInput.PS4.Aim.ReadValue<Vector2>();
                _lookDirection = _playerInput.PS4.Aim.ReadValue<Vector2>();
                _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.XBOX:
                _axisPos = _playerInput.XBOX.Aim.ReadValue<Vector2>();
                _lookDirection = _playerInput.XBOX.Aim.ReadValue<Vector2>();
                _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.Gamepad:
                _axisPos = _playerInput.Gamepad.Aim.ReadValue<Vector2>();
                _lookDirection = _playerInput.Gamepad.Aim.ReadValue<Vector2>();
                _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(_heroStats.CoolDown);
        _isOnCooldown = false;
    }

    private void Guard()
    {
        _isGuardInvoked = true;
        _sword.gameObject.SetActive(false);
         onGuardPerformed.Invoke();
    }

    private void GuardRelease()
    {
        _isGuardInvoked = false;
        onGuardExit.Invoke();
        _heroStats.RestoreShield(_guard.ShieldRecoveryAmount, _guard.ShieldRecoveryTick);
    }
    
    private void ElementSpecial1()
    {
        if (Time.time > _nextFireTime)
        {
            if (!_isGuardInvoked && !_isOnCooldown && !_heroMovement.IsDashing)
            {
                _playerAnimator.SetTrigger("SkillTrigger");
                _nextFireTime = Time.time + HeroStats.CoolDown;
                onSkillPerformed.Invoke(HeroStats.GetElement);
            }
        }
    }

    private void SwordSwing()
    {
        if (!_isGuardInvoked && !_heroMovement.IsDashing)
        {
            _playerAnimator.SetBool("IsJumping",false);
            _playerAnimator.SetTrigger("AttackTrigger");
            _sword.gameObject.SetActive(true);
            onAttackPerformed.Invoke();
        }
    }

    private void FastFall()
    {
        if (HeroStats.GetElement == Elements.ElementalAttribute.Earth)
        {
            _playerAnimator.SetBool("IsJumping", false);
            _playerAnimator.SetTrigger("FastFallTrigger");
            StartCoroutine(GravityModifier());
        }
    }

    private IEnumerator GravityModifier()
    {        
        _rigidBody.gravityScale = 15;
        yield return new WaitForSeconds(0.5f);
        _rigidBody.gravityScale = 1;
    }

    private void Pause()
    {
        Debug.Log("Called");
        onPausePeformed.Invoke();
    }
}
