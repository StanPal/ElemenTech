using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    [SerializeField] private float _damageTime = 1.0f;
    [SerializeField] private float _damage = 5.0f;
    private const float _delayTime = 1.0f;
    [SerializeField]
    private GameObject[] _waypoints;
    private int _current = 0;
    private float _wPreadius = 1;
    [SerializeField]
    private float _speed;

    public struct TrappedHeroData
    {
        public HeroStats HeroStats;
        public DateTime EnterTime;
    }
    private List<TrappedHeroData> _trappedHeros = new List<TrappedHeroData>();

    private void Awake()
    {
        StartCoroutine(LavaDamageRoutine());
    }

    void Update()
    {
        if (Vector3.Distance(_waypoints[_current].transform.position, transform.position) < _wPreadius)
        {
            _current++;
            if (_current >= _waypoints.Length)
            {
                _current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_current].transform.position, Time.deltaTime * _speed);
    }

    private IEnumerator LavaDamageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delayTime);
            if(_trappedHeros.Count > 0)
            {
                foreach(var trappedHero in _trappedHeros)
                {
                    trappedHero.HeroStats.TakeDamage(_damage, true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            TrappedHeroData data = new TrappedHeroData()
            {
                HeroStats = collision.GetComponent<HeroStats>(),
                EnterTime = DateTime.Now
            };
            _trappedHeros.Add(data);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroStats>())
        {
            //TODO - remove hero from _trappedHeros list.
            TrappedHeroData data = new TrappedHeroData()
            {
                HeroStats = collision.GetComponent<HeroStats>(),
                EnterTime = DateTime.Now
            };

            for (int i = 0; i < _trappedHeros.Count; ++i)
            {
                if (_trappedHeros[i].HeroStats == data.HeroStats)
                {
                    _trappedHeros.Remove(_trappedHeros[i]);
                }
            }
        }
    }
}
