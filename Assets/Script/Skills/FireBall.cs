using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private CanonBall canonball;
    private Rigidbody2D rb;
    private FireSkills fireSkills;

    private void Awake()
    {
        fireSkills = FindObjectOfType<FireSkills>();
    }
    // Start is called before the first frame update
    void Start()
    {
     
        canonball = FindObjectOfType<CanonBall>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 20;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Golem golem = collision.GetComponent<Golem>();
            golem.TakeDamage(fireSkills.Damage);
            Destroy(gameObject);
        }
        else if(collision.GetComponent<Shield>())
        {      
            Debug.Log("Shield Hit");
            collision.GetComponent<Shield>().ComboSkillOn = true;

            Destroy(gameObject);
        }
        //else if (collision.GetComponentInChildren<Wall>())
        //{
        //    Destroy(gameObject);
        //}
    }
}
