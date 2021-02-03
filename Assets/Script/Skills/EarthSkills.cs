using UnityEngine;

public class EarthSkills : MonoBehaviour
{
    // Earth Skills
    public GameObject EarthBoulder;
    public GameObject Points;
    private GameObject[] _PointsArr;
    [SerializeField] int _NumberOfPoints = 20;
    [SerializeField] float _SpaceBetweenPoints = 0.01f;
    [SerializeField] private float _LaunchForce = 10f;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _Damage = 10f;
    [SerializeField] private float _SplashRange = 1.5f;
    [SerializeField] private float _KnockBackAmount = 2f;
    public Vector3 LaunchOffset;
    private PlayerSkills mHeroSkills;
    public float KnockBack { get { return _KnockBackAmount; } }
    public float SplashRange { get { return _SplashRange; } }
    public float Damage { get { return _Damage; } }
    public float Mass { get { return _mass; } }
    public float LaunchForce { get {return _LaunchForce; } }
    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }
    private PlayerManager _PlayerManager;


    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
        _PointsArr = new GameObject[_NumberOfPoints];
    }

    private void Initialize()
    {
        _PlayerManager = FindObjectOfType<PlayerManager>();
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onEarthSkillPerformed += Boulder;

    }

    private void Update()
    {

    }

    //private void Start()
    //{
    //    for (int i = 0; i < _NumberOfPoints; ++i)
    //    {
    //        _PointsArr[i] = Instantiate(Points, _PlayerManager.mPlayersList[3].GetComponent<HeroActions>().FirePoint.position, Quaternion.identity);
    //    }
    //}

    //private void Update()
    //{
    //    for (int i = 0; i < _NumberOfPoints; ++i)
    //    {
    //        _PointsArr[i].transform.position = PointPosition(i * _SpaceBetweenPoints);
    //    }
    //}

    private void Boulder()
    {
        //Quaternion.Euler(0, 0, _PlayerManager.mPlayersList[3].GetComponent<HeroActions>().GetLookAngle)
        //if(_PlayerManager.mPlayersList[3].GetComponent<HeroMovement>().GetIsLeft)
        //{
        //    _LaunchForce *= -1;
        //}
        //else
        //{
        //    _LaunchForce = Mathf.Abs(_LaunchForce);
        //}
        GameObject earthskill = Instantiate(EarthBoulder, _PlayerManager.mPlayersList[3].GetComponent<HeroActions>().FirePoint.position, Quaternion.Euler(0, 0, mHeroSkills.HeroAction.GetLookAngle));
        //if (_PlayerManager.mPlayersList[3].GetComponent<HeroActions>().GetLookAngle<= 90 &&
        //    _PlayerManager.mPlayersList[3].GetComponent<HeroActions>().GetLookAngle >= -90)
        //{
        //    earthskill.GetComponent<Rigidbody2D>().velocity = transform.right * LaunchForce;
        //}
        //else
        //{
        //    earthskill.GetComponent<Rigidbody2D>().velocity = -transform.right * LaunchForce;
        //}
        earthskill.tag = PlayerSkills.HeroMovement.tag;
    }

    private Vector2 PointPosition(float t)
    { 
        Vector2 position = (Vector2)_PlayerManager.mPlayersList[3].GetComponent<HeroActions>().FirePoint.position + 
            (_PlayerManager.mPlayersList[3].GetComponent<HeroActions>().GetLookDir * LaunchForce * t)
            + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}