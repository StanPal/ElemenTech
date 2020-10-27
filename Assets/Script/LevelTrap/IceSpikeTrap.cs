using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeTrap : MonoBehaviour
{
    private IceTrapManager iceTrapManager;
    Vector3 startTransform = new Vector3();

    private void Awake()
    {
        iceTrapManager = FindObjectOfType<IceTrapManager>();
        startTransform = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            collision.GetComponent<HeroStats>().TakeDamage(iceTrapManager.Damage);
        }
        if ( collision.CompareTag("Wall") || collision.GetComponent<HeroStats>())
        {
            iceTrapManager.IceSpikeCounter--;
            transform.position = startTransform;
            iceTrapManager.RandomNum.Add(transform);
            Debug.Log("hit");
            Destroy(gameObject.transform.parent);
        }
    }
}
