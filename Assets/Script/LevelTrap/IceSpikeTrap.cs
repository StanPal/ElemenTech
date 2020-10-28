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
        var heroStats = collision.GetComponent<HeroStats>();
        if (heroStats != null)
        {
            Debug.Log("hit player");
            heroStats.TakeDamage(iceTrapManager.Damage);
            DestroySpike();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            DestroySpike();
        }
    }


    private void DestroySpike()
    {
        iceTrapManager.IceSpikeCounter--;
        //transform.position = startTransform;
        //iceTrapManager.RandomNum.Add(transform);
        // If it hits anything, destroy it.
        iceTrapManager.SpawnSpike();
        Destroy(transform.parent.gameObject);
    }
}
