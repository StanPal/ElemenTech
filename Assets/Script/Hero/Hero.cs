﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;

    public enum Controller
    {
        PS4,
        KeyBoard
    };

    [SerializeField]
    Controller controllerType;
    [SerializeField]
    string mName;
    [SerializeField]
    float mAttack;
    [SerializeField]
    float mMaxHealth;
    float mCurrentHealth;

    public float CurrentHealth { get { return mCurrentHealth; } }
    public float MaxHealth { get { return mMaxHealth; } }

    [SerializeField]
    Elements.ElementalAttribute mElementalType;
    public Elements.ElementalAttribute GetElement { get { return mElementalType; } }

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
    Vector2 mPosition = Vector2.zero;

    [SerializeField]
    Transform arrowPosition;
    public GameObject projectile;

    void Awake()
    {
        mCurrentHealth = mMaxHealth;
        rb = GetComponent<Rigidbody2D>();

    }


    void FixedUpdate()
    {
        if (controllerType == Controller.KeyBoard)
        {
            //move
            mMoveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(mMoveInput * mSpeed, rb.velocity.y);
        }
        else if (controllerType == Controller.PS4)
        {
            mMoveInput = Input.GetAxis("PS4Horizontal");
            rb.velocity = new Vector2(mMoveInput * mSpeed, rb.velocity.y);
        }

    }

    void Update()
    {
         isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (controllerType == Controller.KeyBoard)
        {
            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                isGrounded = false;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * mJumpForce;
            }
            if (Input.GetKey(KeyCode.Space) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * mJumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                rangeAttack();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                onSkillPerformed.Invoke(GetElement);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                onPausePeformed.Invoke();
            }
            if (Input.GetKey(KeyCode.G))
                onGuardPerformed.Invoke();
            //else
            //    onGuardExit.Invoke();
        }
        else if(controllerType == Controller.PS4)
        {
            if (isGrounded == true && Input.GetButtonDown("PS4Jump"))
            {
                isJumping = true;
                isGrounded = false;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * mJumpForce;
            }
            if (Input.GetButtonDown("PS4Jump") && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * mJumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetButtonDown("PS4Jump"))
            {
                isJumping = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                rangeAttack();
            }

            if (Input.GetButtonDown("PS4Skill"))
            {
                onSkillPerformed.Invoke(GetElement);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                onPausePeformed.Invoke();
            }
            if (Input.GetKey(KeyCode.G))
                onGuardPerformed.Invoke();
            //else
            //    onGuardExit.Invoke();
        }
    }

    void rangeAttack()
    {
        Instantiate(projectile, arrowPosition.position, arrowPosition.rotation);
    }

    void HeroDie()
    {
       //Destroy(gameObject);
    }

}
