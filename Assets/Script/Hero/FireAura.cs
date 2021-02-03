using System.Collections;
using UnityEngine;

public class FireAura : MonoBehaviour
{
    [SerializeField] private float _Damage = 5f;
    [SerializeField] private float _tick = 1f;
    private bool _TookDamage = false;
    public float SetDamage { set { _Damage = value; } }
    public float SetTick { set => _tick = value; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if(!_TookDamage)
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    StartCoroutine(DamageOverTimeCoroutine(heroStats,_Damage));
                }
            }
        }

        if (tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (!_TookDamage)
                {
                    if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                    {
                        StartCoroutine(DamageOverTimeCoroutine(heroStats, _Damage));
                    }
                }
            }
        }
    }

    private IEnumerator DamageOverTimeCoroutine(HeroStats hero, float damageAmount)
    {
        hero.TakeDamage(damageAmount);
        Debug.Log("Damaged Current Health: " + hero.CurrentHealth);
        _TookDamage = true;
        yield return new WaitForSeconds(_tick);
        _TookDamage = false;
    
    }

}
