using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    float mMaxHealth = 100.0f;
    [SerializeField]
    float mCurrentHealth;

    public float MaxHealth { get { return mMaxHealth; } }
    public float CurrentHealth { get { return mCurrentHealth; } }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mCurrentHealth = mMaxHealth;
    }

    private void Update()
    {
        KillEnemy();
    }

    private void KillEnemy()
    {
        if (mCurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        mCurrentHealth -= damage;
        Debug.Log("damage TAKEN");
    }

    
}
