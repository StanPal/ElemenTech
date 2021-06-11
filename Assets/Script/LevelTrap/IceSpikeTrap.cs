using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeTrap : MonoBehaviour
{
    private IceTrapManager iceTrapManager;
    Vector3 startTransform = new Vector3();
    Rigidbody2D rb;
    [SerializeField] GameObject IceSpike;

    private void Awake()
    {
        iceTrapManager = FindObjectOfType<IceTrapManager>();
        startTransform = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IceSpike.GetComponent<IceSpikeMovement>().IsActive)
        {
            var heroStats = collision.GetComponent<HeroStats>();
            if (heroStats != null)
            {
                Debug.Log("hit player");
                heroStats.TakeDamage(iceTrapManager.Damage,true);
                DestroySpike();
            }
            if (collision.GetComponentInParent<Walls>())
            {
                Debug.Log("hit wall");
                DestroySpike();
            }
        }
    }


    private void DestroySpike()
    {
        //transform.position = startTransform;
        //iceTrapManager.RandomNum.Add(transform);
        // If it hits anything, destroy it.
        iceTrapManager.IceSpikeCounter--;
        iceTrapManager.addNewLocation(IceSpike.transform);
        Destroy(IceSpike.gameObject); 
    }

    public void activeSpike()
    {
        rb.isKinematic = false;
    }
}
