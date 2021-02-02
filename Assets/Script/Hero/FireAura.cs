using System.Collections;
using UnityEngine;

public class FireAura : MonoBehaviour
{
    [SerializeField] private float _Damage = 0f;
    public float SetDamage { set { _Damage = value; } }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    StartCoroutine(DamageOverTimeCoroutine(heroStats,_Damage, 1f));
                }
            }
        }

        if (tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    StartCoroutine(DamageOverTimeCoroutine(heroStats,_Damage, 1f));
                }
            }
        }
    }


    private IEnumerator DamageOverTimeCoroutine(HeroStats hero, float damageAmount, float duration)
    {
        float amountDamaged = 0;
        float damagePerloop = damageAmount / duration;
        while (amountDamaged < damageAmount)
        {
            hero.TakeDamageFromProjectile(damagePerloop);
            yield return new WaitForSeconds(1f);
        }
    }

}
