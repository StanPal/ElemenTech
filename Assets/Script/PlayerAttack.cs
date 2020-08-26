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

    private void Update()
    {
        if(timeBtwAttack <= 0 && Input.GetMouseButtonDown(0))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; ++i)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
            timeBtwAttack = startTimeBtAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
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
