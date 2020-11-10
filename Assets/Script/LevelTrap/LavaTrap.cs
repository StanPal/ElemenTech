using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    [SerializeField] private float damageTime = 1.0f;
    [SerializeField] private float damage = 5.0f;
    private const float delayTime = 1.0f;
    
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

    private IEnumerator LavaDamageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            if(_trappedHeros.Count > 0)
            {
                foreach(var trappedHero in _trappedHeros)
                {
                    trappedHero.HeroStats.TakeDamage(damage);
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
        }
    }
}
