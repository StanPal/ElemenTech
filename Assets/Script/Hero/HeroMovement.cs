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

    [SerializeField] private bool _IsLeft = false;
    [SerializeField] private bool _IsJumping = false;
    [SerializeField] private float _JumpForce = 5f;
    [SerializeField] private int _NumOfJumps = 0;
    [SerializeField] private int _MaxJumps = 1;
    [SerializeField] private int _NumOfWallJump = 0;
    [SerializeField] private int _MaxWallJump = 1;
    [SerializeField] private LayerMask _Ground;
    [SerializeField] private LayerMask _Wall;
    [SerializeField] private bool _CanDash = true;
    [SerializeField] private bool _IsDashing;
    [SerializeField] private float _DashSpeed = 5f;
    [SerializeField] private float _DashCooldown = 1f;
    [SerializeField] private float _DashStartUpTime = 1f;
    [SerializeField] private float mSpeed = 8f;
    [SerializeField] private float _KnockBackRecieved;
    [SerializeField] private float _KnockBackCount;
    [SerializeField] private float _RecoveryTime = 1f;
    [SerializeField] private bool _IsRecovering = false;

    private HeroActions _HeroActions;
    private PlayerInput _PlayerInput;
    private Collider2D _Col;
    private float _MoveInput;
    private Rigidbody2D _Rb;
    private bool _OnHitLeft = false;
    private float _OriginalRecoveryTime;

    // Getters/Setters 
    public PlayerInput PlayerInput { get { return _PlayerInput; } }
    public bool GetIsLeft { get { return _IsLeft; } }
    public float Speed { get { return mSpeed; } set { mSpeed = value; } }
    public float RecoveryTime { get { return _RecoveryTime; } set { _RecoveryTime = value; } }
    public bool Recovering { get { return _IsRecovering; } set { _IsRecovering = value; } }

    private void Awake()
    {
        _Rb = GetComponent<Rigidbody2D>();
        _PlayerInput = new PlayerInput();
        _Col = GetComponent<Collider2D>();
        _HeroActions = GetComponent<HeroActions>();
        _OriginalRecoveryTime = _RecoveryTime;

        _CanDash = true;
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
    }

    private void Update()
    {
        if (IsGrounded())
        {
            _NumOfJumps = _MaxJumps;
            _NumOfWallJump = _MaxWallJump;
        }
        switch (ControllerInput)
        {
            case Controller.None:
                break;
            case Controller.Keyboard:
                if (_PlayerInput.KeyboardMouse.Jump.triggered && _NumOfJumps > 0 || _PlayerInput.KeyboardMouse.Jump.triggered && IsWall() && _NumOfWallJump >0)
                {
                    Jump();
                }
                break;
            case Controller.Keyboard2:
                if (_PlayerInput.KeyboardLayout2.Jump.triggered && _NumOfJumps > 0 || _PlayerInput.KeyboardMouse.Jump.triggered && IsWall() && _NumOfWallJump > 0)
                {
                    Jump();
                }
                break;
            case Controller.PS4:
                if (_PlayerInput.PS4.Jump.triggered && _NumOfJumps > 0 || _PlayerInput.KeyboardMouse.Jump.triggered && IsWall() && _NumOfWallJump > 0)
                {
                    Jump();
                }
                break;
            case Controller.XBOX:
                if (_PlayerInput.XBOX.Jump.triggered && _NumOfJumps > 0 || _PlayerInput.KeyboardMouse.Jump.triggered && IsWall() && _NumOfWallJump > 0)
                {
                    Jump();
                }
                break;
            default:
                break;
        }
        if (IsWall())
        {

        }
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
        float extraHeightText = .05f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(_Col.bounds.center, Vector2.down, _Col.bounds.extents.y + extraHeightText, _Ground);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_Col.bounds.center, Vector2.down * (_Col.bounds.extents.y + extraHeightText), rayColor);
        return raycastHit2D.collider != null;
    }

    public bool IsWall()
    {
        float extraLengthText = .05f;
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(_Col.bounds.center , Vector2.left, -(_Col.bounds.extents.x + extraLengthText), _Wall);
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(_Col.bounds.center, Vector2.left, (_Col.bounds.extents.x + extraLengthText), _Wall);
        Color rayColor;
        if (raycastHit2DLeft.collider != null || raycastHit2DRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_Col.bounds.center, Vector2.left * -(_Col.bounds.extents.x + extraLengthText), rayColor);
        Debug.DrawRay(_Col.bounds.center, Vector2.left * (_Col.bounds.extents.x + extraLengthText), rayColor);
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
        if (IsWall())
        {
            _NumOfWallJump--;
        }
        else
        {
            _NumOfJumps--;
        }
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
        _Rb.gravityScale = 1f;
        _IsDashing = false;
        _CanDash = false;
        _IsRecovering = true;
        yield return new WaitForSeconds(_DashCooldown);
        _CanDash = true;
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
