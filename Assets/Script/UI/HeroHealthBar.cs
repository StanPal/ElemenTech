using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealthBar : MonoBehaviour
{
    private Hero mHero;
    private Slider slider;

    void Awake()
    {
        mHero = GetComponentInParent<Hero>();
        slider = GetComponent<Slider>();
        slider.transform.position = 
            new Vector3(this.GetComponentInParent<Hero>().gameObject.transform.position.x,
                        this.GetComponentInParent<Hero>().gameObject.transform.position.y + 1.0f);
    }



    void Update()
    {
        float fillValue = mHero.CurrentHealth / mHero.MaxHealth;
        slider.value = fillValue;
    }
}
