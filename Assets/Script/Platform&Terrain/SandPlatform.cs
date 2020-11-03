using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPlatform : MonoBehaviour
{
    float SandSpeed = 5f;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<HeroMovement>().SandDecrease(SandSpeed);
    }

}
