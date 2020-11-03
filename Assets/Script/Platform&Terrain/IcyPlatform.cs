using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyPlatform : MonoBehaviour
{
    float SlidSpeed = 5f;
    private void OnTriggerEnter(Collider other)
    {
        SlidSpeed = Random.Range(0f, 10f);
        other.gameObject.GetComponent<HeroMovement>().IcySlidding(SlidSpeed);
    }




}
