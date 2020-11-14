using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFallJump : MonoBehaviour
{
    [SerializeField]
    private float mfallMultiplier = 2.5f;
    //[SerializeField]
    //private float mLowJumpMultiplier = 2f;

    Rigidbody2D rb;
    private HeroMovement mHeroMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mHeroMovement = GetComponent<HeroMovement>();
    }

    private void Update()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (mfallMultiplier - 1) * Time.deltaTime;
        }
        //else if (rb.velocity.y > 0 && 
        //    (!mHeroMovement.PlayerInput.KeyboardMouse.Jump.triggered || !mHeroMovement.PlayerInput.PS4.Jump.triggered || !mHeroMovement.PlayerInput.XBOX.Jump.triggered))
        //{
        //    rb.velocity += Vector2.up * Physics2D.gravity.y * (mLowJumpMultiplier - 1) * Time.deltaTime;

        //}
    }
}
