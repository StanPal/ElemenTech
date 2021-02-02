using UnityEngine;

public class Guard : MonoBehaviour
{
    public GameObject Shield;
    private GameObject _shield;
    private HeroActions _heroAction;
    private HeroMovement _heroMovement;
    private bool _shieldCreated = false;
    private bool _skillCombine = false;
    private bool _isShieldDisabled = false;
    private bool _shieldBreak = false;

    private ParticleSystemManager _particleSystemManager;

    [SerializeField] private bool _IsGuarding = false;
    [SerializeField] private float _GuardTime = 0.5f;
    [SerializeField] private float _shieldSize = 2.1f;
    [SerializeField] private float _shieldRecoveryTick = 3f;
    [SerializeField] private float _SpeedDecrease = 1f;
    [SerializeField] private float _shieldMaxEnergy = 100f;
    [SerializeField] private float _shieldRecoverAmount = 1f;
    [SerializeField] private float _shieldEnergyTick = 0.2f;
    [SerializeField] private float _shieldEnergy = 100f;

    // Setters & Getters    
    public bool Guarding { get => _IsGuarding; set => _IsGuarding = value; } 
    public bool IsShieldDisabled { get => _isShieldDisabled; set => _isShieldDisabled = value; } 
    public float ShieldMaxEnergy { get => _shieldMaxEnergy; set => _shieldMaxEnergy = value; }
    public float ShieldEnergy { get => _shieldEnergy; set => _shieldEnergy = value; } 
    public float ShieldRecoveryAmount { get => _shieldRecoverAmount; }
    public float ShieldRecoveryTick { get => _shieldRecoveryTick; } 
    public bool ComboSkillOn { get => _skillCombine; set => _skillCombine = value; }


    public GameObject ComboSkill;

    private void Start()
    {
        _particleSystemManager = FindObjectOfType<ParticleSystemManager>();
        _shieldEnergy = _shieldMaxEnergy;
        _heroAction = GetComponentInParent<HeroActions>();
        _heroAction.onGuardPerformed += OnGuardPerformed;
        _heroAction.onGuardExit += OnGuardExit;
    }

    private void OnGuardPerformed()
    {
        Guarding = true;
        SummonGuard();
    }

    private void Update()
    {
        if (_IsGuarding)
        {
            if (_shield == null)
            {
                Debug.Log("Cannot Create Shield, No Element Attached");
            }
            else
            {
                _shield.transform.position = Vector3.MoveTowards(_shield.transform.position, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), 1.0f);
                if (_shieldEnergy > 0)
                {
                    _shieldEnergy -= Time.deltaTime / _shieldEnergyTick;
                }
                else
                {
                    _isShieldDisabled = true;
                    _shieldBreak = true;
                    OnGuardExit();
                }
                Color color = _shield.GetComponent<SpriteRenderer>().color;
                color.a = (_shieldEnergy * 0.01f);
                _shield.GetComponent<SpriteRenderer>().color = color;
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

    private void OnGuardExit()
    {
        Destroy(_shield);
        if (_shieldBreak)
        {
            GameObject shieldBreak = Instantiate(_particleSystemManager.OtherEffects[0]);
            shieldBreak.transform.position = this.transform.position;
            _shieldBreak = false;
            if (shieldBreak.TryGetComponent<ParticleSystem>(out ParticleSystem particle))
            {
                particle.Play();
            }
        }
        _IsGuarding = false;
        _shieldCreated = false;
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
            _shield = Instantiate(Shield, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), Quaternion.identity);
            _shield.transform.localScale = new Vector3(_shieldSize,_shieldSize,_shieldSize);
            Color color = _shield.GetComponent<SpriteRenderer>().color;
            color.a = (_shieldEnergy * 0.01f);
            _shield.GetComponent<SpriteRenderer>().color = color;
            _shieldCreated = true;
        }
    }    
}
