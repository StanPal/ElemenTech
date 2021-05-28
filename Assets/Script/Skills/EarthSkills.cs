using UnityEngine;

public class EarthSkills : MonoBehaviour
{
    // Earth Skills
    public GameObject EarthBoulder;
    public GameObject Points;
    private GameObject[] _pointsArr;
    public Vector3 LaunchOffset;
    private PlayerManager _playerManager;

    [SerializeField] private float _launchForce = 10f;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _splashRange = 1.5f;
    [SerializeField] private float _knockBackAmount = 8f;
    [SerializeField] private float _knockBackLength = 0.2f;
    private PlayerSkills _heroSkills;

    public bool FiredLeft;
    public float KnockBack { get => _knockBackAmount; }
    public float KnockBackLength { get => _knockBackLength; }
    public float SplashRange { get => _splashRange; } 
    public float Damage { get => _damage; } 
    public float Mass { get => _mass; } 
    public float LaunchForce { get => _launchForce; }     
    public PlayerSkills PlayerSkills { get => _heroSkills; } 


    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _heroSkills = GetComponent<PlayerSkills>();
        _heroSkills.onEarthSkillPerformed += Boulder;

    }
    private void Boulder()
    {
        GameObject earthskill = Instantiate(EarthBoulder, 
            _playerManager.mPlayersList[3].GetComponent<HeroActions>().FirePoint.position, Quaternion.Euler(0, 0, _heroSkills.HeroAction.GetLookAngle));
        earthskill.tag = PlayerSkills.HeroMovement.tag;
        Debug.Log(_heroSkills.HeroAction.GetLookAngle);
        if(_heroSkills.HeroAction.GetLookAngle > -90 && _heroSkills.HeroAction.GetLookAngle < 90)
        {
            FiredLeft = false;
        }
        else
        {
            FiredLeft = true;
        }
    }

    private Vector2 PointPosition(float t)
    { 
        Vector2 position = (Vector2)_playerManager.mPlayersList[3].GetComponent<HeroActions>().FirePoint.position + 
            ((Vector2)_playerManager.mPlayersList[3].GetComponent<HeroActions>().GetLookDir * LaunchForce * t)
            + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}