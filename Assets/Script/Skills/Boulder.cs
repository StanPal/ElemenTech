using UnityEngine;

public class Boulder : MonoBehaviour
{
    public GameObject ExplosionEffect;
    private Rigidbody2D _RigidBody;
    private EarthSkills _EarthSkills;
    private bool _HasHit;

    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        _EarthSkills = FindObjectOfType<EarthSkills>();
        _RigidBody.gravityScale = _EarthSkills.Gravity;
    }

    private void Update()
    {
        float angle = Mathf.Atan2(_RigidBody.velocity.y, _RigidBody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponentInParent<Walls>())
        {
            Explode();
            Destroy(gameObject);
        }

        if (_EarthSkills.SplashRange > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, _EarthSkills.SplashRange);
            foreach (var hitCollider in hitColliders)
            {
                var enemyStats = hitCollider.GetComponent<HeroStats>();
                var enemyMovement = hitCollider.GetComponent<HeroMovement>();
                if (tag.Equals("Team1")) 
                {
                    var closestPont = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPont, transform.position);
                    var damagePercent = Mathf.InverseLerp(_EarthSkills.SplashRange, 0, distance);
                    if (enemyStats && enemyStats.tag.Equals("Team2"))
                    {                        
                        enemyStats.TakeDamageFromProjectile(damagePercent * _EarthSkills.Damage);
                        enemyMovement.OnKnockBackHit(_EarthSkills.KnockBack, enemyMovement.GetIsLeft);
                    }
                    if (enemyStats && enemyStats.tag.Equals("Team1"))
                    {
                        enemyStats.TakeDamageFromProjectile(damagePercent * (_EarthSkills.Damage / 2f));
                        enemyMovement.OnKnockBackHit((_EarthSkills.KnockBack / 2f), !enemyMovement.GetIsLeft);
                    }
                }
                if (tag.Equals("Team2"))
                {
                    var closestPont = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPont, transform.position);
                    var damagePercent = Mathf.InverseLerp(_EarthSkills.SplashRange, 0, distance);
                    if (enemyStats && enemyStats.tag.Equals("Team1"))
                    {
                        enemyStats.TakeDamageFromProjectile(damagePercent * _EarthSkills.Damage);
                        enemyMovement.OnKnockBackHit(_EarthSkills.KnockBack, !enemyMovement.GetIsLeft);
                    }
                    if (enemyStats && enemyStats.tag.Equals("Team2"))
                    {
                        enemyStats.TakeDamageFromProjectile(damagePercent * (_EarthSkills.Damage / 2f));
                        enemyMovement.OnKnockBackHit((_EarthSkills.KnockBack / 2f), !enemyMovement.GetIsLeft);
                    }
                }
                
            }
        }

        if(gameObject.tag.Equals("Team1"))
        {
           if(collision.collider.tag.Equals("Team2"))
            {
                if(collision.gameObject.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamageFromProjectile(_EarthSkills.Damage);
                    Explode();
                    Destroy(gameObject);
                }
            }
        }
        if(gameObject.tag.Equals("Team2"))
        {
            if (collision.collider.tag.Equals("Team1"))
            {
                if (collision.gameObject.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamageFromProjectile(_EarthSkills.Damage);
                    Explode();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void Explode()
    {
        ParticleSystem ps = ExplosionEffect.GetComponent<ParticleSystem>();
        var sh = ps.shape;
        sh.radius = _EarthSkills.SplashRange;
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        ExplosionEffect.GetComponent<ParticleSystem>().Play();
    }
}
