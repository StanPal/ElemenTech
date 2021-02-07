using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private ParticleSystemManager _ParticleSystemManager;
    public event System.Action<GameObject> onAuraActivated;
    public event System.Action<GameObject> onAuraDeActivated;
    HeroActions mHeroAction;
    HeroMovement mHeroMovement;

    [SerializeField]
    private float startTimeBtAttack;
    private float timeBtwAttack;
    [SerializeField] private float mHitStun = 1f;
    private bool swingdown = false;
    private bool beginSwing = false;
    private bool swingActive = false;

    [SerializeField] private float _rotationSpeed = 200f;
    [SerializeField] private float _rotationPerFrame = 20f;
    [SerializeField] private float _rotationgAngleLimit = 10f;
    [SerializeField] private float originalRotation = 60f;
    [SerializeField] private float mKnockBackAmount = 5f; 
    [SerializeField] private float _rotation = 0;
    private bool _originalDirLeft;
    private void Awake()
    {
        _ParticleSystemManager = FindObjectOfType<ParticleSystemManager>();
        mHeroAction = GetComponentInParent<HeroActions>();
        mHeroMovement = GetComponentInParent<HeroMovement>();
        mHeroAction.onAttackPerformed += AttackPerformed;
    }
  

    private void AttackPerformed()
    {
        Debug.Log("Action Performed");
        swingActive = true;
        beginSwing = true;
        if (mHeroMovement.GetIsLeft)
        {
            _originalDirLeft = true;
            transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, -originalRotation);
        }
        else
        {
            _originalDirLeft = false;
            transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, originalRotation);
        }
    }

    private void Update()
    {
        if (swingActive)
        {            
            if (mHeroMovement.GetIsLeft)
            {
                if(swingActive)
                {
                    SwordSwing(true);
                }
            }            
            else
            {
                if (swingActive)
                {
                    SwordSwing(false);
                }
            }
            if (mHeroMovement.GetIsLeft == !_originalDirLeft)
            {
                _rotation = 0;
               
                gameObject.SetActive(false);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void SwordSwing(bool isLeft)
    {
        if (isLeft)
        {
            transform.RotateAround(transform.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
            _rotation = _rotation + (Time.deltaTime * _rotationPerFrame);
            if (_rotation >= _rotationgAngleLimit)
            {
                _rotation = 0;
                transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, -originalRotation);
                this.gameObject.SetActive(false);
                swingActive = false;
            }
        }
        else
        {
            transform.RotateAround(transform.position, -Vector3.forward, _rotationSpeed * Time.deltaTime);
            _rotation = _rotation + (Time.deltaTime * _rotationPerFrame); 
            if (_rotation >= _rotationgAngleLimit)
            {
                _rotation = 0;
                transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, originalRotation);                
                this.gameObject.SetActive(false);
                swingActive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team1"))
        {
            if (collision.tag.Equals("Team2"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(mHeroAction.HeroStats.AttackDamage);
                    collision.GetComponent<HeroMovement>().OnKnockBackHit(mKnockBackAmount, GetComponentInParent<HeroMovement>().GetIsLeft);
                    if (mHeroAction.HeroStats.GetElement.Equals(Elements.ElementalAttribute.Fire))
                    {
                        _ParticleSystemManager.FireAura(mHeroMovement.gameObject);
                    }
                }
                if (!collision.GetComponent<Guard>().Guarding)
                {
                    collision.GetComponent<HeroMovement>().RecoveryTime = mHitStun;
                    collision.GetComponent<HeroMovement>().Recovering = true;
                }
            }
        }
        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team2"))
        {
            if (collision.tag.Equals("Team1"))
            {
                if (collision.TryGetComponent<HeroStats>(out HeroStats heroStats))
                {
                    heroStats.TakeDamage(mHeroAction.HeroStats.AttackDamage);
                    collision.GetComponent<HeroMovement>().OnKnockBackHit(mKnockBackAmount, GetComponentInParent<HeroMovement>().GetIsLeft);
                    if (mHeroAction.HeroStats.GetElement == Elements.ElementalAttribute.Fire)
                    {
                        _ParticleSystemManager.FireAura(mHeroMovement.gameObject);
                    }
                }                
                if (!collision.GetComponent<Guard>().Guarding)
                {
                    collision.GetComponent<HeroMovement>().RecoveryTime = mHitStun;
                    collision.GetComponent<HeroMovement>().Recovering = true;
                }
            }
        }

        if (collision.GetComponent<Golem>())
        {
            collision.GetComponent<Golem>().TakeDamage(50);
        }
    }


}
