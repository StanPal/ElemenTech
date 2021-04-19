using UnityEngine;

public class FastFallJump : MonoBehaviour
{
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _weightDropRate = 1f;
    [SerializeField] private float _weightModifier = 0.1f;
    [SerializeField] private float _weightMin = 0.3f;
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
    }

    private void Start()
    {
        _originalFallMultiplier = _fallMultiplier;
    }

    private void FixedUpdate()
    {
        if(_rb.velocity.y < 0)
        {
            if (_heroMovement.WeightShifting)
            {
                _rb.velocity += Vector2.up  * (_fallMultiplier - _fallOffsetSpeed) * Time.deltaTime;

            }
            else
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}
