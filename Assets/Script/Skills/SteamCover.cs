using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SteamCover : MonoBehaviour
{
    private ParticleSystem _SteamParticle;
    [SerializeField] private float _SteamDuration = 2f;
    [SerializeField] private float _Damage = 2f;
    [SerializeField] private float _DamageTick = 1f;
    private float totalTime = 0;
    
    void Start()
    {
        _SteamParticle = GetComponent<ParticleSystem>();
        StartCoroutine(SteamTimer());
    }
    
    IEnumerator SteamTimer()
    {
        _SteamParticle.Play();
        yield return new WaitForSeconds(_SteamDuration);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        totalTime += Time.deltaTime;
        if (tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (totalTime > _DamageTick)
                {
                    collision.GetComponent<HeroStats>().TakeDamage(_Damage);
                    totalTime = 0;
                }
            }
        }
        if (tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (totalTime > _DamageTick)
                {
                    collision.GetComponent<HeroStats>().TakeDamage(_Damage);
                    totalTime = 0;
                }
            }
        }
    }
}
