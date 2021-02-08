using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJet : MonoBehaviour
{       
    private float _projectileSpeed = 1f;
    private float _exitTime = 1f; 
    private Rigidbody2D _rigidbody;
    private Vector3 _scaleSize = new Vector3(0.5f, 0.5f, 0.5f);

    private AirSkills _airskills;
    [SerializeField]
    private GameObject _hitParticle;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _airskills = FindObjectOfType<AirSkills>();
        _projectileSpeed = _airskills.Speed;
        _scaleSize = _airskills.Scale;
        _exitTime = _airskills.ExitTime;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * _projectileSpeed;
        transform.localScale = Vector3.Lerp(transform.localScale, _scaleSize, _airskills.ScaleSpeed * Time.deltaTime);
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

        //if hit walls
        if (collision.GetComponentInParent<Walls>())
        {
            Destroy(gameObject);
        }
        //chieck if Team1 hit 
        if (_airskills.PlayerSkills.HeroMovement.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.HitParticle = _hitParticle;
                    heroStats.TakeDamage(_airskills.Damage);
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
                    heroStats.TakeDamage(_airskills.Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
