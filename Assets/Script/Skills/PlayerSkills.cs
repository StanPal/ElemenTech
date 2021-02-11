using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public event System.Action onEarthSkillPerformed;
    public event System.Action onFireSkillPerformed;
    public event System.Action onAirSkillPerformed;
    public event System.Action onWaterSkillPerformed;

    private HeroActions _heroAction;
    public HeroActions HeroAction { get => _heroAction; } 
    private HeroMovement _heroMovement;
    public HeroMovement HeroMovement { get => _heroMovement; } 
    private bool _isSkillActivated = false;
    public bool SkillActive { get => _isSkillActivated;  set => _isSkillActivated = value; } 
 
    private PlayerManager _playerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        if (_playerManager._playersList[0].gameObject != null)
        {
            _playerManager._playersList[0].GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        }
        if (_playerManager._playersList[1].gameObject != null)
        {
            _playerManager._playersList[1].GetComponentInChildren<HeroActions>().onSkillPerformed += PerformSkill;
        }
        if (_playerManager._playersList[2].gameObject != null)
        {
            _playerManager._playersList[2].GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        }
        if (_playerManager._playersList[3].gameObject != null)
        {
            _playerManager._playersList[3].GetComponent<HeroActions>().onSkillPerformed += PerformSkill;
        }
    }

    void PerformSkill(Elements.ElementalAttribute elementalAttribute)
    {
        switch (elementalAttribute)
        {
            case Elements.ElementalAttribute.Fire:
                _heroAction = _playerManager._playersList[0].GetComponent<HeroActions>();
                _heroMovement = _playerManager._playersList[0].GetComponent<HeroMovement>();
                onFireSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Earth:
                _heroAction = _playerManager._playersList[3].GetComponent<HeroActions>();
                _heroMovement = _playerManager._playersList[3].GetComponent<HeroMovement>();
                onEarthSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Water:
                _heroAction = _playerManager._playersList[1].GetComponent<HeroActions>();
                _heroMovement = _playerManager._playersList[1].GetComponent<HeroMovement>();
                onWaterSkillPerformed.Invoke();
                break;
            case Elements.ElementalAttribute.Air:
                _heroAction = _playerManager._playersList[2].GetComponent<HeroActions>();
                _heroMovement = _playerManager._playersList[2].GetComponent<HeroMovement>();
                onAirSkillPerformed.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        if (_playerManager._playersList[0].gameObject != null)
        {
            _playerManager._playersList[0].GetComponent<HeroActions>().onSkillPerformed -= PerformSkill;
        }
        if (_playerManager._playersList[1].gameObject != null)
        {
            _playerManager._playersList[2].GetComponent<HeroActions>().onSkillPerformed -= PerformSkill;
        }
        if (_playerManager._playersList[2].gameObject != null)
        {
            _playerManager._playersList[2].GetComponent<HeroActions>().onSkillPerformed -= PerformSkill;
        }
        if (_playerManager._playersList[3].gameObject != null)
        {
            _playerManager._playersList[3].GetComponent<HeroActions>().onSkillPerformed -= PerformSkill;
        }
    }
}
