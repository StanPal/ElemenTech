using UnityEngine;

public class FastFallJump : MonoBehaviour
{
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _weightDropRate = 1f;
    [SerializeField] private float _weightModifier = 0.1f;
    [SerializeField] private float _weightMin = 2f;
    private SwordAttack _swordAttack;
     private float _fallOffsetSpeed = 2f;
    private float _originalFallMultiplier = 0f;
    public float WeightMin { get => _weightMin; }
    public float Weight { get => _fallMultiplier; set => _fallMultiplier = value; }
    public float WeightModifier { get => _weightModifier; }
    public float OriginalWeight { get => _originalFallMultiplier; }
    public float WeightDropRate { get => _weightDropRate; }    
    //[SerializeField]
    //private float mLowJumpMultiplier = 2f;

    Rigidbody2D _rb;
    private HeroMovement _heroMovement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _heroMovement = GetComponent<HeroMovement>();
        _swordAttack = GetComponentInChildren<SwordAttack>();
    }

    private void Start()
    {
        _originalFallMultiplier = _fallMultiplier;
        _swordAttack.onPlayerChargeAttack += onPlayerChargeAttack;
    }

    private void onPlayerChargeAttack()
    {
        _fallMultiplier = _originalFallMultiplier * 2;
    }

    private void FixedUpdate()
    {
        if(_rb.velocity.y < 0)
        {
            if (_heroMovement.WeightShifting)
            {    
                _rb.velocity += Vector2.up  * (_fallMultiplier - _fallOffsetSpeed) * Time.deltaTime;
                if (_heroMovement.SelfKnockBack <= 0)
                {
                    _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _fallMultiplier);
                }
            }
            else
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;


            }
        }
    }
}
