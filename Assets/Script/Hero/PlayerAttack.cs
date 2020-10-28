using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    float damage = 2;
    [SerializeField]
    float startTimeBtAttack;
    float timeBtwAttack;

    [SerializeField]
    Transform attackPos;
    [SerializeField]
    float attackRange;

    [SerializeField]
    GameObject target;
    [SerializeField]
    float rotaSpeed;
    [SerializeField]
    float rotaBackSpeed;
    HeroActions mHeroAction;
    HeroMovement mHeroMovement;
    float swordAngle = 45.0f;
    bool swingdown = false;
    bool beginSwing = false;
    bool swingActive = false;

    private void Awake()
    {
        mHeroAction = GetComponentInParent<HeroActions>();
        mHeroMovement = GetComponentInParent<HeroMovement>();
        mHeroAction.onAttackPerformed += AttackPerformed;
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
                collision.GetComponent<HeroStats>().TakeDamage(10f);
            }
        }
        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                collision.GetComponent<HeroStats>().TakeDamage(10f);
            }
        }

        if (collision.GetComponent<Golem>())
        {
            collision.GetComponent<Golem>().TakeDamage(50);
        }
    }
}
