using System.Collections;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public event System.Action onPlayerFlip;

    private HeroActions _heroActions;
    private HeroStats _heroStats;
    private PlayerInput _playerInput;
    private Animator _playerAnimator;
    private SwordAttack _swordAttack;
    private CapsuleCollider2D _capsuleCollider;
    private Collider2D _col2D;
    private AnimationEvents _animationEvents;
    private Rigidbody2D _rb;
    private FastFallJump _fastFallJump;
    [SerializeField] private GameObject _Crosshair;
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

    public enum Team
    {
        Team1,
        Team2,
        Team3,
        Team4
    }

    public Team OnTeam = Team.Team1;

    private float _horizontalMove;
    private float _moveInput;
    private bool _onHitLeft = false;
    private float _originalGravity;
    private float _originalRecoveryTime;
    private float _originalMoveSpeed;
    private float _originalJumpForce;

    private bool _isLeft = false;
    private bool _isJumping = false;
    private float _selfKnockBack;

    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _airStrifeSpeed = 6f;
    [SerializeField] private float _groundJumpForce = 15f;
    [SerializeField] private float _airJumpForce = 10f;
    [SerializeField] private int _numOfJumps = 0;
    [SerializeField] private int _maxJumps = 1;
    [SerializeField] private int _numOfWallJumps = 0;
    [SerializeField] private int _maxWallJump = 1;
    [SerializeField] private float _jumpTimer = 5;
    //[SerializeField] private float _clampValue = 10f;
    private bool _isJumpHeld = false;
    private float _jumpTimeCounter = 5;
    private float _extraJumpForce = 0;
    [SerializeField] private float _extraJumpForceRate = 1;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsWall;

    //Dash & Weight Modifiers
    private bool _isWeightShifting = false;
    private bool _canDash = true;
    private bool _isDashing;
    private bool _isTapDashing;
    [SerializeField] private float _tapDashMultiplier = 0.3f;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashCoolDown = 1f;
    [SerializeField] private float _dashTime = 1f;
    [SerializeField] private float _dashEndTime = 0.3f;
    [SerializeField] private float _dashAttackDistance = 3f;

    //Recovery Time until the player can move again 
    [SerializeField] private float _recoveryTime = 1f;
    private bool _isRecovering = false;

    //KnockBack
    [SerializeField] private float _knockBackXRecieved;
    [SerializeField] private float _knockBackYRecieved;
    [SerializeField] private float _knockBackCount;

    //Slope & Friction 
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
    public CapsuleCollider2D GetBoxCollider2D { get => _capsuleCollider; }
    public PlayerInput PlayerInput { get => _playerInput; }
    public Rigidbody2D Rigidbody2D { get => _rb; }
    public float Speed { get => _moveSpeed; set => _moveSpeed = value; }
    public float OriginalMoveSpeed { get => _originalMoveSpeed; }
    public float RecoveryTime { get => _recoveryTime; set => _recoveryTime = value; }
    public float OriginalGravity { get => _originalGravity; }
    public float NumberOfJumps { get => _numOfJumps; set => value = _numOfJumps; }
    public bool WeightShifting { get => _isWeightShifting; }
    public float DashSpeed { get => _dashSpeed; }
    public bool Dashing { get => _isDashing; set => _isDashing = value; }
    public bool TapDashing { get => _isTapDashing; set => _isTapDashing = value; }
    public bool GetIsLeft { get => _isLeft; set => _isLeft = value; }
    public bool Recovering { get => _isRecovering; set => _isRecovering = value; }
    public float SelfKnockBack { get => _selfKnockBack; }
    public float MoveInput { get => _moveInput; }

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
        _fastFallJump = GetComponent<FastFallJump>();
        _swordAttack = GetComponentInChildren<SwordAttack>();
        _col2DSize = _capsuleCollider.size;
        _canDash = true;
        _jumpTimeCounter = _jumpTimer;
        _originalMoveSpeed = _moveSpeed;
        _originalJumpForce = _groundJumpForce;

    }

    private void Start()
    {
        //_playerAnimator.SetBool("IsJumping", false);
        _originalGravity = _rb.gravityScale;
        _swordAttack.onPlayerChargeAttack += onPlayerChargeAttack;
    }


    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            _playerAnimator.SetBool("IsJumping", false);
            _playerAnimator.SetBool("IsMultiJump", false);
            _numOfJumps = _maxJumps;
            _numOfWallJumps = _maxWallJump;
            _groundJumpForce = _originalJumpForce;
            _fastFallJump.Weight = _fastFallJump.OriginalWeight;
            _isJumping = false;
            _heroActions.ChargeMax = false;
            _heroActions.ChargeAmount = 0;
            if (!_isJumpHeld)
            {
                _jumpTimeCounter = _jumpTimer;
                _extraJumpForce = 0f;
            }
            //_rb.velocity = new Vector2(_moveSpeed, Mathf.Clamp(_rb.velocity.y, _rb.gravityScale, _clampValue));
        }
        else
        {
            _isJumping = true;
            _groundJumpForce = _airJumpForce;
            //_rb.velocity = new Vector2(_moveSpeed, Mathf.Clamp(_rb.velocity.y, -_rb.gravityScale, -_clampValue));
        }

        if (_isJumping)
        {
            _moveSpeed = _airStrifeSpeed;
            _jumpTimeCounter = _jumpTimer;
            _extraJumpForce = 0f;
        }

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

        if (_isDashing)
        {
            StartCoroutine(Dash(_isLeft, 1f));
            StartCoroutine(DisableCrossHair());
        }

        if (_isTapDashing)
        {
            StartCoroutine(Dash(_isLeft, _tapDashMultiplier));
            StartCoroutine(DisableCrossHair());
        }

        if (_heroActions.DashStriking)
        {
            StartCoroutine(DashStrike());
        }

        if (_isRecovering)
        {
            StartCoroutine(Recover());
        }

        #region Movement
        if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Earth) && _heroActions.IsEarthStomping)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        if (_knockBackCount >= 0)
        {
            if (_onHitLeft)
            {
                if (_knockBackYRecieved != float.NaN)
                {
                    _rb.velocity = new Vector2(-_knockBackXRecieved, _knockBackYRecieved);
                }
            }
            else
            {
                if (_knockBackYRecieved != float.NaN)
                {
                    _rb.velocity = new Vector2(_knockBackXRecieved, _knockBackYRecieved);
                }
            }
            _knockBackCount -= Time.deltaTime;
        }
        else
        {
            //*****HARD FIX Look into why this occurs****** 
            if (_rb.velocity.y != float.NaN)
            {
                _rb.velocity = new Vector2(_moveInput * _moveSpeed, _rb.velocity.y);
            }
            else
            {

            }
        }

        if (_selfKnockBack >= 0 && _heroStats.GetElement.Equals(Elements.ElementalAttribute.Air))
        {
            _rb.velocity = new Vector2(_knockBackXRecieved, _knockBackYRecieved);
            _selfKnockBack -= Time.deltaTime;
        }
        #endregion
    }

    private void Update()
    {


        switch (ControllerInput)
        {
            case Controller.None:
                break;
            case Controller.Keyboard:
                #region Keyboard
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water))
                {
                    if (_playerInput.KeyboardMouse.TapDash.triggered)
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
                    if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Air))
                    {
                        if (_playerInput.KeyboardMouse.WeightShiftHold.triggered)
                        {
                            OnWeightShift();
                        }
                        if (_playerInput.KeyboardMouse.WeightShiftRelease.triggered)
                        {
                            OnWeightShiftRelease();
                        }
                    }
                    else
                    {
                        _playerInput.KeyboardMouse.Dash.performed += _ => OnDash();
                    }
                }
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Earth))
                {
                    if (_playerInput.KeyboardMouse.TapJump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            TapJump();
                        }
                    }
                    else if (_playerInput.KeyboardMouse.Jump.triggered)
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
            #endregion 
            case Controller.PS4:
                #region PS4
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water))
                {
                    if (_playerInput.PS4.TapDash.triggered)
                    {
                        OnDashTap();
                    }
                    else if (_playerInput.PS4.Dash.triggered)
                    {
                        OnDash();
                    }
                }
                else
                {
                    if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Air))
                    {
                        if (_playerInput.PS4.WeightShiftHold.triggered)
                        {
                            OnWeightShift();
                        }
                        if (_playerInput.PS4.WeightShiftRelease.triggered)
                        {
                            OnWeightShiftRelease();
                        }
                    }
                    else
                    {
                        _playerInput.PS4.Dash.performed += _ => OnDash();
                    }
                }
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Earth))
                {
                    if (_playerInput.PS4.TapJump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            TapJump();
                        }
                    }
                    else if (_playerInput.PS4.Jump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            Jump();
                        }
                    }
                }
                else
                {
                    if (_playerInput.PS4.Jump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            Jump();
                        }
                    }
                }
                break;
            #endregion
            case Controller.XBOX:
                #region XBOX
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water))
                {
                    if (_playerInput.XBOX.TapDash.triggered)
                    {
                        OnDashTap();
                    }
                    else if (_playerInput.XBOX.Dash.triggered)
                    {
                        OnDash();
                    }
                }
                else
                {
                    if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Air))
                    {
                        if (_playerInput.XBOX.WeightShiftHold.triggered)
                        {
                            OnWeightShift();
                        }
                        if (_playerInput.XBOX.WeightShiftRelease.triggered)
                        {
                            OnWeightShiftRelease();
                        }
                    }
                    else
                    {
                        _playerInput.XBOX.Dash.performed += _ => OnDash();
                    }
                }
                if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Earth))
                {
                    if (_playerInput.XBOX.TapJump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            TapJump();
                        }
                    }
                    else if (_playerInput.XBOX.Jump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            Jump();
                        }
                    }
                }
                else
                {
                    if (_playerInput.XBOX.Jump.triggered)
                    {
                        if (CheckCanJump())
                        {
                            Jump();
                        }
                    }
                }
                break;
            #endregion
            case Controller.Gamepad:
                if (_playerInput.Gamepad.Jump.triggered)
                {
                    Jump();
                }
                break;
            default:
                break;
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
        if (collision.collider.tag.Equals(tag))
        {            
           Physics2D.IgnoreCollision(_capsuleCollider, collision.collider, true);
        }

        #region SpecificWaterDash
        if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water) && this.tag.Equals("Team1"))
        {
            if (_isDashing || _isTapDashing || _heroActions.DashStriking)
            {
                if (collision.collider.tag.Equals("Team2") || collision.collider.tag.Equals("Team3")
                    || collision.collider.tag.Equals("Team4"))
                {
                    Physics2D.IgnoreCollision(_capsuleCollider, collision.collider, true);
                    StartCoroutine(ResetCollision(collision.collider));
                }
            }
        }
        if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water) && this.tag.Equals("Team2"))
        {
            if (_isDashing || _isTapDashing || _heroActions.DashStriking)
            {
                if (collision.collider.tag.Equals("Team1") || collision.collider.tag.Equals("Team3")
                || collision.collider.tag.Equals("Team4"))
                {
                    Physics2D.IgnoreCollision(_capsuleCollider, collision.collider, true);
                    StartCoroutine(ResetCollision(collision.collider));

                }
            }
        }

        if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water) && this.tag.Equals("Team3"))
        {
            if (_isDashing || _isTapDashing || _heroActions.DashStriking)
            {
                if (collision.collider.tag.Equals("Team1") || collision.collider.tag.Equals("Team2")
                || collision.collider.tag.Equals("Team4"))
                {
                    Physics2D.IgnoreCollision(_capsuleCollider, collision.collider, true);
                    StartCoroutine(ResetCollision(collision.collider));

                }
            }
        }

        if (_heroStats.GetElement.Equals(Elements.ElementalAttribute.Water) && this.tag.Equals("Team4"))
        {
            if (_isDashing || _isTapDashing || _heroActions.DashStriking)
            {
                if (collision.collider.tag.Equals("Team1") || collision.collider.tag.Equals("Team2")
                || collision.collider.tag.Equals("Team3"))
                {
                    Physics2D.IgnoreCollision(_capsuleCollider, collision.collider, true);
                    StartCoroutine(ResetCollision(collision.collider));

                }
            }
        }
        #endregion
    }

    private void onPlayerChargeAttack()
    {
        _numOfJumps = 0;
        _isWeightShifting = false;
    }

    public void flipCharacter()
    {
        Vector3 characterScale = transform.localScale;
        if (characterScale.x == -1)
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

    //private void SlopeCheck()
    //{
    //    Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, _col2DSize.y / 2));
    //    SlopeCheckHorizontal(checkPos);
    //    SlopeCheckVertical(checkPos);
    //}

    //private void SlopeCheckHorizontal(Vector2 checkPos)
    //{
    //    RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance, _whatIsGround);
    //    RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, _whatIsGround);
    //    if (slopeHitFront)
    //    {
    //        _isOnSlope = true;
    //        _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
    //    }
    //    else if (slopeHitBack)
    //    {
    //        _isOnSlope = true;
    //        _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
    //    }
    //    else
    //    {
    //        _slopeSideAngle = 0.0f;
    //        _isOnSlope = false;
    //    }
    //}

    //private void SlopeCheckVertical(Vector2 checkPos)
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeCheckDistance, _whatIsGround);
    //    if (hit)
    //    {
    //        _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
    //        //Return angle between y-axis and our normal
    //        _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
    //        if (_slopeDownAngle != _slopeDownAngleOld)
    //        {
    //            _isOnSlope = true;
    //        }
    //        _slopeDownAngleOld = _slopeDownAngle;
    //        Debug.DrawRay(hit.point, _slopeNormalPerp, Color.red);
    //        Debug.DrawRay(hit.point, hit.normal, Color.green);
    //    }
    //    if (_isOnSlope && _moveInput == 0.0f)
    //    {
    //        _rb.sharedMaterial = _fullFriction;
    //    }
    //    else
    //    {
    //        _rb.sharedMaterial = _noFriction;
    //    }
    //}

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
        float extraHeightText = 0.5f;
        RaycastHit2D raycastHit2D = Physics2D.CapsuleCast(_capsuleCollider.bounds.center, _capsuleCollider.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, extraHeightText, _whatIsGround);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        return raycastHit2D.collider != null;
    }

    public bool IsWall()
    {
        float extraLengthText = .25f;
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(_col2D.bounds.center, Vector2.left, -(_col2D.bounds.extents.x + extraLengthText), _whatIsWall);
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
        if (raycastHit2DLeft.collider != null)
        {
            Debug.Log(raycastHit2DLeft.collider);
            return true;
        }
        if (raycastHit2DRight.collider != null)
        {
            Debug.Log(raycastHit2DRight.collider);
            return true;
        }
        return false;

    }

    private void OnDash()
    {
        if (_canDash && !_heroActions.IsEarthStomping)
        {
            _playerAnimator.SetBool("IsDashing", true);
            _isDashing = true;
        }
    }

    private void OnDashTap()
    {
        if (_canDash)
        {
            Debug.Log("TapDash Triggered");
            _playerAnimator.SetBool("IsDashing", true);
            _isTapDashing = true;
        }
    }

    private void OnWeightShift()
    {
        _isWeightShifting = true;
        StartCoroutine(DropWeight());
    }

    private void OnWeightShiftRelease()
    {
        _isWeightShifting = false;
        _fastFallJump.Weight = _fastFallJump.OriginalWeight;
    }

    private void TapJump()
    {
        CreateDust();
        _playerAnimator.SetBool("IsJumping", true);
        _playerAnimator.SetTrigger("JumpTrigger");

        _isJumping = true;
        if (IsWall())
        {
            if (_numOfWallJumps > 0)
            {
                _rb.velocity = Vector2.up * (_groundJumpForce * 0.5f);
                _numOfWallJumps--;
            }
        }
        else
        {
            if (_numOfJumps > 0)
            {
                _rb.velocity = Vector2.up * (_groundJumpForce * 0.5f);
                _numOfJumps--;
            }
        }
    }

    private void Jump()
    {
        CreateDust();
        _playerAnimator.SetBool("IsJumping", true);
        _playerAnimator.SetTrigger("JumpTrigger");
        _isJumping = true;
        if (IsWall())
        {
            if (_numOfWallJumps > 0)
            {
                _rb.velocity = Vector2.up * _groundJumpForce;
                _numOfWallJumps--;
            }
        }
        else
        {
            if (_numOfJumps > 0)
            {
                if (WeightShifting)
                {
                    _rb.velocity = Vector2.up * ((_groundJumpForce / (_fastFallJump.OriginalWeight - _fastFallJump.Weight)) / 2f);
                    _numOfJumps--;
                }
                else
                {
                    _rb.velocity = Vector2.up * _groundJumpForce;
                }
                _numOfJumps--;
            }
        }
    }

    public void OnKnockBackHit(float knockBackX, float knockBackY, float knockBackLength, bool direction)
    {
        Debug.Log(knockBackY);
        _knockBackCount = knockBackLength;
        _knockBackXRecieved = knockBackX;
        _knockBackYRecieved = knockBackY;
        _onHitLeft = direction;
    }

    public void OnSelfKnockBack(Vector2 KnockBack, float knockBackLength)
    {
        _selfKnockBack = knockBackLength;
        if (_isWeightShifting)
        {            
            _knockBackXRecieved = KnockBack.x * 2f;
            _knockBackYRecieved = KnockBack.y * 2f;
        }
        else
        { 
            _knockBackXRecieved = KnockBack.x;
            _knockBackYRecieved = KnockBack.y;
        }
    }

    private bool CheckCanJump()
    {
        return (_numOfJumps > 0 || ((IsWall() && _numOfWallJumps > 0)));
    }


    //Coroutines Start

    private IEnumerator DropWeight()
    {
        while (_isWeightShifting && _fastFallJump.Weight > _fastFallJump.WeightMin)
        {
            _fastFallJump.Weight -= _fastFallJump.WeightModifier;
            yield return new WaitForSeconds(_fastFallJump.WeightDropRate);
        }
    }

    private IEnumerator ResetCollision(Collider2D enemyCollider)
    {
        Debug.Log("Collision Timer Started");
        yield return new WaitForSeconds(_dashEndTime);
        Physics2D.IgnoreCollision(_capsuleCollider, enemyCollider, false);
    }

    private IEnumerator Dash(bool _isLeft, float valueModifier)
    {
        if (_isDashing)
        {
            if (_isLeft)
            {
                _rb.velocity = Vector2.left * _dashSpeed;
            }
            else
            {
                _rb.velocity = Vector2.right * _dashSpeed;
            }
            _dashEndTime = 0.3f;

        }
        if (_isTapDashing)
        {
            if (_isLeft)
            {
                _rb.velocity = Vector2.left * _tapDashMultiplier;
            }
            else
            {
                _rb.velocity = Vector2.right * _tapDashMultiplier;
            }
            _dashEndTime = 0.1f;
        }
        _rb.gravityScale = 0f;

        yield return new WaitForSeconds(_dashEndTime);
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

    private IEnumerator DisableCrossHair()
    {
        _Crosshair.SetActive(false);
        yield return new WaitForSeconds(1f);
        _Crosshair.SetActive(true);
    }

    private IEnumerator DashStrike()
    {
        CreateDashPartile();
        if (_isLeft)
        {
            _rb.velocity = Vector2.left * _dashAttackDistance;
        }
        else
        {
            _rb.velocity = Vector2.right * _dashAttackDistance;
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

    //Coroutines End


    //SoundEffects Start

    private void CreateDust()
    {
        _dust.Play();
    }

    private void CreateDashPartile()
    {
        _dashParticleEffect.Play();
    }

    //SoundEffects End
}
