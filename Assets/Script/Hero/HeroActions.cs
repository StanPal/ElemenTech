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
    public Transform PivotPoint;
    public Transform FirePoint;

    private Guard _Guard;
    private HeroMovement _HeroMovement;
    private HeroStats _HeroStats;
    private PlayerInput _PlayerInput;
    private bool _IsGuardInvoked = false;
    private float mNextFireTime;

    [SerializeField] private bool _IsOnCooldown = false;
    [SerializeField] private Vector2 _LookDirection;
    [SerializeField] private float _LookAngle;
    [SerializeField] private Vector2 _AxisPos; 

    // Getters & Setters
    public HeroMovement HeroMovement { get { return _HeroMovement; } }
    public HeroStats HeroStats { get { return _HeroStats; } }
    public PlayerInput PlayerInput { get { return _PlayerInput; } }
    public bool IsCooldown { get { return _IsOnCooldown; } set { _IsOnCooldown = value; } }
    public Vector2 GetLookDir { get { return _LookDirection; } }
    public float GetLookAngle { get { return _LookAngle; } }

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _HeroMovement = GetComponent<HeroMovement>();
        _HeroStats = GetComponent<HeroStats>();
        _PlayerInput = new PlayerInput();
        _Guard = GetComponent<Guard>();     
    }

    private void Start()
    {
        if (!_HeroMovement.Recovering)
        {
            if (_HeroMovement.ControllerInput == HeroMovement.Controller.Keyboard)
            {
                _PlayerInput.KeyboardMouse.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.KeyboardMouse.ElementSpecial1.performed += _ => ElementSpecial1();

                if (!this.gameObject.GetComponent<Guard>().isShieldDisabled)
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
                if (!this.gameObject.GetComponent<Guard>().isShieldDisabled)
                {
                    _PlayerInput.KeyboardLayout2.Guard.performed += _ => Guard();
                }
                _PlayerInput.KeyboardLayout2.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.KeyboardLayout2.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.PS4)
            {
                _PlayerInput.PS4.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.PS4.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().isShieldDisabled)
                {
                    _PlayerInput.PS4.Guard.performed += _ => Guard();
                }
                _PlayerInput.PS4.GuardRelease.performed += _ => GuardRelease();
                _PlayerInput.PS4.Pause.performed += _ => Pause();
            }

            if (HeroMovement.ControllerInput == HeroMovement.Controller.XBOX)
            {
                _PlayerInput.XBOX.SwordSwing.performed += _ => SwordSwing();
                _PlayerInput.XBOX.ElementSpecial1.performed += _ => ElementSpecial1();
                if (!this.gameObject.GetComponent<Guard>().isShieldDisabled)
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
                //_AxisPos = _PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>();
                _LookDirection = Camera.main.ScreenToWorldPoint(_PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>()) - transform.position;
                _LookAngle = Mathf.Atan2(_LookDirection.y, _LookDirection.x) * Mathf.Rad2Deg;
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

    private void OnEnable()
    {
        _PlayerInput.Enable();
    }
    private void OnDisable()
    {
        _PlayerInput.Disable();
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
                mNextFireTime = Time.time + HeroStats.CoolDown;
                onSkillPerformed.Invoke(HeroStats.GetElement);
            }
        }
    }

    private void SwordSwing()
    {
        if (!_IsGuardInvoked)
        {
            Sword.gameObject.SetActive(true);
            onAttackPerformed.Invoke();
        }
    }

    private void Pause()
    {
        Debug.Log("Called");
        onPausePeformed.Invoke();
    } 
}
