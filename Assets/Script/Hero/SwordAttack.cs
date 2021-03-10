using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private float _knockBackXAmount = 10f;
    [SerializeField] private float _knockBackYAmount = 5f;
    [SerializeField] private float _knockBackLength = 0.2f;
    [SerializeField] private float _hitStun = 1f;
    private HeroActions _heroAction;
    private HeroMovement _heroMovement;
    private ParticleSystemManager _particleSystemManager;
    private Animator _animator;
    private bool _isOriginalDirectionleft;
    private Vector3 _originalLocalScale;

    private void Awake()
    {
        _particleSystemManager = FindObjectOfType<ParticleSystemManager>();
        _heroAction = GetComponentInParent<HeroActions>();
        _heroMovement = GetComponentInParent<HeroMovement>();
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
                            _heroAction.HeroMovement.OnKnockBackHit(10f, 10f, 0.2f, !_heroMovement.GetIsLeft);
                            //heroStats.Guard.TakeShieldDamage(_heroAction.HeroStats.AttackDamage);
                        }
                    }
                    else
                    {
                        heroStats.TakeDamage(_heroAction.HeroStats.AttackDamage);
                        collision.GetComponent<HeroMovement>().OnKnockBackHit(_knockBackXAmount, _knockBackYAmount, _knockBackLength,GetComponentInParent<HeroMovement>().GetIsLeft);
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
                        heroStats.TakeDamage(_heroAction.HeroStats.AttackDamage);
                        collision.GetComponent<HeroMovement>().OnKnockBackHit(_knockBackXAmount, _knockBackYAmount, _knockBackLength, GetComponentInParent<HeroMovement>().GetIsLeft);
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
            collision.GetComponent<Golem>().TakeDamage(_heroAction.HeroStats.AttackDamage);
        }
    }
}
