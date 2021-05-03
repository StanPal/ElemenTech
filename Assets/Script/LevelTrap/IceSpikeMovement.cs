using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeMovement : MonoBehaviour
{

    bool _isActive = false;
    public bool IsActive { get { return _isActive; }}

    private void Awake()
    {
        _isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            GetComponentInChildren<IceSpikeTrap>().activeSpike();
            _isActive = true;
        }
    }
}
