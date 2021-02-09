using UnityEngine;

public class FireSkills : MonoBehaviour
{ 
    // FireSkills 
    public GameObject FireBall;
    private PlayerSkills _HeroSkills;

    [SerializeField] private float _Speed = 10.0f;
    [SerializeField] private float _Damage = 10.0f;
    [SerializeField] private float _DotDuration = 5.0f;

    // Getters & Setters
    public float Speed { get { return _Speed; } set { _Speed = value; } }
    public float Damage { get { return _Damage; } }
    public float DotDuration { get { return _DotDuration; } }
    public PlayerSkills PlayerSkills { get { return _HeroSkills; } }

    private void Start()
    {
        _HeroSkills = GetComponent<PlayerSkills>();
        _HeroSkills.onFireSkillPerformed += FireBallCast;
    }

    private void FireBallCast()
    {
       Instantiate(FireBall, _HeroSkills.HeroAction.FirePoint.transform.position, Quaternion.Euler(0, 0, _HeroSkills.HeroAction.GetLookAngle));
    }
}
