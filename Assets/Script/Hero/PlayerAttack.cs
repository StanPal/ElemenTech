﻿using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private ParticleSystemManager _ParticleSystemManager;
    public event System.Action<GameObject> onAuraActivated;
    public event System.Action<GameObject> onAuraDeActivated;

    [SerializeField]
    private float startTimeBtAttack;
    private float timeBtwAttack;
    [SerializeField]
    private float mHitStun = 1f;

    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private float attackRange;
    
    [SerializeField]
    public GameObject target;
    [SerializeField]
    private float rotaSpeed;
    [SerializeField]
    private float rotaBackSpeed;
    HeroActions mHeroAction;
    HeroMovement mHeroMovement;
    private float swordAngle = 45.0f;
    private bool swingdown = false;
    private bool beginSwing = false;
    private bool swingActive = false;

    private Transform originalRotation;
    [SerializeField]
    private float mKnockBackAmount = 5f;    

    private void Awake()
    {
        _ParticleSystemManager = FindObjectOfType<ParticleSystemManager>();
        mHeroAction = GetComponentInParent<HeroActions>();
        mHeroMovement = GetComponentInParent<HeroMovement>();
        mHeroAction.onAttackPerformed += AttackPerformed;
        originalRotation = transform;
    }
        

    private void AttackPerformed()
    {
        Debug.Log("Action Performed");
        swingActive = true;
        beginSwing = true;
    }

    private void Update()
    {
        if (swingActive)
        {
            if (mHeroMovement.GetIsLeft)
            {
                if (swingdown && beginSwing)
                {
                    SwordSwing(rotaBackSpeed);
                }
                else if (!swingdown && beginSwing)
                {
                    SwordSwing(-rotaSpeed);
                }
                if (transform.rotation.z <= -0.45f)
                {
                    swingdown = true;
                }
                if (transform.rotation.z >= 0.0f)
                {
                    beginSwing = false;
                    swingdown = false;
                    this.gameObject.SetActive(false);
                    transform.position = originalRotation.position;

                }
            }
            else
            {

                if (swingdown && beginSwing)
                {
                    SwordSwing(-rotaBackSpeed);
                }
                else if (!swingdown && beginSwing)
                {
                    SwordSwing(rotaSpeed);
                }
                if (transform.rotation.z >= 0.45f)
                {
                    swingdown = true;
                }
                if (transform.rotation.z <= 0.0f)
                {
                    beginSwing = false;
                    swingdown = false;
                    this.gameObject.SetActive(false);
                    transform.position = originalRotation.position;
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void SwordSwing(float Speed)
    {
        transform.RotateAround(target.transform.position, Vector3.forward, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(mHeroAction.HeroStats.AttackDamage);
                    collision.GetComponent<HeroMovement>().OnKnockBackHit(mKnockBackAmount, GetComponentInParent<HeroMovement>().GetIsLeft);
                    _ParticleSystemManager.FireAura(mHeroMovement.gameObject);
                }
                if (!collision.GetComponent<Guard>().Guarding)
                {
                    collision.GetComponent<HeroMovement>().RecoveryTime = mHitStun;
                    collision.GetComponent<HeroMovement>().Recovering = true;
                }
            }
        }
        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(mHeroAction.HeroStats.AttackDamage);
                    collision.GetComponent<HeroMovement>().OnKnockBackHit(mKnockBackAmount, GetComponentInParent<HeroMovement>().GetIsLeft);
                }                
                if (!collision.GetComponent<Guard>().Guarding)
                {
                    collision.GetComponent<HeroMovement>().RecoveryTime = mHitStun;
                    collision.GetComponent<HeroMovement>().Recovering = true;
                }
            }
        }

        if (collision.GetComponent<Golem>())
        {
            collision.GetComponent<Golem>().TakeDamage(50);
        }
    }


}
