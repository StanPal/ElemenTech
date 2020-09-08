using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJet : MonoBehaviour
{
    [SerializeField]
    float damage = 2;
    [SerializeField]
    float projectileSpeed;
    [SerializeField]
    float exitTime = 20f;
    [SerializeField]
    Vector3 ScaleSize = new Vector3(0.5f, 0.5f, 0.5f);
    private Rigidbody2D rigidbody;
    private Hero hero;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        hero = FindObjectOfType<Hero>();
        if (hero.GetIsLeft)
        {
            projectileSpeed = - projectileSpeed;
        }

    }

    private void Update()
    {
        if (exitTime <= 0.0f )
        {
            Destroy(gameObject);
        }

        rigidbody.velocity = transform.right * projectileSpeed;
        gameObject.transform.localScale += ScaleSize;
        exitTime -= Time.deltaTime;
        
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
            }
        }
    }
}
