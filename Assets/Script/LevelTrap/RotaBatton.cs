using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaBatton : MonoBehaviour
{
    [SerializeField] private RotationFloor _rotationFloor = null;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var heroStats = collision.gameObject.GetComponent<HeroStats>();
        if (heroStats)
        {
            _rotationFloor.StartRota();
        }
    }
}
