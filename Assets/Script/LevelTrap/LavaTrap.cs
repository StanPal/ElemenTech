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

    float delayTime = 1.0f;
    float currentDelayTime = 0.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            onLava = true;
        }
        if (onLava)
        {
            if (currentDelayTime < Time.time)
            {
                currentDelayTime = Time.time + delayTime;
                collision.GetComponent<HeroStats>().TakeDamage(damage);
            }
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
