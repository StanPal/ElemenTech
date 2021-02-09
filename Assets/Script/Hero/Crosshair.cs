using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject _P1CrossHairs;
    public GameObject _P2CrossHairs;
    public GameObject _P3CrossHairs;
    public GameObject _P4CrossHairs;

    [SerializeField] private Vector3 _P1Target;
    [SerializeField] private Vector3 _P2Target;
    [SerializeField] private Vector3 _P3Target;
    [SerializeField] private Vector3 _P4Target;

    private PlayerManager _playerManager;
    private HeroActions _playerOne;
    private HeroActions _playerTwo;
    private HeroActions _playerThree;
    private HeroActions _playerFour;


    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        if(_playerManager.mPlayersList.Count == 1)
        {
            _P1CrossHairs.SetActive(true);
        }        
        if (_playerManager.mPlayersList.Count == 2)
        {
            _P1CrossHairs.SetActive(true);
            _P2CrossHairs.SetActive(true);
        }        
        if (_playerManager.mPlayersList.Count == 3)
        {
            _P1CrossHairs.SetActive(true);
            _P2CrossHairs.SetActive(true);
            _P3CrossHairs.SetActive(true);
        }        
        if (_playerManager.mPlayersList.Count == 4)
        {
            _P1CrossHairs.SetActive(true);
            _P2CrossHairs.SetActive(true);
            _P3CrossHairs.SetActive(true);
            _P4CrossHairs.SetActive(true);
        }        
        Cursor.visible = false;

        _playerOne = _playerManager.mPlayersList[0].GetComponent<HeroActions>();
        _playerTwo = _playerManager.mPlayersList[1].GetComponent<HeroActions>();
        _playerThree = _playerManager.mPlayersList[2].GetComponent<HeroActions>();
        _playerFour = _playerManager.mPlayersList[3].GetComponent<HeroActions>();

    }

    private void Update()
    {
        switch ( _playerOne.HeroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _P1Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_playerOne.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
                _P1CrossHairs.transform.position = new Vector3(_P1Target.x, _P1Target.y);
                break;
            case HeroMovement.Controller.PS4:
                _P1CrossHairs.transform.SetParent(_playerOne.transform);
                if (_playerOne.PlayerInput.PS4.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    _playerOne.PlayerInput.PS4.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P1CrossHairs.SetActive(false);
                }
                else
                {
                    _P1CrossHairs.SetActive(true);
                }     
                _P1CrossHairs.transform.position = new Vector3(
                    _playerOne.transform.position.x + (_playerOne.PlayerInput.PS4.Aim.ReadValue<Vector2>().x * 5.5f),
                    _playerOne.transform.position.y +  _playerOne.PlayerInput.PS4.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.XBOX:
                _P1CrossHairs.transform.SetParent(_playerOne.transform);
                if (_playerOne.GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    _playerOne.GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P1CrossHairs.SetActive(false);
                }
                else
                {
                    _P1CrossHairs.SetActive(true);
                }
                _P1CrossHairs.transform.position = new Vector3(
                    _playerOne.transform.position.x + (_playerOne.PlayerInput.XBOX.Aim.ReadValue<Vector2>().x * 5.5f),
                    _playerOne.transform.position.y +  _playerOne.PlayerInput.XBOX.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.Gamepad:
                _P1CrossHairs.transform.SetParent(_playerOne.transform);
                if (_playerOne.PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    _playerOne.PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P1CrossHairs.SetActive(false);
                }
                else
                {
                    _P1CrossHairs.SetActive(true);
                }
                _P1CrossHairs.transform.position = new Vector3(
                    _playerOne.transform.position.x + (_playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x * 5.5f),
                    _playerOne.transform.position.y + _playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            default:
                break;
        }

        switch (_playerTwo.HeroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _P2Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_playerTwo.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
                _P2CrossHairs.transform.position = new Vector3(_P2Target.x, _P2Target.y);
                break;
            case HeroMovement.Controller.PS4:
                _P2CrossHairs.transform.SetParent(_playerTwo.transform);
                if (_playerTwo.PlayerInput.PS4.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    _playerTwo.PlayerInput.PS4.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P2CrossHairs.SetActive(false);
                }
                else
                {
                    _P2CrossHairs.SetActive(true);
                }
                _P2CrossHairs.transform.position = new Vector3(
                                  _playerTwo.transform.position.x + (_playerTwo.PlayerInput.PS4.Aim.ReadValue<Vector2>().x * 5.5f),
                                  _playerTwo.transform.position.y +  _playerTwo.PlayerInput.PS4.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.XBOX:
                _P2CrossHairs.transform.SetParent(_playerTwo.transform);
                if (_playerTwo.PlayerInput.XBOX.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    _playerTwo.PlayerInput.XBOX.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P2CrossHairs.SetActive(false);
                }
                else
                {
                    _P2CrossHairs.SetActive(true);
                }
                _P2CrossHairs.transform.position = new Vector3(
                                  _playerTwo.transform.position.x + (_playerTwo.PlayerInput.XBOX.Aim.ReadValue<Vector2>().x * 5.5f),
                                  _playerTwo.transform.position.y +  _playerTwo.PlayerInput.XBOX.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.Gamepad:
                _P2CrossHairs.transform.SetParent(_playerTwo.transform);                
                if (_playerTwo.PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    _playerTwo.PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P2CrossHairs.SetActive(false);
                }
                else
                {
                    _P2CrossHairs.SetActive(true);
                }
                _P2CrossHairs.transform.position = new Vector3(
                                  _playerTwo.transform.position.x + (_playerTwo.GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x * 5.5f),
                                  _playerTwo.transform.position.y +  _playerTwo.GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }


        switch (_playerThree.HeroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _P3Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_playerThree.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
                _P3CrossHairs.transform.position = new Vector3(_P3Target.x, _P3Target.y);
                break;
            case HeroMovement.Controller.PS4:
                _P3CrossHairs.transform.SetParent(_playerThree.transform);                
                if (_playerThree.PlayerInput.PS4.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    _playerThree.PlayerInput.PS4.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P3CrossHairs.SetActive(false);
                }
                else
                {
                    _P3CrossHairs.SetActive(true);
                }
                _P3CrossHairs.transform.position = new Vector3(
                                  _playerThree.transform.position.x + (_playerThree.PlayerInput.PS4.Aim.ReadValue<Vector2>().x * 5.5f),
                                  _playerThree.transform.position.y +  _playerThree.PlayerInput.PS4.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.XBOX:
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }

        if (_playerFour.HeroMovement.ControllerInput != HeroMovement.Controller.None)
        {
            _P4Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_playerFour.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            _P4CrossHairs.transform.position = new Vector3(_P4Target.x, _P4Target.y);
        }
    }
}
