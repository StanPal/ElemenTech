﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicater : MonoBehaviour
{
    
    public Transform Target;
    public float HideDistance;

    void Update()
    {
        var dir = Target.position - transform.position;

        if (dir.magnitude < HideDistance)
        {
            SetChildActive(false);
        }
        else
        {
            SetChildActive(true);

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetChildActive(bool value)
    {
        foreach (Transform child in transform)

        {
            child.gameObject.SetActive(value);
        }
    }
}
