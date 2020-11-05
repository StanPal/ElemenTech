using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public List<GameObject> DebuffEffects;
    private PlayerManager playerManager;
    private Stack<GameObject> statusEffect = new Stack<GameObject>();
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.AirHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        playerManager.AirHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        playerManager.WaterHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        playerManager.WaterHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        playerManager.EarthHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        playerManager.EarthHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        playerManager.FireHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        playerManager.FireHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;
    }

    private void DebuffEffectOn(GameObject hero)
    {
        switch (hero.GetComponent<HeroStats>().DeBuff)
        {
            case StatusEffects.NegativeEffects.OnFire:
                Burning(hero);
                break;
            case StatusEffects.NegativeEffects.Slowed:
                Slowed(hero);
                break;
            case StatusEffects.NegativeEffects.Stunned:
                break;            
            default:
                break;
        }
    }

    private void Burning(GameObject hero)
    {
        ParticleSystem ps = DebuffEffects[0].GetComponent<ParticleSystem>();
        
        GameObject BurningEffect = Instantiate(ps.gameObject, hero.transform.position, Quaternion.identity);
        BurningEffect.transform.parent = hero.transform;
        BurningEffect.transform.localScale = new Vector3(1f, 1f, 1f);
        BurningEffect.GetComponent<ParticleSystem>().Play();

        statusEffect.Push(BurningEffect);
    }

    private void Slowed(GameObject hero)
    {
        ParticleSystem ps = DebuffEffects[1].GetComponent<ParticleSystem>();

        GameObject SlowEffect = Instantiate(ps.gameObject, hero.transform.position, Quaternion.identity);
        SlowEffect.transform.parent = hero.transform;
        SlowEffect.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        SlowEffect.GetComponent<ParticleSystem>().Play();

        statusEffect.Push(SlowEffect);
    }

    private void DebuffEffectOff()
    {
        if (statusEffect.Count > 0)
        {
            Destroy(statusEffect.Pop());
        }
    }

}
