using System.Collections;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public event System.Action OnShieldRecover;
    public event System.Action onParryWindowActive;
    public event System.Action onParryAttack;

    [SerializeField] private GameObject _shield;
    [SerializeField] private ParticleSystem _shieldBreakEffect;
    private HeroActions _heroAction;
    private HeroMovement _heroMovement;
    private float _resetTimer;
    private bool _isGuardTimerActive = false;
    private bool _shieldCreated = false;
    private bool _skillCombine = false;
    private bool _shieldBreak = false;
    private bool _isShieldDisabled = false;
    private bool _canParry = false;

    private ParticleSystemManager _particleSystemManager;

    [SerializeField] private float _guardTimer = 1f;
    [SerializeField] private bool _isGuarding = false;
    [SerializeField] private float _brokenShieldRecovery = 3f;
    [SerializeField] private float _shieldSize = 2.1f;
    [SerializeField] private float _shieldRecoveryTick = 3f;
    [SerializeField] private float _speedDecrease = 1f;
    [SerializeField] private float _shieldMaxEnergy = 100f;
    [SerializeField] private float _shieldExpendAmount = 2f;
    [SerializeField] private float _shieldRecoverAmount = 1f;
    [SerializeField] private float _shieldEnergyTick = 0.2f;
    [SerializeField] private float _shieldEnergy = 100f;
    [SerializeField] private float _parryTimeWindow = 1f;

    // Setters & Getters
    public bool Guarding { get { return _isGuarding; } }
    public float ShieldMaxEnergy { get { return _shieldMaxEnergy; } }
    public float ShieldEnergy { get { return _shieldEnergy; } set { _shieldEnergy = value; } }
    public float ShieldRecoveryAmount { get { return _shieldRecoverAmount; } }
    public float ShieldRecoveryTime { get => _brokenShieldRecovery; }
    public float ShieldRecoveryTick { get { return _shieldRecoveryTick; } }
    public bool ComboSkillOn { get { return _skillCombine; } set { _skillCombine = value; } }
    public bool IsShieldDisabled { get => _isShieldDisabled; set => _isShieldDisabled = true; }
    public bool CanParry { get => _canParry; }

    private void Start()
    {
        _particleSystemManager = FindObjectOfType<ParticleSystemManager>();
        _shieldEnergy = _shieldMaxEnergy;
        _heroMovement = GetComponentInParent<HeroMovement>();
        _heroAction = GetComponentInParent<HeroActions>();
        _heroAction.onGuardPerformed += OnGuardPerformed;
        _heroAction.onGuardExit += OnGuardExit;
        _heroAction.HeroStats.OnShieldRecovered += HeroStats_OnShieldRecovered;
        onParryWindowActive += OnParryWindowActive;
        _resetTimer = _guardTimer;
    }

    private void Update()
    {
        if (_isGuardTimerActive)
        {
            if (_guardTimer > 0)
            {
                _guardTimer -= Time.deltaTime;
            }
            else
            {
                _guardTimer = 0;
                _isGuardTimerActive = false;
            }
        }
        if(!_isGuardTimerActive && !_isGuarding)
        {
            OnGuardExit();
        }

        if (_isGuarding && !_isShieldDisabled)
        {
            if (_shield == null)
            {
                Debug.Log("Cannot Create Shield, No Element Attached");
            }
            else
            {
                Color color = _shield.GetComponent<SpriteRenderer>().color;
                color.a = (_shieldEnergy * 0.01f);
                _shield.GetComponent<SpriteRenderer>().color = color;
            }
        }

        if(_shieldEnergy <= 0)
        {
            _shieldBreak = true;
            _isShieldDisabled = true;
            OnGuardExit();
        }
    }

    private void OnGuardPerformed()
    {
        if (!_isShieldDisabled)
        {
            _heroMovement.Speed = _heroMovement.Speed / 2f;
            _guardTimer = _resetTimer;
            _isGuarding = true;
            _isGuardTimerActive = true;
            onParryWindowActive?.Invoke();
            SummonGuard();
            StartCoroutine(ShieldEnergyDecrease());
        }
    }

    private void OnGuardExit()
    {
        _isGuarding = false;
         if(_shieldBreak)
        {
            ShieldBreakEffect();
            _shield.SetActive(false);
            _shieldCreated = false;
            _heroMovement.Speed = _heroMovement.OriginalMoveSpeed;
            OnShieldRecover.Invoke();
        }        
        else if (!_isGuardTimerActive)
        {
            _shield.SetActive(false);
            _shieldCreated = false;
            OnShieldRecover.Invoke();
            _heroMovement.Speed = _heroMovement.OriginalMoveSpeed;
        }
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

    private void OnParryWindowActive()
    {
        StartCoroutine(ParryWindow());
    }

    private IEnumerator ParryWindow()
    {
        float parryTimer = 0;
        while (_isGuarding && parryTimer < _parryTimeWindow)
        {
            _canParry = true;
            parryTimer += 1;
            yield return new WaitForSeconds(0.2f);
        }
        _canParry = false;
    }


    private void HeroStats_OnShieldRecovered()
    {
        _isShieldDisabled = false;
        _shieldBreak = false;
    }

    public void TakeShieldDamage(float damage)
    {
        ShieldEnergy -= damage;
    }

    private void ShieldBreakEffect()
    {
        _shieldBreakEffect.Play();
    }

    private IEnumerator ShieldEnergyDecrease()
    {
        while (_isGuarding)
        {
            _shieldEnergy -= _shieldExpendAmount;
            yield return new WaitForSeconds(_shieldEnergyTick);
        }
        if (ShieldEnergy <= 0)
        {
            _shieldEnergy = 0;
        }
    }
}
