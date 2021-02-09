using UnityEngine;

public class FireBall : MonoBehaviour
{
    private FireSkills _fireSkills;
    private Rigidbody2D _rigidBody;
    private float _projectileSpeed;

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

        if (collision.GetComponent<Guard>())
        {
            if (collision.GetComponent<Guard>().tag.Equals(_fireSkills.PlayerSkills.HeroAction.tag))
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

        if (_fireSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamageFromProjectile(_fireSkills.Damage);
                    Destroy(gameObject);
                }
            }
        }

        if (_fireSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                collision.GetComponent<HeroStats>().TakeDamageFromProjectile(_fireSkills.Damage);
                Destroy(gameObject);
            }
        }
    }
}
