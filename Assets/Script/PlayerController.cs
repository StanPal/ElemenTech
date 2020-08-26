using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    public float speed;
    public float JumpSpeed;
    public float DashSpeed;
    private float DashTime;
    public float startDashTime;
    private int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DashTime = startDashTime;

    }

    void Update()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = 1;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = 2;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction = 3;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                direction = 4;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                direction = 5;
            }

        }
        else
        {
            if (DashTime <= 0)
            {
                direction = 0;
                DashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                DashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rb.velocity = Vector2.left * speed;
                }
                else if (direction == 2)
                {
                    rb.velocity = Vector2.right * speed;
                }
                else if (direction == 3)
                {
                    rb.velocity = Vector2.up * JumpSpeed;
                }
                else if (direction == 4)
                {
                    rb.velocity = Vector2.left * DashSpeed;
                }
                else if (direction == 5)
                {
                    rb.velocity = Vector2.right * DashSpeed;
                }

            }

        }


        //float moveHorizontal = Input.GetAxis("Horizontal");
        ////float moveVertical = Input.GetAxis("Vertical");

        //Vector2 movement = new Vector2(moveHorizontal, 0.0f);

        //rb.AddForce(movement * speed);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.AddForce(new Vector2(0, JumpSpeed));
        //}

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    rb.AddForce(new Vector2(DashSpeed, 0));
        //}




    }
}
