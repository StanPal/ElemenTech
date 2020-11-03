using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField]
    float damage = 2;
    [SerializeField]
    float projectileSpeed = 5;
    private Rigidbody2D rigidbody;
    [SerializeField]
    float exitTime = 2.0f;
    private HeroMovement hero;
    WaterSkills waterSkills;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        waterSkills = FindObjectOfType<WaterSkills>();
        hero = FindObjectOfType<HeroMovement>();
        if (hero.GetIsLeft)
        {
            projectileSpeed = -projectileSpeed;
        }

    }

    private void FixedUpdate()
    {
        if (exitTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        exitTime -= Time.deltaTime;
        rigidbody.velocity = transform.right * projectileSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Debug.Log("Trigger");
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                collision.GetComponent<HeroStats>().TakeDamage(damage);
                collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;

            }
        }
        if (waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                collision.GetComponent<HeroStats>().TakeDamage(damage);
                collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
                collision.GetComponent<HeroMovement>().DeBuff = StatusEffects.NegativeEffects.Slowed;


            }
        }

        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }
    }
}
