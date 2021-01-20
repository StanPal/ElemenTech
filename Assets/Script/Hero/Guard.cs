using System.Collections;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public GameObject Shield;
    private GameObject _Shield;
    private HeroActions _HeroAction;
    private HeroMovement _HeroMovement;
    private bool _ShieldCreated = false;
    private bool _SkillCombine = false;
    public bool IsShieldDisabled = false;
    private bool _ShieldBreak = false;

    private ParticleSystemManager _ParticleSystemManager;

    [SerializeField] private bool _IsGuarding = false;
    [SerializeField] private float _GuardTime = 0.5f;
    [SerializeField] private float _ShieldSize = 2.1f;
    [SerializeField] private float _ShieldRecoveryTick = 3f;
    [SerializeField] private float _SpeedDecrease = 1f;
    [SerializeField] private float _ShieldMaxEnergy = 100f;
    [SerializeField] private float _ShieldRecoverAmount = 1f;
    [SerializeField] private float _ShieldEnergyTick = 0.2f;
    [SerializeField] private float _ShieldEnergy = 100f;

    // Setters & Getters
    public bool Guarding { get { return _IsGuarding; } }
    public float ShieldMaxEnergy { get { return _ShieldMaxEnergy; } }
    public float ShieldEnergy { get { return _ShieldEnergy; } set { _ShieldEnergy = value; } }
    public float ShieldRecoveryAmount { get { return _ShieldRecoverAmount; } }
    public float ShieldRecoveryTick { get { return _ShieldRecoveryTick; } }
    public bool ComboSkillOn { get { return _SkillCombine; } set { _SkillCombine = value; } }


    public GameObject ComboSkill;

    private void Start()
    {
        _ParticleSystemManager = FindObjectOfType<ParticleSystemManager>();
        _ShieldEnergy = _ShieldMaxEnergy;
        _HeroAction = GetComponentInParent<HeroActions>();
        _HeroAction.onGuardPerformed += GuardMove;
        _HeroAction.onGuardExit += DestroyGuard;
    }

    private void GuardMove()
    {
        _IsGuarding = true;
        SummonGuard();
    }

    private void Update()
    {
        if (_IsGuarding)
        {
            if (_Shield == null)
            {
                Debug.Log("Cannot Create Shield, No Element Attached");
            }
            else
            {
                _Shield.transform.position = Vector3.MoveTowards(_Shield.transform.position, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), 1.0f);
                if (_ShieldEnergy > 0)
                {
                    _ShieldEnergy -= Time.deltaTime / _ShieldEnergyTick;
                }
                else
                {
                    IsShieldDisabled = true;
                    _ShieldBreak = true;
                    DestroyGuard();
                }
                Color color = _Shield.GetComponent<SpriteRenderer>().color;
                color.a = (_ShieldEnergy * 0.01f);
                _Shield.GetComponent<SpriteRenderer>().color = color;
            }
        }
        if(_IsGuarding && ComboSkillOn)
        {
            if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Water)
            {
                GameObject ComboSkillClone = Instantiate(ComboSkill, transform.position, Quaternion.identity);
                ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            }
            if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Earth)
            {
                GameObject ComboSkillClone = Instantiate(ComboSkill, transform);
                ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            }
            if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Fire)
            {
                GameObject ComboSkillClone = Instantiate(ComboSkill, transform);
                ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            }
            if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Air)
            {
                GameObject ComboSkillClone = Instantiate(ComboSkill, transform);
                ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            }
            //Debug.Log(FindObjectOfType<Guard>().gameObject.transform.position);
            ComboSkillOn = false;
        }
    }

    private void DestroyGuard()
    {
        Destroy(_Shield);
        if (_ShieldBreak)
        {
            GameObject shieldBreak = Instantiate(_ParticleSystemManager.OtherEffects[0]);
            shieldBreak.transform.position = this.transform.position;
            _ShieldBreak = false;
            shieldBreak.GetComponent<ParticleSystem>().Play();
        }
        _IsGuarding = false;
        _ShieldCreated = false;
    }

    private void OnDestroy()
    {
        _HeroAction.onGuardPerformed -= GuardMove;
        _HeroAction.onGuardExit -= DestroyGuard;
    }

    private void SummonGuard()
    {
        if (!_ShieldCreated && !IsShieldDisabled)
        {
            _Shield = Instantiate(Shield, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), Quaternion.identity);
            _Shield.transform.localScale = new Vector3(_ShieldSize,_ShieldSize,_ShieldSize);
            Color color = _Shield.GetComponent<SpriteRenderer>().color;
            color.a = (_ShieldEnergy * 0.01f);
            _Shield.GetComponent<SpriteRenderer>().color = color;
            _ShieldCreated = true;
        }
    }    
}
