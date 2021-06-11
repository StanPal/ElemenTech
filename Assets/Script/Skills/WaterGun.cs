using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private Rigidbody2D _RigidBody;
    private WaterSkills _WaterSkills;
    [SerializeField] private float _ProjectileSpeed;
    [SerializeField] private float _ExitTime = 2.0f;
    private bool _CanDamagePlayer = false;
    [SerializeField] private GameObject _waterExplosionEffect;
    [SerializeField] private GameObject _waterExplosionOnWall;
    [SerializeField] private Transform _endPoint;
    private SoundManager _soundManager;

    private void Awake()
    {
        _CanDamagePlayer = false;
        _RigidBody = GetComponent<Rigidbody2D>();
        _WaterSkills = FindObjectOfType<WaterSkills>();
        _ProjectileSpeed = _WaterSkills.Speed;
        _soundManager = ServiceLocator.Get<SoundManager>();
    }

    private void Start()
    {
        AudioSource.PlayClipAtPoint(_soundManager.ProjectileSounds[1], this.transform.position, _soundManager.AudioVolume);
    }

    private void FixedUpdate()
    {
        if (_ExitTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        _ExitTime -= Time.deltaTime;
        _RigidBody.velocity = transform.right * _WaterSkills.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(_WaterSkills.Damage);
                Destroy(gameObject);
            }
        }

        if (collision.GetComponentInParent<Walls>())
        {
            Instantiate(_waterExplosionOnWall, _endPoint.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (!collision.tag.Equals(_WaterSkills.PlayerSkills.HeroMovement.tag))
        {
            if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
            {
                if (heroStats.Guard.Guarding)
                {
                    heroStats.Guard.TakeShieldDamage(_WaterSkills.Damage);
                }
                else
                {
                    heroStats.TakeDamageFromProjectile(_WaterSkills.Damage);
                Instantiate(_waterExplosionEffect, collision.GetComponent<Transform>());
                }
                Destroy(gameObject);
            }
            //collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
            //collision.GetComponent<HeroStats>().SlowMovement(_WaterSkills.SlowAmount, _WaterSkills.SlowDuration);
        }

    }
}
