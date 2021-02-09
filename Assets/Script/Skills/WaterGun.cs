using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private WaterSkills _waterSkills;
    private bool _canDamagePlayer = false;

    [SerializeField] private float _damage = 2;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _exitTime = 2.0f;

    private void Awake()
    {
        _canDamagePlayer = false;
        _rigidBody = GetComponent<Rigidbody2D>();
        _waterSkills = FindObjectOfType<WaterSkills>();
        _projectileSpeed = _waterSkills.Speed;
    }

    private void FixedUpdate()
    {
        if (_exitTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        _exitTime -= Time.deltaTime;
        _rigidBody.velocity = transform.right * _projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        if (_canDamagePlayer)
        {
            if (collision.collider.GetComponent<Golem>())
            {
                Golem golem = collision.gameObject.GetComponent<Golem>();
                if (golem != null)
                {
                    golem.TakeDamage(_damage);
                    Destroy(gameObject);
                }
            }

            if (collision.collider.GetComponent<Guard>())
            {
                if (collision.collider.GetComponent<Guard>().tag.Equals(_waterSkills.PlayerSkills.HeroAction.tag))
                {
                    Guard guard = collision.collider.GetComponent<Guard>();
                    if (guard.Guarding)
                    {
                        Destroy(gameObject);
                        Debug.Log("Shield Hit");
                        collision.collider.GetComponent<Guard>().ComboSkillOn = true;
                    }
                }
            }

            if (_waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
            {
                if (collision.collider.tag.Equals("Team2"))
                {
                    if (collision.collider.TryGetComponent<HeroStats>(out HeroStats heroStats))
                    {
                        heroStats.TakeDamageFromProjectile(_damage);
                        Destroy(gameObject);

                    }
                }
            }

            if (_waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
            {
                if (collision.collider.tag.Equals("Team1"))
                {
                    if (collision.collider.TryGetComponent<HeroStats>(out HeroStats heroStats))
                    {
                        heroStats.TakeDamageFromProjectile(_damage);
                        Destroy(gameObject);

                    }
                }
            }

            if (collision.collider.GetComponentInParent<Walls>())
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);
        _canDamagePlayer = true;
    }
}
