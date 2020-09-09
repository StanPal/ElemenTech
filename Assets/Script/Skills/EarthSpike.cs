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
        if(collision.GetComponent<Golem>())
        {
            Golem golem = collision.GetComponent<Golem>();
            golem.TakeDamage(EarthSkills.Damage);
        }
        if (collision.tag.Equals("Team2"))
        {
            collision.GetComponent<Hero>().TakeDamage(EarthSkills.Damage);
        }
    }
}
