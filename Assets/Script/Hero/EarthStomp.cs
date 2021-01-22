using UnityEngine;

public class EarthStomp : MonoBehaviour
{
    private HeroStats _HeroStats;
    private float _StompDamage = 20f;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _HeroStats = GetComponentInParent<HeroStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(_HeroStats.tag.Equals("Team1"))
        {
            if(collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(_StompDamage);
                }
            }
        }

      if(_HeroStats.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(_StompDamage);
                }                
            }
        }
    }
}
