using System.Collections;
using UnityEngine;

public class HeroActions : MonoBehaviour
{
    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onAttackPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;
    public event System.Action onParryPerformed;

    [SerializeField] private GameObject Stomp;
    private SoundManager _soundManager; 
    public GameObject Sword;
    public Transform PivotPoint;
    public Transform FirePoint;
    private SpriteRenderer _spriteRenderer;
    private Animator _playerAnimator;
    private Guard _guard;
    private HeroMovement _heroMovement;
    private HeroStats _heroStats;
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private Crosshair _crossHair;
    private bool _isGuardInvoked = false;
    private bool _isSwordSwinging = false;
    private bool _isEarthStomping = false;
    private bool _isChargingAttack = false;
    private bool _isChargingShot = false;
    private bool _isChargeAttackMax = false; 
    private bool _isChargeMax = false; 
    private float _nextFireTime;
    private Camera _camera;
    private Color _originalSpriteColor;

    [SerializeField] private bool _isOnCooldown = false;
    [SerializeField] private float _chargeAmount = 0f;
    [SerializeField] private float _chargeAmountMax = 10f;
    [SerializeField] private float _chargeTime = 2f;
    [SerializeField] private Vector2 _lookDirection;
    [SerializeField] private float _lookAngle;
    [SerializeField] private Vector3 _axisPos;
    private bool _isDashStriking = false;
    
