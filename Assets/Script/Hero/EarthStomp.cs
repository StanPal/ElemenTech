using UnityEngine;

public class EarthStomp : MonoBehaviour
{
    private HeroStats _heroStats;
    private HeroActions _heroActions;
    private HeroMovement _heroMovement;
    [SerializeField] private float _stompDamage = 30f;
    private bool _isHit = false;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _heroMovement = GetComponent<HeroMovement>();
        _heroActions = GetComponentInParent<HeroActions>();
        _heroStats = GetComponentInParent<HeroStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isHit)
        {
            if (!collision.collider.tag.Equals(_heroStats.tag))
            {
                if (collision.collider.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    _isHit = true;
                    this.gameObject.SetActive(false);
                    heroStats.TakeDamage(_stompDamage);
                    heroStats.HeroMovement.Rigidbody2D.velocity = -Vector2.up * _stompDamage;
                    heroStats.GetComponent<HeroMovement>().Recovering = true;
                }
            }
        }

        if (collision.collider.GetComponentInParent<Walls>())
        {
            this.gameObject.SetActive(false);
        }

        _heroActions.PlayerAnimator.SetBool("IsFastFall", false);
        _heroActions.IsEarthStomping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isHit = false;
    }
}
