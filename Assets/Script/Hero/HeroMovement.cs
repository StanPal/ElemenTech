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
        Keyboard,
        PS4,
        XBOX,
        Other
    }

    public Controller controllerInput;
    [SerializeField]
    bool isLeft = false;
    public bool GetIsLeft { get { return isLeft; } }


    [SerializeField]
    float mJumpSpeed = 5f;
    [SerializeField]
    int mNumOfJumps = 0;
    [SerializeField]
    int mMaxJumps = 2;
    [SerializeField]
    private LayerMask mGround;
    private Collider2D col;

    [SerializeField]
    float mSpeed;
    private float mMoveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mPlayerInput = new PlayerInput();
        col = GetComponent<Collider2D>();

        if (controllerInput == Controller.Keyboard)
            mPlayerInput.KeyboardMouse.Jump.performed += _ => Jump();
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
        if(IsGrounded())
        {
            mNumOfJumps = 0;
        }
        if (IsGrounded() || mNumOfJumps < mMaxJumps)
        {
            rb.AddForce(new Vector2(0, mJumpSpeed), ForceMode2D.Impulse);
            mNumOfJumps += 1;
        }
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        //if (isGrounded == true)
        //{
        //    isJumping = true;
        //    isGrounded = false;
        //    jumpTimeCounter = jumpTime;
        //    rb.velocity = Vector2.up * mJumpForce;
        //}
        //if (isJumping == true)
        //{
        //    if (jumpTimeCounter > 0)
        //    {
        //        rb.velocity = Vector2.up * mJumpForce;
        //        jumpTimeCounter -= Time.deltaTime;
        //    }
        //    else
        //        isJumping = false;
        //}
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x;
        bottomRight.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRight, mGround);
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

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    MagicSkill();
        //}
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    onPausePeformed.Invoke();
        //}
        //if (Input.GetKey(KeyCode.G))
        //{
        //    mSpeed = 5;
        //    onGuardPerformed.Invoke();
        //}
        //else
        //{
        //    onGuardExit.Invoke();
        //    mSpeed = 10;
        //}
    }     
    }
