using System.Collections;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private HeroActions _HeroActions;
    private PlayerInput _PlayerInput;
    private Animator _PlayerAnimator;
    private BoxCollider2D _BoxCollider2D;
    private Collider2D _Col;
    private AnimationEvents _AnimationEvents;
    private Rigidbody2D _Rb;

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

    private float _HorizontalMove;
    private float _MoveInput;
    private bool _OnHitLeft = false;

    [SerializeField] private float mSpeed = 8f;
    [SerializeField] private bool _IsLeft = false;
    [SerializeField] private bool _IsJumping = false;
    [SerializeField] private float _JumpForce = 5f;
    [SerializeField] private int _NumOfJumps = 0;
    [SerializeField] private int _MaxJumps = 1;
    [SerializeField] private LayerMask _Ground;

    //Dash Modifiers
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool _IsDashing;
    [SerializeField] private float _DashSpeed = 5f;
    [SerializeField] private float _DashCoolDown = 1f;
    [SerializeField] private float _DashStartUpTime = 1f;

    //Recovery Time until the player can move again 
    [SerializeField] private float _RecoveryTime = 1f;
    [SerializeField] private bool isRecovering = false;
    private float _OriginalRecoveryTime;
    
    [SerializeField] private float _KnockBackRecieved;
    [SerializeField] private float _KnockBackCount;

    [SerializeField] private PhysicsMaterial2D _NoFriction;
    [SerializeField] private PhysicsMaterial2D _FullFriction;

    [SerializeField] private float _SlopeCheckDistance;
    [SerializeField] private float _MaxSlopeAngle;
    private float _SlopeDownAngle;
    private float _SlopeSideAngle;
    private float _SlopeDownAngleOld;
    private bool _IsOnSlope;
    private bool _CanWalkOnSlope;
    private Vector2 _NewVelocity;
    private Vector2 _NewForce;
    private Vector2 _ColliderSize;
    private Vector2 _SlopeNormalPerp;

    //Getters and Setters
    public BoxCollider2D GetBoxCollider2D { get { return _BoxCollider2D; } }
    public PlayerInput PlayerInput { get { return _PlayerInput; } }
    public bool Dashing { get { return _IsDashing; } }
    public float Speed { get { return mSpeed; } set { mSpeed = value; } }
    public bool GetIsLeft { get { return _IsLeft; } }
    public float RecoveryTime { get { return _RecoveryTime; } set { _RecoveryTime = value; } }
    public bool Recovering { get { return isRecovering; } set { isRecovering = value; } }
    
    private void Awake()
    {
        _PlayerAnimator = GetComponentInChildren<Animator>();
        _Rb = GetComponent<Rigidbody2D>();
        _PlayerInput = new PlayerInput();
        _Col = GetComponent<Collider2D>();
        _HeroActions = GetComponent<HeroActions>();
        _OriginalRecoveryTime = _RecoveryTime;
        _AnimationEvents = GetComponentInChildren<AnimationEvents>();
        _BoxCollider2D = GetComponent<BoxCollider2D>();
        _ColliderSize = _BoxCollider2D.size;
        canDash = true;
        if (ControllerInput == Controller.Keyboard)
        {
            _PlayerInput.KeyboardMouse.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.Keyboard2)
        {
            _PlayerInput.KeyboardLayout2.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.PS4)
        {
            _PlayerInput.PS4.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.XBOX)
        {
            _PlayerInput.XBOX.Dash.performed += _ => OnDash();
        }
        if (ControllerInput == Controller.Gamepad)
        {
            _PlayerInput.Gamepad.Dash.performed += _ => OnDash();
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
            _NumOfJumps = _MaxJumps;
        }

        SlopeCheck();
        if (!isRecovering)
        {
            if (ControllerInput == Controller.Keyboard && !_IsDashing)
            {
                _MoveInput = _PlayerInput.KeyboardMouse.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.PS4 && !_IsDashing)
            {
                _MoveInput = _PlayerInput.PS4.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.XBOX && !_IsDashing)
            {
                _MoveInput = _PlayerInput.XBOX.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.Keyboard2 && !_IsDashing)
            {
                _MoveInput = _PlayerInput.KeyboardLayout2.Move.ReadValue<float>();
            }
            else if (ControllerInput == Controller.Gamepad && !_IsDashing)
            {
                _MoveInput = _PlayerInput.Gamepad.Move.ReadValue<float>();
            }
            else
            {
                Debug.Log("Keybindings not set");
            }
        }

        _NewVelocity.Set(mSpeed * _MoveInput, _Rb.velocity.y);
        _Rb.velocity = _NewVelocity;
        //Vector3 currentPosition = transform.position;
        //currentPosition.x += _MoveInput * mSpeed * Time.deltaTime;
        //transform.position = currentPosition;

        //if(IsGrounded() && !_IsOnSlope)
        //{
        //    _NewVelocity.Set(mSpeed * _MoveInput, 0.0f);
        //    _Rb.velocity = _NewVelocity;
        //}
        //else if(IsGrounded() && _IsOnSlope)
        //{
        //    _NewVelocity.Set(mSpeed * _SlopeNormalPerp.x * -_MoveInput, mSpeed * _SlopeNormalPerp.y * -_MoveInput);
        //    _Rb.velocity = _NewVelocity;
        //}
        //else if(!IsGrounded())
        //{
        //    _NewVelocity.Set(mSpeed * _MoveInput, _Rb.velocity.y);
        //    _Rb.velocity = _NewVelocity;
        //}

        if (_KnockBackCount > 0)
        {
            if (_OnHitLeft)
            {
                _Rb.velocity = new Vector2(-_KnockBackRecieved, _KnockBackRecieved);
            }
            else
            {
                _Rb.velocity = new Vector2(_KnockBackRecieved, _KnockBackRecieved);

            }
            _KnockBackCount--;
        }

        if (_IsDashing)
        {
            StartCoroutine(Dash(_IsLeft));
        }

        if (isRecovering)
        {
            StartCoroutine(Recover());
        }

        Vector3 characterScale = transform.localScale;
        if (_MoveInput < 0)
        {
            characterScale.x = -1;
            _IsLeft = true;
        }

        if (_MoveInput > 0)
        {
            characterScale.x = 1;
            _IsLeft = false;
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
                if (_PlayerInput.KeyboardMouse.Jump.triggered && _NumOfJumps > 0)
                {
                    Jump();
                }
                else if (_PlayerInput.KeyboardMouse.Jump.triggered && _NumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            case Controller.Keyboard2:
                if (_PlayerInput.KeyboardLayout2.Jump.triggered && _NumOfJumps > 0)
                {
                    Jump();
                }
                else if (_PlayerInput.KeyboardLayout2.Jump.triggered && _NumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            case Controller.PS4:
                if (_PlayerInput.PS4.Jump.triggered && _NumOfJumps > 0)
                {
                    Jump();
                }
                else if (_PlayerInput.PS4.Jump.triggered && _NumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            case Controller.XBOX:
                if (_PlayerInput.XBOX.Jump.triggered && _NumOfJumps > 0)
                {
                    Jump();
                }
                else if (_PlayerInput.XBOX.Jump.triggered && _NumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            case Controller.Gamepad:
                if (_PlayerInput.Gamepad.Jump.triggered && _NumOfJumps > 0)
                {
                    Jump();
                }
                else if (_PlayerInput.Gamepad.Jump.triggered && _NumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            default:
                break;
        }
        _HorizontalMove = _MoveInput * mSpeed;
        _PlayerAnimator.SetFloat("Speed", Mathf.Abs(_HorizontalMove));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.tag.Equals("Team1"))
        {
            if (collision.collider.tag.Equals("Team1"))
            {
                Physics2D.IgnoreCollision(_BoxCollider2D, collision.collider,true);
            }
        }
        if (this.tag.Equals("Team2"))
        {
            if (collision.collider.tag.Equals("Team2"))
            {
                Physics2D.IgnoreCollision(_BoxCollider2D, collision.collider,true);
            }
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

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position -  (Vector3)(new Vector2(0.0f, _ColliderSize.y / 2));
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _SlopeCheckDistance,_Ground);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _SlopeCheckDistance, _Ground);
        if(slopeHitFront)
        {
            _IsOnSlope = true;
            _SlopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if(slopeHitBack)
        {
            _IsOnSlope = true;
            _SlopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _SlopeSideAngle = 0.0f;
            _IsOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _SlopeCheckDistance, _Ground);
        if (hit)
        {
            _SlopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            //Return angle between y-axis and our normal
            _SlopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (_SlopeDownAngle != _SlopeDownAngleOld)
            {
                _IsOnSlope = true;
            }
            _SlopeDownAngleOld = _SlopeDownAngle;
            Debug.DrawRay(hit.point, _SlopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal,Color.green);
        }
        if(_IsOnSlope && _MoveInput == 0.0f)
        {
            _Rb.sharedMaterial = _FullFriction;
        }
        else
        {
            _Rb.sharedMaterial = _NoFriction;
        }
    }

    public void IcySlidding(float SliddingSpeed)
    {
        mSpeed += SliddingSpeed;
    }

    public void SandDecrease(float SandDecreaseSpeed)
    {
        mSpeed -= SandDecreaseSpeed;
    }

    public bool IsGrounded()
    {
        float extraHeightText = .05f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(_BoxCollider2D.bounds.center, Vector2.down, _BoxCollider2D.bounds.extents.y + extraHeightText, _Ground);
        //Color rayColor;
        //if (raycastHit2D.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}
        //Debug.DrawRay(_BoxCollider2D.bounds.center, Vector2.down * (_BoxCollider2D.bounds.extents.y + extraHeightText),rayColor);
        return raycastHit2D.collider != null;
    }

    private void OnDash()
    {
        if (canDash)
        {
            _PlayerAnimator.SetTrigger("DashTrigger");
            StartCoroutine(DashStartUp());
        }
    }

    IEnumerator DashStartUp()
    {
        yield return new WaitForSeconds(_DashStartUpTime);

        _IsDashing = true;
    }

    private void Jump()
    {
        _PlayerAnimator.SetBool("IsJumping", true);

        _Rb.velocity = Vector2.up * _JumpForce;
        _NumOfJumps--;
    }

    private void MultiJump()
    {  
        _Rb.velocity = Vector2.up * _JumpForce;
    }

    IEnumerator Dash(bool _IsLeft)
    { 
        Vector3 currentPosition = transform.position;
        if (_IsLeft)
        {
            currentPosition.x -= (_DashSpeed * 0.1f);
        }

        else
        {
            currentPosition.x += (_DashSpeed * 0.1f);
        }

        transform.position = currentPosition;
        float gravity = _Rb.gravityScale;
        _Rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.4f);
        _Rb.gravityScale = 1f;
        _IsDashing = false;
        canDash = false;
        isRecovering = true;
        yield return new WaitForSeconds(_DashCoolDown);
        canDash = true;
    }

    IEnumerator Recover()
    {
        _HeroActions.enabled = false;
        isRecovering = true;
        yield return new WaitForSeconds(_RecoveryTime);
        _RecoveryTime = _OriginalRecoveryTime;
        _HeroActions.enabled = true;
        isRecovering = false;
    }

    public void OnKnockBackHit(float knockbackamount, bool direction)
    {
        _KnockBackCount++;
        _KnockBackRecieved = knockbackamount;
        _OnHitLeft = direction;
    }
}
