using UnityEngine;

public class FireBall : MonoBehaviour
{
    //private CanonBall canonball;
    private FireSkills _fireSkills;
    private Rigidbody2D _rigidBody;
    private float _projectileSpeed;
    [SerializeField] GameObject _fireExplosion; 

    private void Awake()
    {
        _fireSkills = FindObjectOfType<FireSkills>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _projectileSpeed = _fireSkills.Speed;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = transform.right * _projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Golem golem = collision.GetComponent<Golem>();
            golem.TakeDamage(_fireSkills.Damage);
            Destroy(gameObject);
        }

        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }

        if (!collision.tag.Equals(_fireSkills.PlayerSkills.HeroMovement.tag))
        {
            if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
            {
                if (heroStats.Guard.Guarding)
                {
                    heroStats.Guard.TakeShieldDamage(_fireSkills.Damage);
                }
                else
                {
                    collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.OnFire;
                    collision.GetComponent<HeroStats>().TakeDamageFromProjectile(_fireSkills.Damage);
                    collision.GetComponent<HeroStats>().DamageOverTime(_fireSkills.DotDamage, _fireSkills.DotDuration);
                }
                Instantiate(_fireExplosion, collision.GetComponent<Transform>().position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
