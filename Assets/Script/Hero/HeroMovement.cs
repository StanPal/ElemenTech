using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HeroMovement : MonoBehaviour
{
    private PlayerInput mPlayerInput;
    public PlayerInput PlayerInput { get { return mPlayerInput; } }
    public enum Controller
    {
        None,
        Keyboard,
        PS4,
        XBOX
    }

    public Controller controllerInput = Controller.None;
    [SerializeField]
    private bool isLeft = false;
    public bool GetIsLeft { get { return isLeft; } }

    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private float mJumpSpeed = 5f;
    [SerializeField]
    private int mNumOfJumps = 0;
    [SerializeField]
    private int mMaxJumps = 2;
    [SerializeField]
    private LayerMask mGround;
    private Collider2D col;

    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    private bool isDashing;
    [SerializeField]
    private float dashSpeed = 5f;
    [SerializeField]
    private float dashCoolDown = 1f;


    [SerializeField]
    private float mSpeed;
    public float Speed { get { return mSpeed; } set { mSpeed = value; } }
    private float mMoveInput;
    private Rigidbody2D rb;

    [SerializeField]
    private float mKnockbackRecieved;
    [SerializeField]
    private float mKnockbackCount;
    private bool mOnHitLeft = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mPlayerInput = new PlayerInput();
        col = GetComponent<Collider2D>();
        canDash = true;
        if (controllerInput == Controller.Keyboard)
        {
            mPlayerInput.KeyboardMouse.Jump.performed += _ => Jump();
            mPlayerInput.KeyboardMouse.Dash.performed += _ => OnDash();
        }
        if (controllerInput == Controller.PS4)
        {
            mPlayerInput.PS4.Jump.performed += _ => Jump();
        }
        if (controllerInput == Controller.XBOX)
        {
            mPlayerInput.XBOX.Jump.performed += _ => Jump();
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

    private void Jump()
    {      
        if (IsGrounded())
        {
            mNumOfJumps = 0;
        }
        if (IsGrounded() || mNumOfJumps <= mMaxJumps)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, mJumpSpeed), ForceMode2D.Impulse);            
            mNumOfJumps++;
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
            isDashing = true;
        }
    }


    void FixedUpdate()
    {

        if (controllerInput == Controller.Keyboard && !isDashing)
        {
            mMoveInput = mPlayerInput.KeyboardMouse.Move.ReadValue<float>();
        }
        else if (controllerInput == Controller.PS4 && !isDashing)
        {
            mMoveInput = mPlayerInput.PS4.Move.ReadValue<float>();
        }
        else if (controllerInput == Controller.XBOX && !isDashing)
        {
            mMoveInput = mPlayerInput.XBOX.Move.ReadValue<float>();
        }
        else
        {
            Debug.Log("Keybindings not set");
        }

        Vector3 currentPosition = transform.position;
        currentPosition.x += mMoveInput * mSpeed * Time.deltaTime;
        transform.position = currentPosition;

        if (mKnockbackCount > 0)
        {
            if (mOnHitLeft)
            {
                rb.velocity = new Vector2(-mKnockbackRecieved, mKnockbackRecieved);
            }
            else
            {
                rb.velocity = new Vector2(mKnockbackRecieved, mKnockbackRecieved);

            }
            mKnockbackCount--;
        }

        if(isDashing)
        {
            StartCoroutine(Dash(isLeft));
        }
        Vector3 characterScale = transform.localScale;
        if (mMoveInput < 0)
        {
            characterScale.x = -1;
            isLeft = true;
        }
        if (mMoveInput > 0)
        {
            characterScale.x = 1;
            isLeft = false;
        }
        transform.localScale = characterScale;
    }

    IEnumerator Dash(bool isLeft)
    {

        Vector3 currentPosition = transform.position;
        if (isLeft)
        {
            currentPosition.x -= (dashSpeed * 0.1f);
        }
        else
        {
            currentPosition.x += (dashSpeed * 0.1f);
        }
        transform.position = currentPosition;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.4f);
        rb.gravityScale = 1f;
        isDashing = false;
        canDash = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    public void OnKnockBackHit(float knockbackamount, bool direction)
    {
        mKnockbackCount++;
        mKnockbackRecieved = knockbackamount;
        mOnHitLeft = direction;
    }

}
