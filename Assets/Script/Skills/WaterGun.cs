using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField]
    private float damage = 2;
    [SerializeField]
    private float projectileSpeed;
    private Rigidbody2D mRigidbody;
    [SerializeField]
    private float exitTime = 2.0f;
    private WaterSkills waterSkills;
    private bool _CanDamagePlayer = false;
    private void Awake()
    {
        _CanDamagePlayer = false;
        mRigidbody = GetComponent<Rigidbody2D>();
        waterSkills = FindObjectOfType<WaterSkills>();
        projectileSpeed = waterSkills.Speed;

    }

    private void FixedUpdate()
    {
        if (exitTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        exitTime -= Time.deltaTime;
        mRigidbody.velocity = transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        if (_CanDamagePlayer)
        {
            if (collision.collider.GetComponent<Golem>())
            {
                Golem golem = collision.gameObject.GetComponent<Golem>();
                if (golem != null)
                {
                    golem.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }

            if (collision.collider.GetComponent<Guard>())
            {
                if (collision.collider.GetComponent<Guard>().tag.Equals(waterSkills.PlayerSkills.HeroAction.tag))
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

            if (waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
            {
                if (collision.collider.tag.Equals("Team2"))
                {
                    if (collision.collider.TryGetComponent<HeroStats>(out HeroStats heroStats))
                    {
                        heroStats.TakeDamageFromProjectile(damage);
                        Destroy(gameObject);

                    }
                    //collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
                    //collision.GetComponent<HeroStats>().SlowMovement(waterSkills.SlowAmount, waterSkills.SlowDuration);
                }
            }
            if (waterSkills.PlayerSkills.HeroMovement.tag.Equals("Team2"))
            {
                if (collision.collider.tag.Equals("Team1"))
                {
                    if (collision.collider.TryGetComponent<HeroStats>(out HeroStats heroStats))
                    {
                        heroStats.TakeDamageFromProjectile(damage);
                        Destroy(gameObject);

                    }
                    //collision.GetComponent<HeroStats>().TakeDamage(damage);
                    //collision.GetComponent<HeroStats>().DeBuff = StatusEffects.NegativeEffects.Slowed;
                    //collision.GetComponent<HeroStats>().SlowMovement(waterSkills.SlowAmount, waterSkills.SlowDuration);
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
        _CanDamagePlayer = true;
    }

}
