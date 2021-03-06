using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private float _knockBackAmount = 20f;
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
                    heroStats.TakeDamage(_heroAction.HeroStats.AttackDamage);
                    collision.GetComponent<HeroMovement>().OnKnockBackHit(_knockBackAmount, GetComponentInParent<HeroMovement>().GetIsLeft);
                    if (_heroAction.HeroStats.GetElement.Equals(Elements.ElementalAttribute.Fire))
                    {
                        _particleSystemManager.FireAura(_heroMovement.gameObject);
                    }
                }
                if (collision.TryGetComponent<Guard>(out Guard guard))
                {
                    if (guard.Guarding)
                    {                        
                        //guard.RecoveryTime = _hitStun;
                        //guard..Recovering = true;
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
                    heroStats.TakeDamage(_heroAction.HeroStats.AttackDamage);
                    collision.GetComponent<HeroMovement>().OnKnockBackHit(_knockBackAmount, GetComponentInParent<HeroMovement>().GetIsLeft);
                    if (_heroAction.HeroStats.GetElement == Elements.ElementalAttribute.Fire)
                    {
                        _particleSystemManager.FireAura(_heroMovement.gameObject);
                    }
                }
                if (!collision.GetComponent<Guard>().Guarding)
                {
                    collision.GetComponent<HeroMovement>().RecoveryTime = _hitStun;
                    collision.GetComponent<HeroMovement>().Recovering = true;
                }
            }
        }

        if (collision.GetComponent<Golem>())
        {
            collision.GetComponent<Golem>().TakeDamage(_heroAction.HeroStats.AttackDamage);
        }
    }
}
