using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeMovement : MonoBehaviour
{
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            rb.isKinematic = false;
            Debug.Log("hit");
            Destroy(gameObject, 5.0f);
        }
    }
}
