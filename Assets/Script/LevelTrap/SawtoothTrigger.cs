using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawtoothTrigger : MonoBehaviour
{
    [SerializeField] private SawtoothTrap mTrap = null;
    [SerializeField] private int mTriggerId = 0;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SwordAttack>())
        {
            mTrap.Move(mTriggerId);
        }
    }
}
