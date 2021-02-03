using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingJump : MonoBehaviour
{
    private float jumpPressure = 0f;

    private float MinjumpPressure = 3f;

    private float MaxjumpPressure = 10f;

    private Rigidbody2D rb;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }
    //private void Update()
    //{
    //    if(GetComponent<HeroMovement>().IsGrounded())
    //    {
    //        if(GetComponent<HeroMovement>().PlayerInput())
    //        {
    //            if(jumpPressure < MaxjumpPressure)
    //            {
    //                jumpPressure += Time.deltaTime*10f;
    //            }
    //            else
    //            {
    //                jumpPressure = MaxjumpPressure;
    //            }
    //            print("hold:" + jumpPressure);
    //        }
    //        else
    //        {
    //            if(jumpPressure > 0)
    //            {
    //                jumpPressure += MinjumpPressure;
    //                rb.velocity = Vector2.up * jumpPressure;
    //                jumpPressure = 0f;
    //            }
    //        }

    //    }

}
