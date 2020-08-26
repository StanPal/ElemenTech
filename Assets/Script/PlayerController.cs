using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D rb;
    public float JumpSpeed = 400.0f;
    public float DashSpeed = 50.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
     
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
       
        rb.AddForce(movement * speed);

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                movement = new Vector2(moveHorizontal + DashSpeed, 0.0f);
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, JumpSpeed));
        }
        

    }
}
