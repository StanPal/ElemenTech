﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public event System.Action onSkillPerformed;

    [SerializeField]
    string mName;
    [SerializeField]
    float mAttack;
    [SerializeField]
    float mMaxHealth;
    float mCurrentHealth;

    [SerializeField]
    float mSpeed;
    private float mMoveInput;
    private Rigidbody2D rb;
    Vector2 mPosition = Vector2.zero;
    
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
    Elements.ElementalAttribute mElementalType = Elements.ElementalAttribute.Earth;
    public Elements.ElementalAttribute GetElement { get { return mElementalType; } }

    [SerializeField]
    Transform arrowPosition;
    public GameObject projectile;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mCurrentHealth = mMaxHealth;
    }


    void FixedUpdate()
    {
        //move
        mMoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(mMoveInput * mSpeed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            isGrounded = false;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * mJumpForce;
        }
        if (Input.GetKey(KeyCode.Space)&& isJumping == true)
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

        if(Input.GetKeyUp(KeyCode.E))
        {
            onSkillPerformed.Invoke();
        }
    }

    void HeroDie()
    {
       Destroy(gameObject);
    }

    void rangeAttack()
    {
        Instantiate(projectile, arrowPosition.position, arrowPosition.rotation);
    }


}
