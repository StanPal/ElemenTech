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
    LayerMask whatIsEnemy;
    [SerializeField]
    float attackRange;

    [SerializeField]
    GameObject target;
    [SerializeField]
    float rotaSpeed;
    [SerializeField]
    float rotaBackSpeed;

    bool swingdown = false;
    bool beginSwing = false;

    private void Update()
    {
        //if (transform.rotation.z.Equals(-20.0f))
        //{
        //    SwordSwing(rotaSpeed);
        //}
        //else if (transform.rotation.z.Equals(0.0f))
        //{
        //    SwordSwing(rotaBackSpeed);
        //}

        if (swingdown && beginSwing)
        {
            SwordSwing(-rotaBackSpeed);
        }
        else if (!swingdown && beginSwing)
        {
            SwordSwing(rotaSpeed);
        }

        if (transform.rotation.z >= 0.45f )
        {
            swingdown = true;
        }
        if (transform.rotation.z <= 0.0f)
        {
            beginSwing = false;
            swingdown = false;
        }

        if (!beginSwing && Input.GetMouseButtonDown(0))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; ++i)
            {
                enemiesToDamage[i].GetComponent<Golem>().TakeDamage(damage);
            }
            beginSwing = true;
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Enemy>())
    //    {
    //        Debug.Log("Trigger");
    //        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    //        //&& transform.position.y -0.5 > enemy.transform.position.y +0.5
    //        if (enemy != null)
    //        {
    //            enemy.TakeDamage(damage);
    //        }
    //    }      
    //}     
}
