using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _DebuffEffects = new List<GameObject>();
    [SerializeField] private List<GameObject> _OtherEffects = new List<GameObject>();
    [SerializeField] private List<GameObject> _AuraEffects = new List<GameObject>();
    private PlayerManager _PlayerManager;
    private Dictionary<GameObject, GameObject> _NegativeDict = new Dictionary<GameObject, GameObject>();
    public List<GameObject> GetDebuffEffects { get { return _DebuffEffects; } }

    [SerializeField] private float _AuraDuration = 1f;
    [SerializeField] private float _AuraDamage = 0.1f;
    private bool _IsAuraExist = false;
    private GameObject _AuraType;
    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _PlayerManager = FindObjectOfType<PlayerManager>();
        for (int i = 0; i < _PlayerManager.mPlayersList.Count; i++)
        {
            _PlayerManager.mPlayersList[i].GetComponent<HeroStats>().onDebuffActivated += DebuffEffectOn;
            _PlayerManager.mPlayersList[i].GetComponent<HeroStats>().onDebuffDeActivated += DebuffEffectOff;

        }
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


    private void DebuffEffectOff(GameObject hero)
    {
        foreach (var statuseffect in _NegativeDict)
        {
            if (statuseffect.Key.GetComponent<HeroStats>().DeBuff == StatusEffects.NegativeEffects.None)
            {
                statuseffect.Value.GetComponent<ParticleSystem>().Stop();
                _NegativeDict.Remove(statuseffect.Key);
                break;
            }
        }
    }

    private void Burning(GameObject hero)
    {
        ParticleSystem ps = _DebuffEffects[0].GetComponent<ParticleSystem>();
        
        GameObject BurningEffect = Instantiate(ps.gameObject, new Vector3(hero.transform.position.x,hero.transform.position.y - 1) , Quaternion.identity);
        BurningEffect.transform.parent = hero.transform;
        BurningEffect.transform.localScale = new Vector3(1f, 1.5f, 1f);
        BurningEffect.GetComponent<ParticleSystem>().Play();
        if (_NegativeDict.ContainsKey(hero))
        {
            _NegativeDict.Remove(hero);
            _NegativeDict.Add(hero, BurningEffect);
        }
        else
        {
            _NegativeDict.Add(hero, BurningEffect);
        }
    }

    private void Slowed(GameObject hero)
    {
        ParticleSystem ps = _DebuffEffects[1].GetComponent<ParticleSystem>();

        GameObject SlowEffect = Instantiate(ps.gameObject, hero.transform.position, Quaternion.identity);
        SlowEffect.transform.parent = hero.transform;
        SlowEffect.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        SlowEffect.GetComponent<ParticleSystem>().Play();
    }

    public void FireAura(GameObject hero)
    {
        Debug.Log("reached");
        if (!_IsAuraExist)
        {
            _AuraType = Instantiate(_AuraEffects[0].gameObject, hero.transform.position, Quaternion.identity);
            _AuraType.tag = hero.tag;
            _AuraType.GetComponent<FireAura>().SetDamage = _AuraDamage;
            _AuraType.transform.parent = hero.transform;
            _AuraType.transform.localScale = new Vector3(3f, 3f, 3f);
            _AuraType.GetComponent<ParticleSystem>().Play();
            _IsAuraExist = true;
        }
        else
        {
            _AuraType.GetComponent<ParticleSystem>().Play();
        }
        StartCoroutine(TurnOffAura(_AuraType));
    }
    
    private IEnumerator TurnOffAura(GameObject Aura)
    {
        yield return new WaitForSeconds(_AuraDuration);
        _IsAuraExist = false;
        Destroy(Aura);
    }

}
