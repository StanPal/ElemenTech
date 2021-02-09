using System.Collections;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private HeroActions _HeroActions;
    private PlayerInput _playerInput;
    private Animator _PlayerAnimator;
    private BoxCollider2D _BoxCollider2D;
    private Collider2D _Col;
    private AnimationEvents _AnimationEvents;
    private Rigidbody2D _Rb;
    private Collider2D col;

    private float _defendTimeVal = 1f;
    private bool _isDefended = true;
    private float _horizontalMove;
    private float _moveInput;
    private bool _onHitLeft = false;

    public enum Controller
    {
        None,
        Keyboard,
        PS4,
        XBOX,
        Keyboard2,
        Gamepad
    }
    public Controller ControllerInput = Controller.None;

    [SerializeField] private bool _onGround;
    [SerializeField] private bool _isJumpPressure = true;
    [SerializeField] private float jumpPressure = 0f;
    [SerializeField] private float MinjumpPressure = 3f;
    [SerializeField] private float MaxjumpPressure = 10f;

    [SerializeField] private float _speed = 8f;
    [SerializeField] private bool _isLeft = false;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _numOfJumps = 0;
    [SerializeField] private int _maxJumps = 1;
    [SerializeField] private LayerMask _Ground;

    //Dash Modifiers
    [SerializeField] private bool _canDash = true;
    [SerializeField] private bool _isDashing;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashCoolDown = 1f;
    [SerializeField] private float _dashStartUpTime = 1f;

    //Recovery Time until the player can move again 
    [SerializeField] private float _recoveryTime = 1f;
    [SerializeField] private bool _isRecovering = false;
    private float _originalRecoveryTime;

    [SerializeField] private float _knockBackRecieved;
    [SerializeField] private float _knockBackCount;

    //Getters and Setters
    public GameObject defendEffectPrefab;
    public PlayerInput PlayerInput { get { return _playerInput; } }
    public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public bool GetIsLeft { get { return _isLeft; } }
    public float RecoveryTime { get { return _recoveryTime; } set { _recoveryTime = value; } }
    public bool Recovering { get { return _isRecovering; } set { _isRecovering = value; } }

    private void Awake()
    {
        _onGround = true;
        _PlayerAnimator = GetComponentInChildren<Animator>();
        _Rb = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _Col = GetComponent<Collider2D>();
        _HeroActions = GetComponent<HeroActions>();
        _originalRecoveryTime = _recoveryTime;
        _AnimationEvents = GetComponentInChildren<AnimationEvents>();
        _BoxCollider2D = GetComponent<BoxCollider2D>();
        _canDash = true;

        if (ControllerInput == Controller.Keyboard)
        {
            _playerInput.KeyboardMouse.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.Keyboard2)
        {
            _playerInput.KeyboardLayout2.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.PS4)
        {
            _playerInput.PS4.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.XBOX)
        {
            _playerInput.XBOX.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.Gamepad)
        {
            _playerInput.Gamepad.Dash.performed += _ => OnDash();
        }
    }

    private void Start()
    {
        _PlayerAnimator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            _PlayerAnimator.SetBool("IsJumping", false);
            _PlayerAnimator.SetBool("IsMultiJump", false);
            _numOfJumps = _maxJumps;
        }

        if (!_isRecovering)
        {
            if (ControllerInput == Controller.Keyboard && !_isDashing)
            {
                _moveInput = _playerInput.KeyboardMouse.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.PS4 && !_isDashing)
            {
                _moveInput = _playerInput.PS4.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.XBOX && !_isDashing)
            {
                _moveInput = _playerInput.XBOX.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.Keyboard2 && !_isDashing)
            {
                _moveInput = _playerInput.KeyboardLayout2.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.Gamepad && !_isDashing)
            {
                _moveInput = _playerInput.Gamepad.Move.ReadValue<float>();
            }
            else
            {
                Debug.Log("Keybindings not set");
            }
        }

        Vector3 currentPosition = transform.position;
        currentPosition.x += _moveInput * _speed * Time.deltaTime;
        transform.position = currentPosition;

        if (_knockBackCount > 0)
        {
            if (_onHitLeft)
            {
                _Rb.velocity = new Vector2(-_knockBackRecieved, _knockBackRecieved);
            }
            else
            {
                _Rb.velocity = new Vector2(_knockBackRecieved, _knockBackRecieved);

            }
            _knockBackCount--;
        }

        if (_isDashing)
        {
            StartCoroutine(Dash(_isLeft));
        }

        if (_isRecovering)
        {
            StartCoroutine(Recover());
        }

        Vector3 characterScale = transform.localScale;
        if (_moveInput < 0)
        {
            characterScale.x = -1;
            _isLeft = true;
        }

        if (_moveInput > 0)
        {
            characterScale.x = 1;
            _isLeft = false;
        }
    }

    IEnumerator DashStartUp()
    {
        yield return new WaitForSeconds(_dashStartUpTime);
        IsDashing = true;
    }

    private void Update()
    {
        _numOfJumps = _maxJumps;
        switch (ControllerInput)
        {
            case Controller.None:
                break;
            case Controller.Keyboard:
                if (_playerInput.KeyboardMouse.Jump.triggered && _numOfJumps > 0)
                {
                    if (_playerInput.KeyboardMouse.Jump.triggered && _numOfJumps > 0)
                    {

                        if (jumpPressure < MaxjumpPressure)
                        {
                            Debug.Log("JumpPressure < MaJumpPressure successful!");
                            jumpPressure += Time.deltaTime * 10f;
                        }
                        else
                        {

                            jumpPressure = MaxjumpPressure;
                            Debug.Log("JumpPressure  =  MaxJumpPressure successful!");
                        }

                    }
                    else if (_playerInput.KeyboardMouse.JumpRelease.triggered && _numOfJumps > 0)
                    {
                        Debug.Log("hold:" + jumpPressure);

                        if (jumpPressure > 0)
                        {
                            jumpPressure += MinjumpPressure;

                            Debug.Log("jumpPressure + MinjumpPressure = " + jumpPressure);

                            _Rb.velocity = Vector2.up * (_jumpForce + jumpPressure);
                            jumpPressure = 0f;
                            _onGround = false;
                            Debug.Log("PressureJump successful!");
                        }
                        _numOfJumps--;
                        Debug.Log("number of jumps : " + _numOfJumps);

                    }
                }
                if (_playerInput.KeyboardMouse.Jump.triggered && _numOfJumps > 0)
                {
                    Jump();
                }
                else if (_playerInput.KeyboardMouse.Jump.triggered && _numOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            case Controller.Keyboard2:
                if (_playerInput.KeyboardLayout2.Jump.triggered && _numOfJumps > 0)
                {
                    if (_playerInput.KeyboardLayout2.JumpHold.triggered && _numOfJumps > 0)
                    {

                        if (jumpPressure < MaxjumpPressure)
                        {
                            jumpPressure += Time.deltaTime * 10f;
                        }
                        else
                        {
                            jumpPressure = MaxjumpPressure;
                        }
                        Debug.Log("hold:" + jumpPressure);
                    }
                    else
                    {
                        if (jumpPressure > 0)
                        {
                            jumpPressure += MinjumpPressure;
                            _Rb.velocity = Vector2.up * (_jumpForce * jumpPressure);
                            //jumpPressure = 0f;
                        }
                    }
                }
                else if (_playerInput.KeyboardLayout2.Jump.triggered && _numOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                else
                {

                }
                break;
            case Controller.PS4:
                if (_playerInput.PS4.Jump.triggered && _numOfJumps > 0)
                {
                    Jump();
                }
                else if (_playerInput.PS4.Jump.triggered && _numOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;

            case Controller.XBOX:
                if (_playerInput.XBOX.Jump.triggered && _numOfJumps > 0)
                {
                    Jump();
                }
                else if (_playerInput.XBOX.Jump.triggered && _numOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            case Controller.Gamepad:
                if (_playerInput.Gamepad.Jump.triggered && _numOfJumps > 0)
                {
                    Jump();
                }
                else if (_playerInput.Gamepad.Jump.triggered && _numOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            default:
                break;
        }
        _horizontalMove = _moveInput * _speed;
        _PlayerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void IcySlidding(float SliddingSpeed)
    {
        _speed += SliddingSpeed;
    }

    public void SandDecrease(float SandDecreaseSpeed)
    {
        _speed -= SandDecreaseSpeed;
    }

    public bool IsGrounded()
    {
        float extraHeightText = .05f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(_BoxCollider2D.bounds.center, Vector2.down, _BoxCollider2D.bounds.extents.y + extraHeightText, _Ground);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_BoxCollider2D.bounds.center, Vector2.down * (_BoxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        return raycastHit2D.collider != null;
    }

    private void OnDash()
    {
        if (_canDash)
        {
            _PlayerAnimator.SetTrigger("DashTrigger");
            StartCoroutine(DashStartUp());
        }
    }

    private void Jump()
    {
        _PlayerAnimator.SetBool("IsJumping", true);
        _Rb.velocity = Vector2.up * _jumpForce;
        _numOfJumps--;
    }

    private void MultiJump()
    {
        _Rb.velocity = Vector2.up * _jumpForce;
    }

    private IEnumerator Dash(bool _IsLeft)
    {
        Vector3 currentPosition = transform.position;
        if (_IsLeft)
        {
            currentPosition.x -= (_dashSpeed * 0.1f);
        }

        else
        {
            currentPosition.x += (_dashSpeed * 0.1f);
        }

        transform.position = currentPosition;
        float gravity = _Rb.gravityScale;
        _Rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.4f);
        _Rb.gravityScale = 1f;
        _isDashing = false;
        _canDash = false;
        _isRecovering = true;
        yield return new WaitForSeconds(_dashCoolDown);
        _canDash = true;
    }

    private IEnumerator Recover()
    {
        _HeroActions.enabled = false;
        _isRecovering = true;
        yield return new WaitForSeconds(_recoveryTime);
        _recoveryTime = _originalRecoveryTime;
        _HeroActions.enabled = true;
        _isRecovering = false;
    }

    public void OnKnockBackHit(float knockbackamount, bool direction)
    {
        _knockBackCount++;
        _knockBackRecieved = knockbackamount;
        _onHitLeft = direction;
    }
}
