using UnityEngine;

public class AirJet : MonoBehaviour
{       

    private float _ProjectileSpeed = 1f;
    private float _ExitTime = 1f;
    private Rigidbody2D _RigidBody;
    private Vector3 _ScaleSize = new Vector3(0.5f, 0.5f, 0.5f);
    private AirSkills airskills;

    private AirSkills _airskills;
    [SerializeField] private GameObject _hitParticle;
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        airskills = FindObjectOfType<AirSkills>();
        _ProjectileSpeed = airskills.Speed;
        _ScaleSize = airskills.Scale;
        _ExitTime = airskills.ExitTime;
    }

    private void FixedUpdate()
    {
        if (_ExitTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        _ExitTime -= Time.deltaTime;
        _RigidBody.velocity = transform.right * _ProjectileSpeed;
        transform.localScale = Vector3.Lerp(transform.localScale, _ScaleSize, airskills.ScaleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Debug.Log("Trigger");
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(_airskills.Damage);
            }
        }

        if (collision.GetComponent<Guard>())
        {
            if (collision.GetComponent<Guard>().tag.Equals(_airskills.PlayerSkills.HeroAction.tag))
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
        //if (collision.GetComponentInParent<Walls>())
        //{
        //    Destroy(gameObject);
        //}
        if (airskills.PlayerSkills.HeroMovement.tag.Equals("Team1"))

        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.HitParticle = _hitParticle;
                    heroStats.TakeDamageFromProjectile(airskills.Damage);
                    Destroy(gameObject);
                }
            }
        }
        //chieck if Team1 hit
        if (_airskills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.HitParticle = _hitParticle;
                    heroStats.TakeDamageFromProjectile(airskills.Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
