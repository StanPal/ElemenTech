using System.Collections;
using UnityEngine;
public class HeroMovement : MonoBehaviour
{
    public enum Controller
    {
        None,
        Keyboard,
        PS4,
        XBOX,
        Keyboard2
    }
    public Controller ControllerInput = Controller.None;
    private Animator _PlayerAnimator;
    private float _HorizontalMove;

    [SerializeField]
    private bool isLeft = false;
    public bool GetIsLeft { get { return isLeft; } }

    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private float mJumpForce = 5f;
    [SerializeField]
    private int mNumOfJumps = 0;
    [SerializeField]
    private int mMaxJumps = 1;
    [SerializeField]
    private LayerMask mGround;
    private Collider2D col;


    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    private bool isDashing;
    [SerializeField]
    private float mDashSpeed = 5f;
    [SerializeField]
    private float mDashCoolDown = 1f;
    [SerializeField]
    private float mDashStartUpTime = 1f;
    private AnimationEvents _AnimationEvents;

    // Getters/Setters 
    public PlayerInput PlayerInput { get { return _PlayerInput; } }
    public bool GetIsLeft { get { return _IsLeft; } }
    public float Speed { get { return mSpeed; } set { mSpeed = value; } }
    private float mMoveInput;
    private Rigidbody2D rb;

    [SerializeField]
    private float mKnockbackRecieved;
    [SerializeField]
    private float mKnockbackCount;
    private bool mOnHitLeft = false;

    [SerializeField]
    private float mRecoveryTime = 1f;
    public float RecoveryTime { get { return mRecoveryTime; } set { mRecoveryTime = value; } }
    [SerializeField]
    private bool isRecovering = false;
    public bool Recovering { get { return isRecovering; } set { isRecovering = value; } }
    private float mOriginalRecoveryTime;
    
    private void Awake()
    {
        _PlayerAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mPlayerInput = new PlayerInput();
        col = GetComponent<Collider2D>();
        mHeroActions = GetComponent<HeroActions>();
        mOriginalRecoveryTime = mRecoveryTime;
        _AnimationEvents = GetComponentInChildren<AnimationEvents>();
        canDash = true;
        if (controllerInput == Controller.Keyboard)
        {
            mPlayerInput.KeyboardMouse.Dash.performed += _ => OnDash();
        }
        if (controllerInput == Controller.Keyboard2)
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
        if(IsGrounded())
        {
            _PlayerAnimator.SetBool("IsJumping", false);

        }
    }
    private void OnEnable()
    {
        mPlayerInput.Enable();
    }
    private void OnDisable()
    {
        mPlayerInput.Disable();
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
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x;
        bottomRight.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRight, mGround);
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
        yield return new WaitForSeconds(mDashStartUpTime);

        isDashing = true;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            _PlayerAnimator.SetBool("IsJumping", false);
            _PlayerAnimator.SetBool("IsMultiJump", false);
            mNumOfJumps = mMaxJumps;
        }
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
                if (mPlayerInput.XBOX.Jump.triggered && mNumOfJumps > 0)
                {                    
                    Jump();
                }
                else if (_PlayerInput.XBOX.Jump.triggered && _NumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            default:
                break;
        }
        _HorizontalMove = mMoveInput * mSpeed;
        _PlayerAnimator.SetFloat("Speed", Mathf.Abs(_HorizontalMove));
    }

    private void Jump()
    {
        _PlayerAnimator.SetBool("IsJumping", true);
        rb.velocity = Vector2.up * mJumpForce;
        mNumOfJumps--;
    }

    private void MultiJump()
    {  
        rb.velocity = Vector2.up * mJumpForce;
    }

    private void FixedUpdate()
    {
        if (!_IsRecovering)
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
            else
            {
                Debug.Log("Keybindings not set");
            }
        }


        Vector3 currentPosition = transform.position;
        currentPosition.x += _MoveInput * mSpeed * Time.deltaTime;
        transform.position = currentPosition;

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

        if (_IsRecovering)
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

    private void OnEnable()
    {
        _PlayerInput.Enable();
    }
    private void OnDisable()
    {
        _PlayerInput.Disable();
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
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= _Col.bounds.extents.x;
        topLeftPoint.y += _Col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += _Col.bounds.extents.x;
        bottomRight.y -= _Col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRight, _Ground);
    }

    private void OnDash()
    {
        if (_CanDash)
        {
            StartCoroutine(DashStartUp());
        }
    }

    private IEnumerator DashStartUp()
    {
        yield return new WaitForSeconds(_DashStartUpTime);
        _IsDashing = true;
    }

    private void Jump()
    {
        _Rb.velocity = Vector2.up * _JumpForce;
        _NumOfJumps--;
    }

    private void MultiJump()
    {
        _Rb.velocity = Vector2.up * _JumpForce;
    }

    private IEnumerator Dash(bool _IsLeft)
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
        //.DashProjectileInvincibility = false;
        rb.gravityScale = 1f;
        isDashing = false;
        canDash = false;
        isRecovering = true;
        yield return new WaitForSeconds(mDashCoolDown);
        canDash = true;
    }

    private IEnumerator Recover()
    {
        _HeroActions.enabled = false;
        _IsRecovering = true;
        yield return new WaitForSeconds(_RecoveryTime);
        _RecoveryTime = _OriginalRecoveryTime;
        _HeroActions.enabled = true;
        _IsRecovering = false;
    }

    public void OnKnockBackHit(float knockbackamount, bool direction)
    {
        _KnockBackCount++;
        _KnockBackRecieved = knockbackamount;
        _OnHitLeft = direction;
    }
}
