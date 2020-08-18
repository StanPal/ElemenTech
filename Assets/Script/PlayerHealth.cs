using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100.0f;
    float currentHealth;
    public float guardDamage;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(maxHealth);
    }



    public void SetHealth(float health)
    {
        currentHealth = health;
        HealthBar.Instance.slider.value = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        SetHealth(currentHealth + guardDamage - damage);
    }

    
    void Update()
    {
        //slider.value = health / maxHealth;

        //if (health <= 0.0f)
        //{
        //    //end the game/.
        //}
    }
}
