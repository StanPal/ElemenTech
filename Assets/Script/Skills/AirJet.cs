using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJet : MonoBehaviour
{       
    private float mProjectileSpeed = 1f;
    private float mExitTime = 1f; 
    private Rigidbody2D mRigidbody;
    private Vector3 mScaleSize = new Vector3(0.5f, 0.5f, 0.5f);

    private AirSkills airskills;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        airskills = FindObjectOfType<AirSkills>();
        mProjectileSpeed = airskills.Speed;
        mScaleSize = airskills.Scale;
        mExitTime = airskills.ExitTime;
    }

    private void FixedUpdate()
    {
        mRigidbody.velocity = transform.right * mProjectileSpeed;
        transform.localScale = Vector3.Lerp(transform.localScale, mScaleSize, airskills.ScaleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Debug.Log("Trigger");
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(airskills.Damage);
            }
        }
        
        if (collision.GetComponent<Guard>())
        {
            if (collision.GetComponent<Guard>().tag.Equals(airskills.PlayerSkills.HeroAction.tag))
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
        if (airskills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(airskills.Damage);
                    Destroy(gameObject);
                }
            }
        }
        if (airskills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(airskills.Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
