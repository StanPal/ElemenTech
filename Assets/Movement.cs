using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float moveInput;
    private bool jumpInput;
    private float speed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveInput * speed, 0);
    }

    private void FixedUpdate()
    {
        if(jumpInput)
        {
            JumpForce();
        }
    }

    private void JumpForce()
    {
        rb.velocity = Vector2.up * 10f;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpInput = context.ReadValue<bool>();
        
    }
}
