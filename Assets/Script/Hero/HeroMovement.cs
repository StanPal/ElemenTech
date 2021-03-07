using System.Collections;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public event System.Action onPlayerFlip;

    private HeroActions _heroActions;
    private HeroStats _heroStats;
    private PlayerInput _playerInput;
    private Animator _playerAnimator;
    private CapsuleCollider2D _capsuleCollider;
    private Collider2D _col2D;
    private AnimationEvents _animationEvents;
    private Rigidbody2D _rb;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private ParticleSystem _dashParticleEffect;

    public enum Controller
    {
        None,
        Keyboard,
        PS4,
        XBOX,
        Gamepad
    }
    public Controller ControllerInput = Controller.None;

    private float _horizontalMove;
    private float _moveInput;
    private bool _onHitLeft = false;
    private float _originalGravity;
    private float _originalRecoveryTime;
    private float _originalMoveSpeed;
    private float _originalJumpForce;

     private bool _isLeft = false;
     private bool _isJumping = false;
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _airStrifeSpeed = 6f;
    [SerializeField] private float _groundJumpForce = 15f;
    [SerializeField] private float _airJumpForce = 10f;
    [SerializeField] private int _numOfJumps = 0;
    [SerializeField] private int _maxJumps = 1;
    [SerializeField] private int _numOfWallJumps = 0;
    [SerializeField] private int _maxWallJump = 1;
    [SerializeField] private float _jumpTimer = 5;
    private bool _isJumpHeld = false;
    private float _jumpTimeCounter = 5;
    private float _extraJumpForce = 0;
    [SerializeField] private float _extraJumpForceRate = 1;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsWall;

    //Dash Modifiers
    private bool _canDash = true;
    private bool _isDashing;
    private bool _isTapDashing;
    [SerializeField] private float _tapDashMultiplier = 0.3f;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashCoolDown = 1f;
    [SerializeField] private float _dashTime = 1f;
    [SerializeField] private float _dashStartUpTime = 1f;

    //Recovery Time until the player can move again 
    [SerializeField] private float _recoveryTime = 1f;
    private bool _isRecovering = false;
    
    [SerializeField] private float _knockBackXRecieved;
    [SerializeField] private float _knockBackYRecieved;
    [SerializeField] private float _knockBackCount;

    [SerializeField] private PhysicsMaterial2D _noFriction;
    [SerializeField] private PhysicsMaterial2D _fullFriction;
    [SerializeField] private float _slopeCheckDistance;
    [SerializeField] private float _maxSlopeAngle;
    private float _slopeDownAngle;
    private float _slopeSideAngle;
    private float _slopeDownAngleOld;
    private bool _isOnSlope;
    private bool _canWalkOnSlope;
    private Vector2 _newVelocity;
    private Vector2 _newForce;
    private Vector2 _col2DSize;
    private Vector2 _slopeNormalPerp;

    //Getters and Setters
    public CapsuleCollider2D GetBoxCollider2D { get => _capsuleCollider;  }
    public PlayerInput PlayerInput { get => _playerInput; } 
    public Rigidbody2D Rigidbody2D { get => _rb; }
    public float Speed { get => _moveSpeed;  set => _moveSpeed = value; } 
    public float RecoveryTime { get => _recoveryTime;  set => _recoveryTime = value; } 
    public float OriginalGravity { get => _originalGravity; }
    public float DashSpeed { get => _dashSpeed; }
    public bool Dashing { get => _isDashing; set => _isDashing = value; } 
    public bool TapDashing { get => _isTapDashing; set => _isTapDashing = value; }
    public bool GetIsLeft { get  => _isLeft; } 
    public bool Recovering { get => _isRecovering;  set => _isRecovering = value; }

    private void Awake()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _col2D = GetComponent<Collider2D>();
        _heroActions = GetComponent<HeroActions>();
        _originalRecoveryTime = _recoveryTime;
        _animationEvents = GetComponentInChildren<AnimationEvents>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _heroStats = GetComponent<HeroStats>();
        _col2DSize = _capsuleCollider.size;
        _canDash = true;
        _jumpTimeCounter = _jumpTimer;
        _originalMoveSpeed = _moveSpeed;
        _originalJumpForce = _groundJumpForce;
     
    }

    private void Start()
    {
        _playerAnimator.SetBool("IsJumping", false);
        _originalGravity = _rb.gravityScale;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            _playerAnimator.SetBool("IsJumping", false);
            _playerAnimator.SetBool("IsMultiJump", false);
            _numOfJumps = _maxJumps;
            _numOfWallJumps = _maxWallJump;
            _moveSpeed = _originalMoveSpeed;
            _groundJumpForce = _originalJumpForce;
            _isJumping = false;
            if(!_isJumpHeld)
            {
                _jumpTimeCounter = _jumpTimer;
                _extraJumpForce = 0;
            }
        }
        else
        {
            _isJumping = true;
            _groundJumpForce = _airJumpForce;
        }

        if(_isJumping)
        {
            _moveSpeed = _airStrifeSpeed; 
            _jumpTimeCounter = _jumpTimer;
            _extraJumpForce = 0;
        }

        SlopeCheck();
        if (!_isRecovering)
        {
            if (ControllerInput == Controller.Keyboard && !_isDashing)
            {
                _moveInput = _playerInput.KeyboardMouse.Move.ReadValue<float>();
            }
            if (ControllerInput == Controller.PS4 && !_isDashing)
            {
                _moveInput = _playerInput.PS4.Move.ReadValue<float>();
            }
            if (ControllerInput == Controller.XBOX && !_isDashing)
            {
                _moveInput = _playerInput.XBOX.Move.ReadValue<float>();
            }
            if (ControllerInput == Controller.Gamepad && !_isDashing)
            {
                _moveInput = _playerInput.Gamepad.Move.ReadValue<float>();
            }
        }

        if(_knockBackCount <= 0)
        {
            _newVelocity.Set(_moveSpeed * _moveInput, _rb.velocity.y);
            _rb.velocity = _newVelocity;
        }
        else
        {
            if (_onHitLeft)
            {
                _rb.velocity = new Vector2(-_knockBackXRecieved, _knockBackYRecieved);
            }
            else
            {
                _rb.velocity = new Vector2(_knockBackXRecieved, _knockBackYRecieved);
            }
            _knockBackCount -= Time.deltaTime;
        }

        if (_isDashing)
        {
            StartCoroutine(Dash(_isLeft,1f));
        }

        if (_isTapDashing)
        {
            StartCoroutine(Dash(_isLeft, _tapDashMultiplier));
        }

        if (_heroActions.DashStriking)
        {
            StartCoroutine(DashStrike());
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
            onPlayerFlip?.Invoke();
       

        }

        if (_moveInput > 0)
        {
            characterScale.x = 1;
            _isLeft = false;
            onPlayerFlip?.Invoke();
        }

        transform.localScale = characterScale;
    }

    private void Update()
    {
        switch (ControllerInput)
        {
            case Controller.None:
                break;
            case Controller.Keyboard:
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Earth))
                {    
                    if(_playerInput.KeyboardMouse.TapJump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            TapJump();
                        }
                    }
                    else if(_playerInput.KeyboardMouse.Jump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            Jump();
                        }
                    }
                }
                else
                {
                    if (_playerInput.KeyboardMouse.Jump.triggered)
                    {
                        if (CheckCanJump())
                        {  
                            Jump();
                        }
                    }
                }
                break;
            case Controller.PS4:
                if (_playerInput.PS4.Jump.triggered && _numOfJumps > 0 || _playerInput.KeyboardMouse.Jump.triggered && IsWall() && _numOfWallJumps > 0)
                {
                    Jump();
                }
                break;
            case Controller.XBOX:
                if (_playerInput.XBOX.Jump.triggered && _numOfJumps > 0 || _playerInput.KeyboardMouse.Jump.triggered && IsWall() && _numOfWallJumps > 0)
                {
                    Jump();
                }
                break;
            case Controller.Gamepad:
                if (_playerInput.Gamepad.Jump.triggered && _numOfJumps > 0)
                {
                    Jump();
                }
                break;
            default:
                break;
        }

        if (ControllerInput == Controller.Keyboard)
        {
            if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water))
            {
                if (_playerInput.KeyboardMouse.DashTap.triggered)
                {
                    OnDashTap();
                }
                else if (_playerInput.KeyboardMouse.Dash.triggered)
                {
                    OnDash();
                }
            }
            else
            {
                _playerInput.KeyboardMouse.Dash.performed += _ => OnDash();
            }
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

        _horizontalMove = _moveInput * _moveSpeed;
        _playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.tag.Equals("Team1"))
        {
            if (collision.collider.tag.Equals("Team1"))
            {
                Physics2D.IgnoreCollision(_capsuleCollider, collision.collider,true);
            }
        }
        if (this.tag.Equals("Team2"))
        {
            if (collision.collider.tag.Equals("Team2"))
            {
                Physics2D.IgnoreCollision(_capsuleCollider, collision.collider, true);
            }
        }
    }

    public void flipCharacter()
    {
        Vector3 characterScale = transform.localScale;
        if(characterScale.x == -1)
        {
            characterScale.x = 1;
            _isLeft = false;

        }
        else if (characterScale.x == 1)
        {
            characterScale.x = -1;
            _isLeft = true;
        }
        transform.localScale = characterScale;
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position -  (Vector3)(new Vector2(0.0f, _col2DSize.y / 2));
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance,_whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, _whatIsGround);
        if(slopeHitFront)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if(slopeHitBack)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeCheckDistance, _whatIsGround);
        if (hit)
        {
            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            //Return angle between y-axis and our normal
            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (_slopeDownAngle != _slopeDownAngleOld)
            {
                _isOnSlope = true;
            }
            _slopeDownAngleOld = _slopeDownAngle;
            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal,Color.green);
        }
        if(_isOnSlope && _moveInput == 0.0f)
        {
            _rb.sharedMaterial = _fullFriction;
        }
        else
        {
            _rb.sharedMaterial = _noFriction;
        }
    }

    public void IcySlidding(float SliddingSpeed)
    {
        _moveSpeed += SliddingSpeed;
    }

    public void SandDecrease(float SandDecreaseSpeed)
    {
        _moveSpeed -= SandDecreaseSpeed;
    }

    public bool IsGrounded()
    {
        float extraHeightText = .05f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(_capsuleCollider.bounds.center, Vector2.down, _capsuleCollider.bounds.extents.y + extraHeightText, _whatIsGround);
        return raycastHit2D.collider != null;
    }

    public bool IsWall()
    {
        float extraLengthText = .15f;
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(_col2D.bounds.center , Vector2.left, -(_col2D.bounds.extents.x + extraLengthText), _whatIsWall);
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(_col2D.bounds.center, Vector2.left, (_col2D.bounds.extents.x + extraLengthText), _whatIsWall);
        Color rayColor;
        if (raycastHit2DLeft.collider != null || raycastHit2DRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_col2D.bounds.center, Vector2.left * -(_col2D.bounds.extents.x + extraLengthText), rayColor);
        Debug.DrawRay(_col2D.bounds.center, Vector2.left * (_col2D.bounds.extents.x + extraLengthText), rayColor);
        if (raycastHit2DLeft.collider != null)
        {
            return true;
        }
        else if (raycastHit2DRight.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDash()
    {
        if (_canDash)
        {
            _playerAnimator.SetBool("IsDashing", true);
            //_playerAnimator.SetTrigger("DashTrigger");
            _isDashing = true;
        }
    }

    private void OnDashTap()
    {
        if (_canDash)
        {
            _tapDashMultiplier = 0.3f;
            _playerAnimator.SetBool("IsDashing", true);
            //_playerAnimator.SetTrigger("DashTrigger");
            _isTapDashing = true;
        }
    }

    private void TapJump()
    {
        _playerAnimator.SetBool("IsJumping", true);
        if (IsWall())
        {
            _numOfWallJumps--;
        }
        else
        {
            _numOfJumps--;
        }
        _rb.velocity = Vector2.up * (_groundJumpForce / 2f);
    }

    private void Jump()
    {
        CreateDust();
        _playerAnimator.SetBool("IsJumping", true);
        _isJumping = true;
        if (IsWall())
        {
            _numOfWallJumps--;
        }
        else
        {
            _numOfJumps--;
        }
        if (_numOfJumps > 0)
        {
            _rb.velocity = Vector2.up * _groundJumpForce;
        }
        if (_numOfWallJumps > 0)
        {
            _rb.velocity = Vector2.up * _groundJumpForce;

        }
    }

    public void OnKnockBackHit(float knockBackX, float knockBackY, float knockBackLength ,bool direction)
    {
        _knockBackCount = knockBackLength;
        _knockBackXRecieved = knockBackX;
        _knockBackYRecieved = knockBackY;
        _onHitLeft = direction;
    }

    private bool CheckCanJump()
    {
        return (_numOfJumps > 0 || ((IsWall() && _numOfWallJumps > 0)));
    }

    private IEnumerator Dash(bool _isLeft, float valueModifier)
    {        
        if (_isLeft)
        {
            _rb.velocity = Vector2.left * _dashSpeed * valueModifier;
        }
        else
        {
            _rb.velocity = Vector2.right * _dashSpeed * valueModifier;
        }
        _rb.gravityScale = 0f;
        yield return new WaitForSeconds(_dashStartUpTime);
        _playerAnimator.SetBool("IsDashing", false);
        _isTapDashing = false;
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = _originalGravity;
        _isDashing = false;
        _isTapDashing = false;
        _canDash = false;
        _isRecovering = true;
        yield return new WaitForSeconds(_dashCoolDown);
        _canDash = true;
    }

    private IEnumerator DashStrike()
    {        
        if (GetIsLeft)
        {
            _rb.velocity = Vector2.left * _dashSpeed;
        }
        else
        {
            _rb.velocity = Vector2.right * _dashSpeed;
        }
        _rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.3f);
        _playerAnimator.SetBool("IsDashStriking", false);
        _heroActions.DashStriking = false;
        _rb.gravityScale = OriginalGravity;
        _rb.velocity = Vector2.zero;
    }

    private IEnumerator Recover()
    {
        _heroActions.enabled = false;
        _isRecovering = true;
        yield return new WaitForSeconds(_recoveryTime);
        _recoveryTime = _originalRecoveryTime;
        _heroActions.enabled = true;
        _isRecovering = false;
    }

    private void CreateDust()
    {
        _dust.Play();
    }

    private void CreateDashPartile()
    {
        _dashParticleEffect.Play();
    }
}
