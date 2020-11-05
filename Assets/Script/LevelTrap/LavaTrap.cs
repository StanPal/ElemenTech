using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrap: MonoBehaviour
{
    [SerializeField]
    float damageTime = 1.0f;
    [SerializeField]
    float damage = 5.0f;
    float currentDamageTime;
    bool onLava = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            onLava = true;
        }
        if (onLava)
        {
            collision.GetComponent<HeroStats>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            onLava = false;
        }
    }



}
