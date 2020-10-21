using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public List<GameObject> DebuffEffects;
    public GameObject playerManager;
    private GameObject statusEffect;
    private void Awake()
    {
        playerManager.GetComponent<PlayerManager>().AirHero.GetComponent<HeroStats>().onDebuffActivated += Burning;
        playerManager.GetComponent<PlayerManager>().AirHero.GetComponent<HeroStats>().onDebuffDeActivated += BurningOff;


    }

    private void Burning(GameObject hero)
    {
        ParticleSystem ps = DebuffEffects[0].GetComponent<ParticleSystem>();
        statusEffect = Instantiate(DebuffEffects[0], hero.transform.position, Quaternion.identity);
        if (hero.GetComponent<HeroStats>().DeBuff == StatusEffects.NegativeEffects.OnFire)
        {
            statusEffect.transform.parent = hero.transform;
            statusEffect.GetComponent<ParticleSystem>().Play();
        }

    }    

    private void BurningOff(GameObject hero)
    {
        Destroy(statusEffect);        
    }

}
