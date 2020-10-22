using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{        
    private float mProjectileSpeed;
    private Rigidbody2D mRigidbody;
    [SerializeField]
    float exitTime;
    WaterSkills waterSkills;

    private void Awake()
    {
        waterSkills = FindObjectOfType<WaterSkills>();
        mRigidbody = GetComponent<Rigidbody2D>();
        mProjectileSpeed = waterSkills.Speed;
        exitTime = waterSkills.ExitTime;
        if (waterSkills.PlayerSkills.HeroMovement.GetIsLeft)
            mProjectileSpeed *= -1;
    }

    private void FixedUpdate()
    {
        if (exitTime <= 0.0f)
            Destroy(gameObject);

        exitTime -= Time.deltaTime;
        mRigidbody.velocity = transform.right * mProjectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Golem>())
        {
            Debug.Log("Trigger");
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(waterSkills.Damage);
                Destroy(gameObject);
            }
        }

        if (waterSkills.PlayerSkills.HeroAction.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
                collision.GetComponent<HeroStats>().TakeDamage(waterSkills.Damage);
                collision.GetComponent<HeroStats>().SlowMovement(waterSkills.SlowAmount, waterSkills.SlowDuration);
                Destroy(gameObject);
            }
        }
        if (waterSkills.PlayerSkills.HeroAction.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
                collision.GetComponent<HeroStats>().TakeDamage(waterSkills.Damage);
                collision.GetComponent<HeroStats>().SlowMovement(waterSkills.SlowAmount, waterSkills.SlowDuration);
                Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
                Destroy(gameObject);

            }
        }

        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }

    
    }
}
