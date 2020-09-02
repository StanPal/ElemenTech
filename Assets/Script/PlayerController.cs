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
    //private float DashTime;
    //public float startDashTime;
    //private int direction;
    public float DashDelay = 0.5f;
    private float DashCoolDown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("CheckDash");
        //DashTime = startDashTime;

    }

    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, JumpSpeed));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector2(-speed, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector2(speed, 0));
        }

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    rb.AddForce(new Vector2(DashSpeed, 0));
        //}

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    rb.AddForce(new Vector2(-DashSpeed, 0));
        //}
    }

    IEnumerator CheckDash()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DashCoolDown = Time.deltaTime;
                while ((Time.deltaTime - DashCoolDown) <= DashDelay)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        rb.AddForce(new Vector2(-DashSpeed, 0));
                    }
                    yield return null;
                }
            }
           
            yield return null;
        }
       
    }
        




    
}
     //if (direction == 0)
     //   {
     //       if (Input.GetKey(KeyCode.LeftArrow))
     //       {
     //           direction = 1;
     //       }
     //       if (Input.GetKey(KeyCode.RightArrow))
     //       {
     //           direction = 2;
     //       }
     //       if (Input.GetKeyDown(KeyCode.Space))
     //       {
     //           direction = 3;
     //       }
     //       if (Input.GetKeyDown(KeyCode.Z))
     //       {
     //           direction = 4;
     //       }
     //       if (Input.GetKeyDown(KeyCode.C))
     //       {
     //           direction = 5;
     //       }

     //   }
     //   else
     //   {
     //       if (DashTime <= 0)
     //       {
     //           direction = 0;
     //           DashTime = startDashTime;
     //           rb.velocity = Vector2.zero;
     //       }
     //       else
     //       {
     //           DashTime -= Time.deltaTime;

     //           if (direction == 1)
     //           {
     //               rb.velocity = Vector2.left* speed * Time.deltaTime;
     //           }
     //           else if (direction == 2)
     //           {
     //               rb.velocity = Vector2.right* speed * Time.deltaTime;
     //           }
     //           else if (direction == 3)
     //           {
     //               rb.velocity = Vector2.up* JumpSpeed * Time.deltaTime;
     //           }
     //           else if (direction == 4)
     //           {
     //               rb.AddForce(new Vector2(-DashSpeed, 0));
     //           }
     //           else if (direction == 5)
     //           {
     //               rb.AddForce(new Vector2(DashSpeed, 0));
     //           }

     //       }