using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFlip : MonoBehaviour
{
    Quaternion rotation = Quaternion.identity;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
