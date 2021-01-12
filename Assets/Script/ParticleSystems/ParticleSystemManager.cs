﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public List<GameObject> DebuffEffects;
    private PlayerManager PlayerManager;
    private Stack<GameObject> statusEffect = new Stack<GameObject>();
    private void Awake()
    {
        PlayerManager = FindObjectOfType<PlayerManager>();
        PlayerManager.AirHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        PlayerManager.AirHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        PlayerManager.WaterHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        PlayerManager.WaterHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        PlayerManager.EarthHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        PlayerManager.EarthHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        PlayerManager.FireHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        PlayerManager.FireHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;
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
        Destroy(statusEffect.Pop());
    }

}
