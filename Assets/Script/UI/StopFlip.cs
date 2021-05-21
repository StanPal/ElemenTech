using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFlip : MonoBehaviour
{
    Quaternion rotation;
    private HeroMovement _heroMovement;
    private void Awake()
    {
        _heroMovement = GetComponentInParent<HeroMovement>();
    }

    private void FixedUpdate()
    {
        Vector3 characterScale = transform.localScale;
        if (_heroMovement.MoveInput  < 0)
        {
            characterScale.x = -0.5f;
            _heroMovement.GetIsLeft = true;
        }

        if (_heroMovement.MoveInput > 0)
        {
            characterScale.x = 0.5f;
            _heroMovement.GetIsLeft = false;
        }

        transform.localScale = characterScale;

    }
}
