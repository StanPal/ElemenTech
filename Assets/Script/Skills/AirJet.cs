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
        _ExitTime = _AirSkills.ExitTime;
        _AirSkills.PlayerSkills.HeroMovement.OnSelfKnockBack
            (new Vector2(-_AirSkills.PlayerSkills.HeroAction.CrossHair.transform.position.x,
            -_AirSkills.PlayerSkills.HeroAction.CrossHair.transform.position.y).normalized * _AirSkills.KnockBackMulitplier, _AirSkills.KnockBackLength);
        Debug.Log(new Vector2(-_AirSkills.PlayerSkills.HeroAction.CrossHair.transform.position.x,
            -_AirSkills.PlayerSkills.HeroAction.CrossHair.transform.position.y).normalized);
        //if(_AirSkills.PlayerSkills.HeroAction.ChargeMax)
        //{
        //    isChargeMax = true;
        //    _AirSkills.PlayerSkills.HeroAction.ChargeMax = false;
        //    _AirSkills.PlayerSkills.HeroAction.ChargeAmount = 0;
        //    _AirSkills.PlayerSkills.HeroMovement.OnKnockBackHit
        //        (-_AirSkills.PlayerSkills.HeroAction.FirePoint.transform.position.x,
        //        -_AirSkills.PlayerSkills.HeroAction.FirePoint.transform.position.y, 1f, !_AirSkills.PlayerSkills.HeroMovement.GetIsLeft);
        //    _AirSkills.Damage = _AirSkills.Damage * 2f;
        //}
        //else
        //{
        //    isChargeMax = false;
        //    _AirSkills.PlayerSkills.HeroMovement.OnKnockBackHit(2f, 2f, 0.5f, !_AirSkills.PlayerSkills.HeroMovement.GetIsLeft);
        //}
    }

    private void FixedUpdate()
    {
        if (_ExitTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        _ExitTime -= Time.deltaTime;
        _RigidBody.velocity = transform.right * _ProjectileSpeed;
      //  transform.localScale = Vector3.Lerp(transform.localScale, _ScaleSize, _AirSkills.ScaleSpeed * 2f * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }


        if (collision.GetComponent<Golem>())
        {
            Debug.Log("Trigger");
            Golem golem = collision.gameObject.GetComponent<Golem>();
            if (golem != null)
            {
                golem.TakeDamage(_AirSkills.Damage);
            }
        }

        if (!collision.tag.Equals(_AirSkills.PlayerSkills.HeroMovement.tag))
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
    }
