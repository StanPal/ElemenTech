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
                    heroStats.TakeDamage(_Damage);
                }
            }
        }

        if (tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(_Damage);
                }
            }
        }
    }
}
