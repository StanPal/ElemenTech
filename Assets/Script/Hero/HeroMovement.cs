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

    public Controller controllerInput;
    [SerializeField]
    private bool isLeft = false;
    public bool GetIsLeft { get { return isLeft; } }


    [SerializeField]
    float mJumpSpeed = 5f;
    [SerializeField]
    private int mNumOfJumps = 0;
    [SerializeField]
    private int mMaxJumps = 2;
    [SerializeField]
    private LayerMask mGround;
    private Collider2D col;

    [SerializeField]
    private bool isDashing;
    [SerializeField]
    private float dashSpeed = 5f;
    //[SerializeField]
    //private float dashTime;
    //private float distanceBetweenImages;
    //private float dashCoolDown;
    //private float dashTimeLeft;
    //private float lastImageXpos;
    //private float lastDash = -100f;

    [SerializeField]
    private float mSpeed;
    public float Speed { get { return mSpeed; } set { mSpeed = value; } }
    private float mMoveInput;
    private Rigidbody2D rb; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mPlayerInput = new PlayerInput();
        col = GetComponent<Collider2D>();

        if (controllerInput == Controller.Keyboard)
        {
            mPlayerInput.KeyboardMouse.Jump.performed += _ => Jump();
            mPlayerInput.KeyboardMouse.Dash.performed += _ => OnDash();
        }
        if (controllerInput == Controller.PS4)
            mPlayerInput.PS4.Jump.performed += _ => Jump();
        if (controllerInput == Controller.XBOX)
            mPlayerInput.XBOX.Jump.performed += _ => Jump();
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
        if (IsGrounded() || mNumOfJumps < mMaxJumps)
        {
            rb.AddForce(new Vector2(0, mJumpSpeed), ForceMode2D.Impulse);
            mNumOfJumps += 1;
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
        isDashing = true;
    }

    private void Update()
    {

    }

    IEnumerator Dash(float direction)
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x += dashSpeed;
        transform.position = currentPosition;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = 1f;
    }
    void FixedUpdate()
    {

        if (controllerInput == Controller.Keyboard)
            mMoveInput = mPlayerInput.KeyboardMouse.Move.ReadValue<float>();
        else if (controllerInput == Controller.PS4)
            mMoveInput = mPlayerInput.PS4.Move.ReadValue<float>();
        else if (controllerInput == Controller.XBOX)
            mMoveInput = mPlayerInput.XBOX.Move.ReadValue<float>();
        else
            Debug.Log("Keybindings not set");

            Vector3 currentPosition = transform.position;
            currentPosition.x += mMoveInput * mSpeed * Time.deltaTime;
            transform.position = currentPosition;

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

   
}
