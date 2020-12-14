using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MysteryBox : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D target)
    {
        var heroStats = target.GetComponent<HeroStats>();
        int n = Random.Range(0, 8);
        if (target.gameObject.GetComponent<HeroStats>() != null)
        {
            Destroy(gameObject);
            Elements.ElementalAttribute prestate = target.gameObject.GetComponent<HeroStats>().mElementalType;

            if (n == 0) // n = 0,will turn to the earth
            {
                target.gameObject.GetComponent<HeroStats>().mElementalType = Elements.ElementalAttribute.Earth;
            }
            else if(n == 1)//air
                {
                target.gameObject.GetComponent<HeroStats>().mElementalType = Elements.ElementalAttribute.Air;
                }
            else if(n == 2)//fire
                {
                target.gameObject.GetComponent<HeroStats>().mElementalType = Elements.ElementalAttribute.Fire;
                }
            else if(n == 3)//water
                {
                target.gameObject.GetComponent<HeroStats>().mElementalType = Elements.ElementalAttribute.Water;
                }
            else if(n == 4)//healthitem
                {
                if (heroStats.CurrentHealth + gameObject.GetComponent<HealthItem>().itemManager.HealthReply > heroStats.MaxHealth)
                {
                heroStats.CurrentHealth = heroStats.MaxHealth;
                }
                }
            else if(n == 5)//golem air
                {
                target.gameObject.GetComponent<Golem>().mGolemType = GolemData.elementType.Air;
                }
            else if (n == 6)//golem earth
            {
                target.gameObject.GetComponent<Golem>().mGolemType = GolemData.elementType.Earth;
            }
            else if (n == 7)//golem fire
            {
                target.gameObject.GetComponent<Golem>().mGolemType = GolemData.elementType.Fire;
            }
            else if (n == 8)//golem water
            {
                target.gameObject.GetComponent<Golem>().mGolemType = GolemData.elementType.Water;
            }
        }
    }
}
