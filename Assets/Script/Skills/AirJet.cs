using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJet : MonoBehaviour
{
    [SerializeField]
    float mDamage = 2;
    [SerializeField]
    float projectileSpeed = 2f;
    [SerializeField]
    float exitTime = 20f;
    [SerializeField]
    Vector3 ScaleSize = new Vector3(0.5f, 0.5f, 0.5f);
    private Rigidbody2D rigidbody;
    AirSkills airskills;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        airskills = FindObjectOfType<AirSkills>();
        if (airskills.PlayerSkills.Hero.GetIsLeft)
        {
             projectileSpeed *= -1; 
        }

    }

    private void FixedUpdate()
    {
        if (exitTime <= 0.0f )
        {
            Destroy(gameObject);
        }

        exitTime -= Time.deltaTime;
        rigidbody.velocity = transform.right * projectileSpeed;
        gameObject.transform.localScale += ScaleSize;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Debug.Log("Trigger");
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(mDamage);
            }
        }
        if (collision.GetComponentInChildren<Walls>())
        {
            Destroy(gameObject);
        }
        if (airskills.PlayerSkills.Hero.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                collision.GetComponent<Hero>().TakeDamage(mDamage);
                Destroy(gameObject);
            }
        }
        if (airskills.PlayerSkills.Hero.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                collision.GetComponent<Hero>().TakeDamage(mDamage);
                Destroy(gameObject);
            }
        }
    }
}
