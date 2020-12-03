﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPickUp : MonoBehaviour
{
 
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.GetComponent<HeroStats>() != null)
        {
            Destroy (gameObject);
            Elements.ElementalAttribute prestate = target.gameObject.GetComponent<HeroStats>().mElementalType;
            target.gameObject.GetComponent<HeroStats>().mElementalType = Elements.ElementalAttribute.Earth;

            if (target.gameObject.GetComponent<HeroStats>().CurrentHealth < target.gameObject.GetComponent<HeroStats>().MaxHealth)
            {
                target.gameObject.GetComponent<HeroStats>().mElementalType = prestate;
            }
        }
    }

  

}
