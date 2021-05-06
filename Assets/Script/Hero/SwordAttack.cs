using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public event System.Action onPlayerChargeAttack;

    private float _knockBackXAmount = 10f;
    private float _knockBackYAmount = 5f;
    private float _knockBackLength = 0.2f;
    private float _hitStun = 1f;
    private HeroActions _heroAction;
    private HeroMovement _heroMovement;
    private HeroStats _heroStats;
    private FastFallJump _fastFallJump;
    private ParticleSystemManager _particleSystemManager;
    private Animator _animator;
    private bool _isOriginalDirectionleft;
    private bool _isChargeMax = false;
    private float _meleeDamage; 
    private Vector3 _originalLocalScale;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _particleSystemManager = FindObjectOfType<ParticleSystemManager>();
        _heroAction = GetComponentInParent<HeroActions>();
        _heroMovement = GetComponentInParent<HeroMovement>();
        _heroStats = GetComponentInParent<HeroStats>();
        _fastFallJump = GetComponentInParent<FastFallJump>();
        _animator = GetComponent<Animator>();
    }

   private void Start()
    {
        _originalLocalScale = _heroMovement.transform.localScale;
        _heroMovement.onPlayerFlip += OnPlayerFlipPerformed;
        _heroAction.onAttackPerformed += OnAttackPerformed;
        _heroAction.onParryPerformed += OnParryPerformed;

    }   

    private void OnParryPerformed()
    {
        _animator.SetBool("isAttacking", true);
        StartCoroutine(SwordStart());
    }

    private void OnPlayerFlipPerformed()
    {

    }

    private IEnumerator TurnBackAnimator()
    {
        _animator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _animator.enabled = true;
    }

    private void OnAttackPerformed()
    {
        if (_heroAction.ChargeMax)
        {
            _isChargeMax = true;
            _heroAction.ChargeMax = false;
            _heroAction.ChargeAmount = 0;
            _meleeDamage = _heroStats.AttackDamage * 2f;
            _knockBackXAmount = _heroStats.KnockBackXAmount * 2f;
            _knockBackYAmount = _heroStats.KnockBackYAmount * 2f;
            _knockBackLength = _heroStats.KnockBackLength * 2f;
            _hitStun = _heroStats.HitStun * 2f;
            onPlayerChargeAttack?.Invoke();
        }
        else
        {
            _meleeDamage = _heroStats.AttackDamage;
            _knockBackXAmount = _heroStats.KnockBackXAmount;
            _knockBackYAmount = _heroStats.KnockBackYAmount;
            _knockBackLength = _heroStats.KnockBackLength;
            _hitStun = _heroStats.HitStun;
            _isChargeMax = false;
        }
        StartCoroutine(SwordStart());
    }

    private IEnumerator SwordStart()
    {  
        yield return new WaitForSeconds(0.217f);
        _animator.SetBool("IsAttacking", false);
        _heroAction._isSwinging = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    if (heroStats.Guard.Guarding)
                    {
                        if (heroStats.Guard.CanParry)
                        {
                            if (heroStats.HeroMovement.GetIsLeft && _heroMovement.GetIsLeft)
                            {
                                heroStats.HeroMovement.flipCharacter();
                            }
                            else if (!heroStats.HeroMovement.GetIsLeft && !_heroMovement.GetIsLeft)
                            {
                                heroStats.HeroMovement.flipCharacter();

                            }
                            heroStats.HeroActions.InvokeParry();
                        }
                        else
                        {
                            _heroAction.HeroMovement.OnKnockBackHit(_knockBackXAmount, _knockBackYAmount, _knockBackLength, !_heroMovement.GetIsLeft);
                            //heroStats.Guard.TakeShieldDamage(_heroAction.HeroStats.AttackDamage);
                        }
                    }
                    else
                    {
                        if (_heroStats.GetElement == Elements.ElementalAttribute.Water && _heroAction.DashStriking)
                        {
                            Debug.Log("AttackHitTriggered");
                            _boxCollider.isTrigger = false;
                            heroStats.TakeDamage(_meleeDamage);
                        }
                        else
                        {
                            heroStats.TakeDamage(_meleeDamage);
                            collision.GetComponent<HeroMovement>().OnKnockBackHit(_knockBackXAmount, _knockBackYAmount, _knockBackLength, GetComponentInParent<HeroMovement>().GetIsLeft);
                        }                    
                        if (_heroAction.HeroStats.GetElement.Equals(Elements.ElementalAttribute.Fire))
                        {
                            _particleSystemManager.FireAura(_heroMovement.gameObject);
                        }
                    }
                }
            }
        }

        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    if (heroStats.Guard.Guarding)
                    {
                        if (heroStats.Guard.CanParry)
                        {
                            if(heroStats.HeroMovement.GetIsLeft && _heroMovement.GetIsLeft)
                            {
                                heroStats.HeroMovement.flipCharacter();
                            }
                            else if (!heroStats.HeroMovement.GetIsLeft && !_heroMovement.GetIsLeft)
                            {
                                heroStats.HeroMovement.flipCharacter();

                            }
                            heroStats.HeroActions.InvokeParry();
                        }
                        else
                        {
                            _heroAction.HeroMovement.OnKnockBackHit(10f, 10f, 0.2f, !_heroMovement.GetIsLeft);
                            //heroStats.Guard.TakeShieldDamage(_heroAction.HeroStats.AttackDamage);
                        }
                    }
                    else
                    {
                        if (_heroAction.DashStriking)
                        {
                            Debug.Log("AttackHitTriggered");
                            heroStats.TakeDamage(_meleeDamage);
                        }
                        else
                        {
                            heroStats.TakeDamage(_meleeDamage);
                            collision.GetComponent<HeroMovement>().OnKnockBackHit(_knockBackXAmount, _knockBackYAmount, _knockBackLength, GetComponentInParent<HeroMovement>().GetIsLeft);
                        }                     
                        if (_heroAction.HeroStats.GetElement.Equals(Elements.ElementalAttribute.Fire))
                        {
                            _particleSystemManager.FireAura(_heroMovement.gameObject);
                        }
                    }
                }
            }
        }

        if (collision.GetComponent<Golem>())
        {
            collision.GetComponent<Golem>().TakeDamage(_meleeDamage);
        }
    }
}
