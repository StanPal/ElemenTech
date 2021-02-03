using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private ParticleSystemManager _ParticleSystemManager;
    public event System.Action<GameObject> onAuraActivated;
    public event System.Action<GameObject> onAuraDeActivated;

    [SerializeField]
    private float startTimeBtAttack;
    private float timeBtwAttack;
    [SerializeField]
    private float mHitStun = 1f;
    [SerializeField]
    private float attackRange;
    
    [SerializeField]
    private float rotaSpeed;
    [SerializeField]
    private float rotaBackSpeed;
    HeroActions mHeroAction;
    HeroMovement mHeroMovement;

    [SerializeField] private float swordAngle = 45.0f;
    private bool swingdown = false;
    private bool beginSwing = false;
    private bool swingActive = false;

    [SerializeField] private float _rotationpersec = 20f;
    [SerializeField] private float _rotationLimit = 10f;
    [SerializeField] private float _rotation = 0;
    [SerializeField] private float originalRotation = 60f;
    [SerializeField] private float mKnockBackAmount = 5f; 

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
            transform.RotateAround(transform.position, Vector3.forward, rotaBackSpeed * Time.deltaTime);
            _rotation = _rotation + (Time.deltaTime * _rotationpersec);
            if (_rotation >= _rotationLimit)
            {
                _rotation = 0;
                transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, -originalRotation);
                this.gameObject.SetActive(false);
                swingActive = false;
            }
        }
        else
        {
            //    if (Vector3.Distance(transform.eulerAngles, target.transform.eulerAngles) > 0.01f)
            //    {
            //        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, -target.transform.eulerAngles, Time.deltaTime);
            //    }
            //    else
            //    {
            //        transform.eulerAngles = target.transform.eulerAngles;
            //        this.gameObject.SetActive(false);

            //    }
            transform.RotateAround(transform.position, -Vector3.forward, rotaBackSpeed * Time.deltaTime);
            _rotation = _rotation + (Time.deltaTime * _rotationpersec);
            if (_rotation >= _rotationLimit)
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
