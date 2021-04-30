using UnityEngine;

public class AirJet : MonoBehaviour
{
    private float _ProjectileSpeed = 1f;
    private float _ExitTime = 1f;
    private bool isChargeMax = false; 
    private Rigidbody2D _RigidBody;
    private Vector3 _ScaleSize = new Vector3(0.5f, 0.5f, 0.5f);
    private AirSkills _AirSkills;

    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        _AirSkills = FindObjectOfType<AirSkills>();
        _ProjectileSpeed = _AirSkills.Speed;
        _ScaleSize = _AirSkills.Scale;
        _ExitTime = _AirSkills.ExitTime;
            _AirSkills.PlayerSkills.HeroMovement.OnKnockBackHit
                (_AirSkills.KnockbackX, _AirSkills.knockbackY , _AirSkills.knockbackLength, !_AirSkills.PlayerSkills.HeroMovement.GetIsLeft);
    }

    private void FixedUpdate()
    {
        if (_ExitTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        _ExitTime -= Time.deltaTime;
        _RigidBody.velocity = transform.right * _ProjectileSpeed;
        transform.localScale = Vector3.Lerp(transform.localScale, _ScaleSize, _AirSkills.ScaleSpeed * 2f * Time.deltaTime);

        //if (isChargeMax)
        //{
        //    transform.localScale = Vector3.Lerp(transform.localScale, _ScaleSize, _AirSkills.ScaleSpeed * 2f * Time.deltaTime);
        //}
        //else
        //{
        //    transform.localScale = Vector3.Lerp(transform.localScale, _ScaleSize / 2, _AirSkills.ScaleSpeed * Time.deltaTime);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Debug.Log("Trigger");
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(_AirSkills.Damage);
            }
        }

        if (_AirSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    if (heroStats.Guard.Guarding)
                    {
                        heroStats.Guard.TakeShieldDamage(_AirSkills.Damage);
                    }
                    else
                    {
                        collision.GetComponent<HeroStats>().TakeDamageFromProjectile(_AirSkills.Damage);
                    }
                    Destroy(gameObject);
                }
            }
        }
        if (_AirSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    if (heroStats.Guard.Guarding)
                    {
                        heroStats.Guard.TakeShieldDamage(_AirSkills.Damage);
                    }
                    else
                    {
                        collision.GetComponent<HeroStats>().TakeDamageFromProjectile(_AirSkills.Damage);
                    }
                    Destroy(gameObject);
                }
            }
        }

        if (_AirSkills.PlayerSkills.HeroMovement.tag.Equals("FFA"))
        {
            if (!collision.Equals(this) && collision.tag.Equals("FFA"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamageFromProjectile(_AirSkills.Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
