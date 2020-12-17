using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField]
    private float damage = 2;
    [SerializeField]
    private float projectileSpeed;
    private Rigidbody2D mRigidbody;
    [SerializeField]
    private float exitTime = 2.0f;
    private WaterSkills waterSkills;
    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        waterSkills = FindObjectOfType<WaterSkills>();
        projectileSpeed = waterSkills.Speed;
        if (waterSkills.PlayerSkills.HeroMovement.GetIsLeft)
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
        mRigidbody.velocity = transform.right * projectileSpeed;
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

        if (collision.GetComponent<Guard>())
        {
            if (collision.GetComponent<Guard>().tag.Equals(waterSkills.PlayerSkills.HeroAction.tag))
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

        if (waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(damage);
                    Destroy(gameObject);

                }
                //collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
                //collision.GetComponent<HeroStats>().SlowMovement(waterSkills.SlowAmount, waterSkills.SlowDuration);
            }
        }
        if (waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(damage);
                    Destroy(gameObject);

                }
                //collision.GetComponent<HeroStats>().TakeDamage(damage);
                //collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
                //collision.GetComponent<HeroStats>().SlowMovement(waterSkills.SlowAmount, waterSkills.SlowDuration);
            }
        }

        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }
    }
}
