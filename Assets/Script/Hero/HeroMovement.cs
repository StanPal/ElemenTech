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
    float mJumpForce;
    [SerializeField]
    Transform feetPos;
    [SerializeField]
    float checkRadius;
    bool isGrounded;
    bool isJumping;
    [SerializeField]
    float jumpTime;
    float jumpTimeCounter;
    [SerializeField]
    LayerMask whatIsGround;

    [SerializeField]
    float mSpeed;
    private float mMoveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mPlayerInput = new PlayerInput();

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
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            isJumping = true;
            isGrounded = false;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * mJumpForce;
        }
        if (isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * mJumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }
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

        Debug.Log(mMoveInput);

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
