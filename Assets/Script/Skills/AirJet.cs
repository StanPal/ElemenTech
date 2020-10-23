using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJet : MonoBehaviour
{       
    float mProjectileSpeed = 1f;
    float mExitTime = 1f; 
    private Rigidbody2D mRigidbody;
    Vector3 mScaleSize = new Vector3(0.5f, 0.5f, 0.5f);

    AirSkills airskills;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        airskills = FindObjectOfType<AirSkills>();
        mProjectileSpeed = airskills.Speed;
        mScaleSize = airskills.Scale;
        mExitTime = airskills.ExitTime;
        if (airskills.PlayerSkills.HeroMovement.GetIsLeft)
        {
            mProjectileSpeed *= -1;
        }
    }

    private void FixedUpdate()
    {
        if (mExitTime <= 0.0f)
        {
            Destroy(gameObject);
        }

        mExitTime -= Time.deltaTime;
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
        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }
        //if (airskills.PlayerSkills.Hero.tag.Equals("Team1"))
        //{
        //    if (collision.tag.Equals("Team2"))
        //    {
        //        collision.GetComponent<Hero>().TakeDamage(mDamage);
        //        Destroy(gameObject);
        //    }
        //}
        //if (airskills.PlayerSkills.Hero.tag.Equals("Team2"))
        //{
        //    if (collision.tag.Equals("Team1"))
        //    {
        //        collision.GetComponent<Hero>().TakeDamage(mDamage);
        //        Destroy(gameObject);
        //    }
        //}
    }
}
