using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //&& transform.position.y -0.5 > enemy.transform.position.y +0.5
            if (enemy != null)
            {
                enemy.KillEnemy();
            }
        }      
    }
}