    //Getters & Setters
    public bool _isSwinging { get => _isSwordSwinging; set => _isSwordSwinging = value; }
    public bool DashStriking { get => _isDashStriking; set => _isDashStriking = value; }
    public bool ChargingShot { get => _isChargingShot; set => _isChargingShot = value; } 
    public bool ChargeMax { get => _isChargeMax; set => _isChargeMax = value; }
    public float ChargeAmount { get => _chargeAmount; set => _chargeAmount = value; }
    public bool IsCooldown { get => _isOnCooldown;  set => _isOnCooldown = value; }
    public bool IsEarthStomping { get => _isEarthStomping; set => _isEarthStomping = value; }
    public HeroMovement HeroMovement { get => _heroMovement; }
    public HeroStats HeroStats { get => _heroStats; } 
    public PlayerInput PlayerInput { get => _playerInput; } 
    public Vector2 GetLookDir { get => _lookDirection; }
    public float GetLookAngle { get => _lookAngle; }
    public Animator PlayerAnimator { get => _playerAnimator; }
    public Crosshair CrossHair { get => _crossHair; }

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();
        _soundManager = ServiceLocator.Get<SoundManager>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _heroMovement = GetComponent<HeroMovement>();
        _heroStats = GetComponent<HeroStats>();
        _playerInput = new PlayerInput();
        _guard = GetComponent<Guard>();
        _camera = FindObjectOfType<Camera>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalSpriteColor = _spriteRenderer.color;
        _crossHair = GetComponentInChildren<Crosshair>();
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
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Air))
                {
                    _playerInput.KeyboardMouse.HoldAttack.performed += _ => OnCharging();
                    Debug.Log("Hold attack Triggered");
                }
                if (!gameObject.GetComponent<Guard>().IsShieldDisabled)
                {
                    _playerInput.KeyboardMouse.Guard.performed += _ => Guard();
                }
                _playerInput.KeyboardMouse.GuardRelease.performed += _ => GuardRelease();
                _playerInput.KeyboardMouse.Pause.performed += _ => Pause();
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

    private void FixedUpdate()
    {
        if(ChargingShot)
        {
            StartCoroutine(Charging());
        }

    }

    private void Update()
    {
        switch (HeroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _lookAngle = Mathf.Atan2(_crossHair.transform.position.y - transform.position.y, _crossHair.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.PS4:
                _axisPos = _playerInput.PS4.Aim.ReadValue<Vector2>();
                _lookDirection = _playerInput.PS4.Aim.ReadValue<Vector2>();
                _lookAngle = Mathf.Atan2(_crossHair.transform.position.y - transform.position.y, _crossHair.transform.position.x - _crossHair.transform.position.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.XBOX:
                _axisPos = _playerInput.XBOX.Aim.ReadValue<Vector2>();
                _lookDirection = _playerInput.XBOX.Aim.ReadValue<Vector2>();
                _lookAngle = Mathf.Atan2(_crossHair.transform.position.y - transform.position.y, _crossHair.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                break;
            case HeroMovement.Controller.Gamepad:
                _axisPos = _playerInput.Gamepad.Aim.ReadValue<Vector2>();
                _lookDirection = _playerInput.Gamepad.Aim.ReadValue<Vector2>();
                _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
                break;
            default:
                break;
        }
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(_heroStats.CoolDown);
        _isOnCooldown = false;
    }

    private void Guard()
    {
        _isGuardInvoked = true;
         onGuardPerformed.Invoke();
    }

    private void GuardRelease()
    {
        _isGuardInvoked = false;
        onGuardExit.Invoke();
    }
    
    private void OnCharging()
    {
        _isChargingShot = true;
    }

    private void ElementSpecial1()
    {
        if (Time.time > _nextFireTime)
        {
            if (!_isGuardInvoked && !_isOnCooldown)
            {
                if(!_heroMovement.Dashing)
                {
                    _playerAnimator.SetTrigger("SkillTrigger");
                    _nextFireTime = Time.time + HeroStats.CoolDown;
                    onSkillPerformed.Invoke(HeroStats.GetElement);
                }
                if((_heroMovement.Dashing && _heroMovement.IsGrounded()))
                {
                    _nextFireTime = Time.time + HeroStats.CoolDown;
                    onSkillPerformed.Invoke(HeroStats.GetElement);
                }
            }
        }
    }

    private void SwordSwing()
    {        
        if (HeroStats.GetElement.Equals(Elements.ElementalAttribute.Water))
        {
            if(_heroMovement.Dashing || _heroMovement.TapDashing)
            {
                _playerAnimator.SetBool("IsDashStriking", true);
                _playerAnimator.SetBool("IsDashing", false);
                DashStriking = true;
                _heroMovement.Dashing = false;
                _heroMovement.TapDashing = false;

            }
            else if (!_isGuardInvoked && !_isSwinging && !_isDashStriking)
            {
                _isSwinging = true;
                _playerAnimator.SetBool("IsAttacking", true);
                onAttackPerformed.Invoke();
                AudioSource.PlayClipAtPoint(_soundManager.CombatSounds[0], this.transform.position, _soundManager.AudioVolume);
            }
        }
        else if (!_isGuardInvoked && !_heroMovement.Dashing && !_isSwinging)
        {
            _isSwinging = true;
            _playerAnimator.SetBool("IsAttacking", true);
            onAttackPerformed.Invoke();
            AudioSource.PlayClipAtPoint(_soundManager.CombatSounds[0], this.transform.position, _soundManager.AudioVolume);
        }
    }

    public void InvokeParry()
    {
        _playerAnimator.SetBool("IsAttacking", true);
        onAttackPerformed.Invoke();
        StartCoroutine(Flash());
        //onParryPerformed?.Invoke();
    }

    private IEnumerator Flash()
    {
        for (int i = 0; i < 2; i++)
        {
            SetSpriteColor(Color.white);
            yield return new WaitForSeconds(0.1f);
            SetSpriteColor(_originalSpriteColor);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SetSpriteColor(Color spriteColor)
    {
        _spriteRenderer.color = spriteColor;
    }

    private void FastFall()
    {
        if (HeroStats.GetElement == Elements.ElementalAttribute.Earth)
        {
            Stomp.SetActive(true);
            IsEarthStomping = true;
            _playerAnimator.SetBool("IsJumping", false);
            _playerAnimator.SetBool("IsFastFall", true);
            StartCoroutine(GravityModifier());
        }
    }

    private IEnumerator GravityModifier()
    {
        _rb.gravityScale = _heroMovement.OriginalGravity * 2f;
        yield return new WaitForSeconds(0.5f);
        _rb.gravityScale = _heroMovement.OriginalGravity;
    }

    private IEnumerator Charging()
    {
        while(_isChargingShot && _chargeAmount < _chargeAmountMax)
        {
            _chargeAmount += 2f;
            yield return new WaitForSeconds(0.5f);
        }
        if(_chargeAmount == _chargeAmountMax)
        {
            StartCoroutine(Flash());
            _isChargeMax = true;
            _isChargingShot = false;
        }
    }

    private void Pause()
    {
        onPausePeformed.Invoke();
    }
}
