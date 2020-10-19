using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    //private CanonBall canonball;
    private FireSkills fireSkills;
    private Rigidbody2D mRigidbody;
    private float mProjectileSpeed;
    private void Awake()
    {
        fireSkills = FindObjectOfType<FireSkills>();
        mRigidbody = GetComponent<Rigidbody2D>();
        mProjectileSpeed = fireSkills.Speed;
        if (fireSkills.PlayerSkills.HeroMovement.GetIsLeft)
        {
            mProjectileSpeed *= -1;
        }
    }

    private void FixedUpdate()
    {
        mRigidbody.velocity = transform.right * mProjectileSpeed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Golem golem = collision.GetComponent<Golem>();
            golem.TakeDamage(fireSkills.Damage);
            Destroy(gameObject);
        }
        if (collision.GetComponent<Guard>())
        {
            Guard guard = collision.GetComponent<Guard>();
            if (guard.Guarding)
            {
                Debug.Log("Shield Hit");
                collision.GetComponent<Guard>().ComboSkillOn = true;
                Destroy(gameObject);
            }
        }
        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }
        //if (fireSkills.PlayerSkills.Hero.tag.Equals("Team1"))
        //{
        //    if (collision.tag.Equals("Team2"))
        //    {
        //        collision.GetComponent<Hero>().TakeDamage(fireSkills.Damage);
        //        Destroy(gameObject);
        //    }
        //}
        //if (fireSkills.PlayerSkills.Hero.tag.Equals("Team2"))
        //{
        //    if (collision.tag.Equals("Team1"))
        //    {
        //        collision.GetComponent<Hero>().TakeDamage(fireSkills.Damage);
        //        Destroy(gameObject);
        //    }
        //}
    }
}
