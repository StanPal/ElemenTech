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
        if (airskills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                collision.GetComponent<HeroStats>().TakeDamage(airskills.Damage);
                Destroy(gameObject);
            }
        }
        if (airskills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                collision.GetComponent<HeroStats>().TakeDamage(airskills.Damage);
                Destroy(gameObject);
            }
        }
    }
}
