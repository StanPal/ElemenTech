using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : MonoBehaviour
{
    EarthSkills EarthSkills;
    private void Awake()
    {
        EarthSkills = FindObjectOfType<EarthSkills>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Golem golem = collision.GetComponent<Golem>();
            golem.TakeDamage(EarthSkills.Damage);
        }
        if (EarthSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                collision.GetComponent<HeroStats>().TakeDamage(EarthSkills.Damage);
            }
        }
        if (EarthSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                collision.GetComponent<HeroStats>().TakeDamage(EarthSkills.Damage);
            }
        }
    }
}
