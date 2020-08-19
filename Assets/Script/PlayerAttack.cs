using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            Debug.Log("Trigger");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //&& transform.position.y -0.5 > enemy.transform.position.y +0.5
            if (enemy != null)
            {
                enemy.KillEnemy();
            }
        }      
    }     
}
