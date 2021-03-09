using System.Collections;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public event System.Action OnShieldRecover;

    [SerializeField] private GameObject _shield;
    [SerializeField] private ParticleSystem _shieldBreakEffect;
    private HeroActions _heroAction;
    private HeroMovement _heroMovement;
    private bool _shieldCreated = false;
    private bool _skillCombine = false;
    private bool _shieldBreak = false;
    private bool _isShieldDisabled = false;

    private ParticleSystemManager _particleSystemManager;

    [SerializeField] private bool _isGuarding = false;
    [SerializeField] private float _shieldSize = 2.1f;
    [SerializeField] private float _shieldRecoveryTick = 3f;
    [SerializeField] private float _speedDecrease = 1f;
    [SerializeField] private float _shieldMaxEnergy = 100f;
    [SerializeField] private float _shieldExpendAmount = 2f;
    [SerializeField] private float _shieldRecoverAmount = 1f;
    [SerializeField] private float _shieldEnergyTick = 0.2f;
    [SerializeField] private float _shieldEnergy = 100f;

    // Setters & Getters
    public bool Guarding { get { return _isGuarding; } }
    public float ShieldMaxEnergy { get { return _shieldMaxEnergy; } }
    public float ShieldEnergy { get { return _shieldEnergy; } set { _shieldEnergy = value; } }
    public float ShieldRecoveryAmount { get { return _shieldRecoverAmount; } }
    public float ShieldRecoveryTick { get { return _shieldRecoveryTick; } }
    public bool ComboSkillOn { get { return _skillCombine; } set { _skillCombine = value; } }
    public bool IsShieldDisabled { get => _isShieldDisabled; set => _isShieldDisabled = true; }

    public GameObject ComboSkill;

    private void Start()
    {
        _particleSystemManager = FindObjectOfType<ParticleSystemManager>();
        _shieldEnergy = _shieldMaxEnergy;
        _heroAction = GetComponentInParent<HeroActions>();
        _heroAction.onGuardPerformed += OnGuardPerformed;
        _heroAction.onGuardExit += OnGuardExit;
        _heroAction.HeroStats.OnShieldRecovered += HeroStats_OnShieldRecovered;
    } 

    private void Update()
    {
        if (_isGuarding && !_isShieldDisabled)
        {
            if (_shield == null)
            {
                Debug.Log("Cannot Create Shield, No Element Attached");
            }
            else
            { 
               StartCoroutine(ShieldEnergyDecrease());
                Color color = _shield.GetComponent<SpriteRenderer>().color;
                color.a = (_shieldEnergy * 0.01f);
                _shield.GetComponent<SpriteRenderer>().color = color;
            }
        }
        if (_isGuarding && ComboSkillOn)
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
            ComboSkillOn = false;
        }

        if (_shieldBreak)
        {
            _shieldBreak = false;
            ShieldBreakEffect();
            OnGuardExit();
        }
    }

    private void OnGuardPerformed()
    {
        if (!_isShieldDisabled)
        {
            _isGuarding = true;
            SummonGuard();
        }
    }

    private void OnGuardExit()
    {
        //Destroy(_shield);
        _shield.SetActive(false);
        _isGuarding = false;
        _shieldCreated = false;
        OnShieldRecover.Invoke();
    }

    private void OnDestroy()
    {
        _heroAction.onGuardPerformed -= OnGuardPerformed;
        _heroAction.onGuardExit -= OnGuardExit;
    }

    private void SummonGuard()
    {
        if (!_shieldCreated && !_isShieldDisabled)
        {
            _shield.SetActive(true);
            Color color = _shield.GetComponent<SpriteRenderer>().color;
            color.a = (_shieldEnergy * 0.01f);
            _shield.GetComponent<SpriteRenderer>().color = color;
            _shieldCreated = true;
        }
    }

    private void HeroStats_OnShieldRecovered()
    {
        _isShieldDisabled = false;
    }

    public void TakeShieldDamage(float damage)
    {
        ShieldEnergy -= damage;
        if (ShieldEnergy <= 0)
        {
            _shieldBreak = true;
        }
    }

    private void ShieldBreakEffect()
    {

        _isShieldDisabled = true;
        _shieldBreakEffect.Play();
    }

    private IEnumerator ShieldEnergyDecrease()
    {
        _shieldEnergy -= _shieldExpendAmount;
        yield return new WaitForSeconds(_shieldEnergyTick);
        if(ShieldEnergy <= 0)
        {
            _shieldEnergy = 0;
            _shieldBreak = true;
        }
    }
}
