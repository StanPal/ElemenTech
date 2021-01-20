using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public List<GameObject> DebuffEffects;
    public List<GameObject> OtherEffects = new List<GameObject>();
    private PlayerManager _PlayerManager;
    private Stack<GameObject> _StatusEffects = new Stack<GameObject>();
    private void Awake()
    {
        _PlayerManager = FindObjectOfType<PlayerManager>();
        _PlayerManager.AirHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _PlayerManager.AirHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        _PlayerManager.WaterHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _PlayerManager.WaterHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        _PlayerManager.EarthHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _PlayerManager.EarthHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        _PlayerManager.FireHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _PlayerManager.FireHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;
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

        _StatusEffects.Push(BurningEffect);
    }

    private void Slowed(GameObject hero)
    {
        ParticleSystem ps = DebuffEffects[1].GetComponent<ParticleSystem>();

        GameObject SlowEffect = Instantiate(ps.gameObject, hero.transform.position, Quaternion.identity);
        SlowEffect.transform.parent = hero.transform;
        SlowEffect.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        SlowEffect.GetComponent<ParticleSystem>().Play();

        _StatusEffects.Push(SlowEffect);
    }

    private void DebuffEffectOff()
    {
        Destroy(_StatusEffects.Pop());
    }

}
