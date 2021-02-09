using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HeroMovement : MonoBehaviour
{
    HeroActions mHeroActions;

    private PlayerInput mPlayerInput;
    public PlayerInput PlayerInput { get { return mPlayerInput; } }
    public enum Controller
    {
        None,
        Keyboard,
        PS4,
        XBOX,
        Keyboard2
    }

    public Controller controllerInput = Controller.None;
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
    bool OnGround;
    [SerializeField]
    private bool IsJumpPressure = true;
    [SerializeField]
    private float jumpPressure = 0f;
    [SerializeField]
    private float MinjumpPressure = 3f;
    [SerializeField]
    private float MaxjumpPressure = 10f;
    [SerializeField]
    private LayerMask mGround;
    private Collider2D col;


    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    public bool isDashing;
    [SerializeField]
    private float mDashSpeed = 5f;
    [SerializeField]
    private float mDashCoolDown = 1f;
    [SerializeField]
    private float mDashStartUpTime = 1f;

    public GameObject defendEffectPrefab;
    private float defendTimeVal = 1;
    private bool isDefended = true;

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

    [SerializeField]
    private float mRecoveryTime = 1f;
    public float RecoveryTime { get { return mRecoveryTime; } set { mRecoveryTime = value; } }
    [SerializeField]
    private bool isRecovering = false;
    public bool Recovering { get { return isRecovering; } set { isRecovering = value; } }
    private float mOriginalRecoveryTime;

    private void Awake()
    {
        OnGround = true;
        rb = GetComponent<Rigidbody2D>();
        mPlayerInput = new PlayerInput();
        col = GetComponent<Collider2D>();
        mHeroActions = GetComponent<HeroActions>();
        mOriginalRecoveryTime = mRecoveryTime;

        canDash = true;
        if (controllerInput == Controller.Keyboard)
        {
            mPlayerInput.KeyboardMouse.Dash.performed += _ => OnDash();
        }
        if (controllerInput == Controller.Keyboard2)
        {
            mPlayerInput.KeyboardLayout2.Dash.performed += _ => OnDash();
        }
        if (controllerInput == Controller.PS4)
        {
            mPlayerInput.PS4.Dash.performed += _ => OnDash();
        }
        if (controllerInput == Controller.XBOX)
        {
            mPlayerInput.XBOX.Dash.performed += _ => OnDash();
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
            mNumOfJumps = mMaxJumps;
        }
        
        switch (controllerInput)
        {
            case Controller.None:
                break;
            case Controller.Keyboard:

                if (GetComponent<HeroStats>().mElementalType == Elements.ElementalAttribute.Earth)
                {

                    
                        if (mPlayerInput.KeyboardMouse.Jump.triggered && mNumOfJumps > 0)
                        {
                           
                            if (jumpPressure < MaxjumpPressure)
                            {
                                print("JumpPressure < MaJumpPressure successful!");
                                jumpPressure += Time.deltaTime * 10f;
                            }
                            else
                            {

                                jumpPressure = MaxjumpPressure;
                            print("JumpPressure  =  MaxJumpPressure successful!");
                            }

                        }
                        else if (mPlayerInput.KeyboardMouse.JumpRelease.triggered && mNumOfJumps > 0)
                        {
                            print("hold:" + jumpPressure);
                            
                        if (jumpPressure > 0)
                            {
                                jumpPressure += MinjumpPressure;

                            print("jumpPressure + MinjumpPressure = " + jumpPressure);

                                rb.velocity = Vector2.up * (mJumpForce + jumpPressure);
                                 jumpPressure = 0f;
                                OnGround = false;
                                print("PressureJump successful!");
                            }
                            mNumOfJumps--;
                            print("number of jumps : " + mNumOfJumps);

                    }

                    

                }
                else if (mPlayerInput.KeyboardMouse.Jump.triggered && mNumOfJumps > 0)
                {
                    Jump();
                }
                else if (mPlayerInput.KeyboardMouse.Jump.triggered && mNumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;


            case Controller.Keyboard2:
                if (GetComponent<HeroStats>().mElementalType == Elements.ElementalAttribute.Earth)
                {
                    if (mPlayerInput.KeyboardLayout2.JumpHold.triggered && mNumOfJumps > 0)
                    {

                        if(jumpPressure < MaxjumpPressure)
                        {
                            jumpPressure += Time.deltaTime * 10f;
                        }
                        else
                        {
                            jumpPressure = MaxjumpPressure;
                        }
                        print("hold:" + jumpPressure);
                    }
                    else
                    {
                        if (jumpPressure > 0)
                        {
                            jumpPressure += MinjumpPressure;
                            rb.velocity = Vector2.up * (mJumpForce * jumpPressure);
                            //jumpPressure = 0f;
                        }
                    }
                }
                    
                else if (mPlayerInput.KeyboardLayout2.Jump.triggered && mNumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                else
                {
                    Jump();
                }

                break;
            case Controller.PS4:
                if (mPlayerInput.PS4.Jump.triggered && mNumOfJumps > 0)
                {
                    Jump();
                }
                else if (mPlayerInput.PS4.Jump.triggered && mNumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            case Controller.XBOX:
                if (mPlayerInput.XBOX.Jump.triggered && mNumOfJumps > 0)
                {
                    Jump();
                }
                else if (mPlayerInput.XBOX.Jump.triggered && mNumOfJumps == 0 && IsGrounded())
                {
                    MultiJump();
                }
                break;
            default:
                break;
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * mJumpForce;
        mNumOfJumps--;
    }

    private void MultiJump()
    { 
        rb.velocity = Vector2.up * mJumpForce;
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.tag == "wall")
    //    {
    //        OnGround = true;
    //    }
    //}

    
    private void FixedUpdate()
    {
        //OnGround = true;

        if (!isRecovering)
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
            else if (controllerInput == Controller.Keyboard2 && !isDashing)
            {
                mMoveInput = mPlayerInput.KeyboardLayout2.Move.ReadValue<float>();
            }
            else
            {
                Debug.Log("Keybindings not set");
            }
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

        if(isRecovering)
        {
            StartCoroutine(Recover());
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
            currentPosition.x -= (mDashSpeed * 0.1f);
        }
        else
        {
            currentPosition.x += (mDashSpeed * 0.1f);
        }
        transform.position = currentPosition;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.4f);
        rb.gravityScale = 1f;
        isDashing = false;
        canDash = false;
        isRecovering = true;
        yield return new WaitForSeconds(mDashCoolDown);
        canDash = true;
    }

    IEnumerator Recover()
    {
        mHeroActions.enabled = false;
        isRecovering = true;
        yield return new WaitForSeconds(mRecoveryTime);
        mRecoveryTime = mOriginalRecoveryTime;
        mHeroActions.enabled = true;
        isRecovering = false;
    }

    public void OnKnockBackHit(float knockbackamount, bool direction)
    {
        mKnockbackCount++;
        mKnockbackRecieved = knockbackamount;
        mOnHitLeft = direction;
    }

}
