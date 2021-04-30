using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeMovement : MonoBehaviour
{

    bool _isActive = false;
    float _delayTime = 0.0f;
    float _currentDelayTime = 0.0f;

    private void Awake()
    {
        _isActive = false;
        _currentDelayTime = 0.0f;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_delayTime < Time.time)
            {
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            GetComponentInChildren<IceSpikeTrap>().activeSpike();
            if (!_isActive)
            {
                _delayTime = _currentDelayTime + Time.time;
            }
            _isActive = true;
        }
    }
}
