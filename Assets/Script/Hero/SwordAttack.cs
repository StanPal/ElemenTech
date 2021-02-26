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

    private void Awake()
    {
        _particleSystemManager = FindObjectOfType<ParticleSystemManager>();
        _heroAction = GetComponentInParent<HeroActions>();
        _heroMovement = GetComponentInParent<HeroMovement>();
        _animator = GetComponent<Animator>();
    }

   private void Start()
    {
        _heroAction.onAttackPerformed += OnAttackPerformed;
    }

    private void OnAttackPerformed()
    {
        StartCoroutine(SwordStart());
    }

    private IEnumerator SwordStart()
    {
        yield return new WaitForSeconds(0.182f);
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
                if (!collision.GetComponent<Guard>().Guarding)
                {
                    collision.GetComponent<HeroMovement>().RecoveryTime = _hitStun;
                    collision.GetComponent<HeroMovement>().Recovering = true;
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
