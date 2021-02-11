using UnityEngine;

public class AirSkills : MonoBehaviour
{
    public GameObject AirJet;
    private PlayerSkills _HeroSkills;

    [SerializeField] float _Damage = 2.0f;
    [SerializeField] float _Speed = 10.0f;
    [SerializeField] float _ExitTime = 10.0f;
    [SerializeField] Vector3 _Scale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] float _ScaleSpeed = 0.5f;
    

    //Getters
    public float Damage { get { return _Damage; } }
    public float Speed { get { return _Speed; } }
    public float ExitTime { get { return _ExitTime; } }
    public Vector3 Scale { get { return _Scale; } }
    public float ScaleSpeed { get { return _ScaleSpeed; } }
    public PlayerSkills PlayerSkills { get { return _HeroSkills; } }


    private void Start()
    {
        _HeroSkills = GetComponent<PlayerSkills>();
        _HeroSkills.onAirSkillPerformed += AirJetCast;
    }

    void AirJetCast()
    {
        Instantiate(AirJet, _HeroSkills.HeroAction.FirePoint.transform.position, Quaternion.Euler(0, 0, _HeroSkills.HeroAction.GetLookAngle));
    }
}
