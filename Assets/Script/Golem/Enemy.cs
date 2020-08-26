using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    float maxHealth;
    float health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    private void Update()
    {
        KillEnemy();
    }

    private void KillEnemy()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("damage TAKEN");
    }

    
}
