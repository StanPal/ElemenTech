using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _debuffEffects = new List<GameObject>();
    [SerializeField] private List<GameObject> _otherEffects = new List<GameObject>();
    private PlayerManager _playerManager;
    private Stack<GameObject> _statusEffects = new Stack<GameObject>();

    public List<GameObject> DebuffEffects { get => _debuffEffects; }
    public List<GameObject> OtherEffects { get => _otherEffects; }

    private void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager.AirHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _playerManager.AirHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        _playerManager.WaterHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _playerManager.WaterHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        _playerManager.EarthHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _playerManager.EarthHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        _playerManager.FireHero.GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
        _playerManager.FireHero.GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;
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
        ParticleSystem ps = _debuffEffects[0].GetComponent<ParticleSystem>();
        
        GameObject BurningEffect = Instantiate(ps.gameObject, hero.transform.position, Quaternion.identity);
        BurningEffect.transform.parent = hero.transform;
        BurningEffect.transform.localScale = new Vector3(1f, 1f, 1f);
        BurningEffect.GetComponent<ParticleSystem>().Play();

        _statusEffects.Push(BurningEffect);
    }

    private void Slowed(GameObject hero)
    {
        ParticleSystem ps = _debuffEffects[1].GetComponent<ParticleSystem>();

        GameObject SlowEffect = Instantiate(ps.gameObject, hero.transform.position, Quaternion.identity);
        SlowEffect.transform.parent = hero.transform;
        SlowEffect.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        SlowEffect.GetComponent<ParticleSystem>().Play();

        _statusEffects.Push(SlowEffect);
    }

    private void DebuffEffectOff()
    {
        Destroy(_statusEffects.Pop());
    }

}
