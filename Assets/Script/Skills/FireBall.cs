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
            if (collision.GetComponent<Guard>().tag.Equals(fireSkills.PlayerSkills.HeroAction.tag))
            {
                Guard guard = collision.GetComponent<Guard>();
                if (guard.Guarding)
                {
                    Destroy(gameObject);
                    Debug.Log("Shield Hit");
                    collision.GetComponent<Guard>().ComboSkillOn = true;
                }
            }
        }
        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }
        if (fireSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                // collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.OnFire;

                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(fireSkills.Damage);
                    Destroy(gameObject);
                }
                    //collision.TryGetComponent<HeroStats>(out HeroStats).TakeDamage(fireSkills.Damage);
                    //collision.GetComponent<HeroStats>().DamageOverTime(fireSkills.Damage, fireSkills.DotDuration);       
                }            
        }
        if (fireSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
               // collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.OnFire;
                collision.GetComponent<HeroStats>().TakeDamage(fireSkills.Damage);
                collision.GetComponent<HeroStats>().DamageOverTime(fireSkills.Damage, fireSkills.DotDuration);
                Destroy(gameObject);
            }
        }
    }
}
