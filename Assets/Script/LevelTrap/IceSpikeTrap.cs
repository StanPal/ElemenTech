using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeTrap : MonoBehaviour
{
    private IceTrapManager IceTrapManager;

    private void Awake()
    {
        IceTrapManager = FindObjectOfType<IceTrapManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            collision.GetComponent<HeroStats>().TakeDamage(IceTrapManager.Damage);
        }
        Destroy(gameObject);
    }
}
