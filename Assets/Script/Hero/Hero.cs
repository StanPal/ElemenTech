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
        KeyBoard,
        Xbox,
        None
    };
    bool isLeft = false;
    public bool GetIsLeft { get { return isLeft; } }
    
    public Controller controllerType;
    [SerializeField]
    string mName;
    [SerializeField]
    float mAttack;
    [SerializeField]
    float mMaxHealth;
    float mCurrentHealth;
    [SerializeField]
    float mCoolDown;
    float mTempCDTime;
    bool isCDFinished;

    public bool IsCDFinished { get { return isCDFinished; } }
    public float CoolDown { get { return mCoolDown; } }
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
        mTempCDTime = 0;
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
        Vector3 characterScale = transform.localScale;
        if (rb.velocity.x < 0)
        {
            characterScale.x = -1;
            isLeft = true;
        }
        if (rb.velocity.x > 0)
        {
            characterScale.x = 1;
            isLeft = false;
        }
        transform.localScale = characterScale;

    }

    void Update()
    {
         isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(mTempCDTime <= 0.0f)
        {
            mTempCDTime = 0.0f;
            isCDFinished = true;
        }

        if(mTempCDTime > 0.0f)
        {
            mTempCDTime -= Time.deltaTime;
        }

        // Imput for keyboard
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                MagicSkill();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                onPausePeformed.Invoke();
            }
            if (Input.GetKey(KeyCode.G))
            {
                mSpeed = 5;
                onGuardPerformed.Invoke();
            }
            else
            {
                onGuardExit.Invoke();
                mSpeed = 10;
            }
        }

        // // Imput for ps4
        if (controllerType == Controller.PS4)
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

            if (Input.GetButtonUp("PS4Skill"))
            {
                MagicSkill();
            }
            if (Input.GetButtonDown("PS4Pause"))
            {
                onPausePeformed.Invoke();
            }
            if (Input.GetButton("PS4Guard"))
            {
                onGuardPerformed.Invoke();
                mSpeed = 5;

            }
            else
            {
                onGuardExit.Invoke();
                mSpeed = 10;
            }
        }
    }

    void MagicSkill()
    {
        if (isCDFinished)
        {
            onSkillPerformed.Invoke(GetElement);
            mTempCDTime = mCoolDown;
            isCDFinished = false;
        }
    }

    void rangeAttack()
    {
        Instantiate(projectile, arrowPosition.position, arrowPosition.rotation);
    }

    public void TakeDamage(float damage)
    {
        mCurrentHealth -= damage;
        if (mCurrentHealth <= 0)
            HeroDie();
    }

    void HeroDie()
    {
        this.gameObject.SetActive(false);
       //Destroy(gameObject);
    }

}
