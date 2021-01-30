using UnityEngine;

public class FireBall : MonoBehaviour
{
    //private CanonBall canonball;
    private FireSkills _FireSkills;
    private Rigidbody2D _RigidBody;
    private float _ProjectileSpeed;

    private void Awake()
    {
        _FireSkills = FindObjectOfType<FireSkills>();
        _RigidBody = GetComponent<Rigidbody2D>();
        _ProjectileSpeed = _FireSkills.Speed;
    }

    private void FixedUpdate()
    {
        _RigidBody.velocity = transform.right * _ProjectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Golem golem = collision.GetComponent<Golem>();
            golem.TakeDamage(_FireSkills.Damage);
            Destroy(gameObject);
        }
        if (collision.GetComponent<Guard>())
        {
            if (collision.GetComponent<Guard>().tag.Equals(_FireSkills.PlayerSkills.HeroAction.tag))
            {
                Guard guard = collision.GetComponent<Guard>();
                if (guard.Guarding)
                {
                    Destroy(gameObject);
                    Debug.Log("Shield Hit");
                    collision.GetComponent<Guard>().ComboSkillOn = true;
                }
            }
        }
        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }
        if (_FireSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.OnFire;
                    heroStats.TakeDamageFromProjectile(_FireSkills.Damage);
                    collision.GetComponent<HeroStats>().DamageOverTime(_FireSkills.Damage, _FireSkills.DotDuration);
                    Destroy(gameObject);
                }
            }
        }
        if (_FireSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
               collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.OnFire;
                collision.GetComponent<HeroStats>().TakeDamageFromProjectile(_FireSkills.Damage);
                collision.GetComponent<HeroStats>().DamageOverTime(_FireSkills.Damage, _FireSkills.DotDuration);
                Destroy(gameObject);
            }
        }

        if (_FireSkills.PlayerSkills.HeroMovement.tag.Equals("FFA"))
        {
            if (!collision.Equals(_FireSkills.PlayerSkills.HeroMovement.tag))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamageFromProjectile(_FireSkills.Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
