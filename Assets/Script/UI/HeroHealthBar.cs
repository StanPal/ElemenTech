using UnityEngine;
using UnityEngine.UI;

public class HeroHealthBar : MonoBehaviour
{
    private HeroStats _Hero;
    private Slider _Slider;

    void Awake()
    {
        _Hero = GetComponentInParent<HeroStats>();
        _Slider = GetComponent<Slider>();
        _Slider.transform.position = 
            new Vector3(this.GetComponentInParent<HeroStats>().gameObject.transform.position.x,
                        this.GetComponentInParent<HeroStats>().gameObject.transform.position.y + 1.0f);
    }

    void Update()
    {
        float fillValue = _Hero.CurrentHealth / _Hero.MaxHealth;
        _Slider.value = fillValue;
    }
}
