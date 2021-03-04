using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject _p1CrossHairs;
    public GameObject _p2CrossHairs;
    public GameObject _p3CrossHairs;
    public GameObject _p4CrossHairs;

    private Vector3 _p1Target;
    private Vector3 _p2Target;
    private Vector3 _p3Target;
    private Vector3 _p4Target;

    private HeroActions _fireHero = null;
    private HeroActions _waterHero = null;
    private HeroActions _airHero = null;
    private HeroActions _earthHero = null;

    private PlayerManager _playerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        if (_playerManager.mPlayersList[0] != null)
        {
            _fireHero = _playerManager.mPlayersList[0].GetComponent<HeroActions>();
        }
        if (_playerManager.mPlayersList[1] != null)
        {
            _waterHero = _playerManager.mPlayersList[1].GetComponent<HeroActions>();
        }
        if (_playerManager.mPlayersList[2] != null)
        {
            _airHero = _playerManager.mPlayersList[2].GetComponent<HeroActions>();
        }
        if (_playerManager.mPlayersList[3] != null)
        {
            _earthHero = _playerManager.mPlayersList[3].GetComponent<HeroActions>();
        }
        if (_fireHero.gameObject.activeSelf)
        {
            _p1CrossHairs.SetActive(true);
        }
        if (_waterHero.gameObject.activeSelf)
        {
            _p2CrossHairs.SetActive(true);
        }
        if (_airHero.gameObject.activeSelf)
        {
            _p3CrossHairs.SetActive(true);
        }
        if (_earthHero.gameObject.activeSelf)
        {
            _p4CrossHairs.SetActive(true);
        }

        Cursor.visible = false;
    }


    void Update()
    {
        if(_fireHero == null)
        {
            return;
        }

        if (_fireHero.TryGetComponent<HeroMovement>(out HeroMovement fireMovement))
        {
            switch (fireMovement.ControllerInput)
            {
                case HeroMovement.Controller.None:
                    break;
                case HeroMovement.Controller.Keyboard:
                    KeyboardCorssHairs(_fireHero,_p1CrossHairs,_p1Target);
                    break;
                case HeroMovement.Controller.PS4:
                    PS4CorssHairs(_fireHero,_p1CrossHairs);
                    break;
                case HeroMovement.Controller.XBOX:
                    XBOXCorssHairs(_fireHero,_p1CrossHairs);
                    break;
                case HeroMovement.Controller.Gamepad:
                    PS4CorssHairs(_fireHero,_p1CrossHairs);
                    break;
                default:
                    break;
            }
        }
        if (_waterHero.TryGetComponent<HeroMovement>(out HeroMovement waterMovement))
        {
            switch (waterMovement.ControllerInput)
            {
                case HeroMovement.Controller.None:
                    break;
                case HeroMovement.Controller.Keyboard:
                    KeyboardCorssHairs(_waterHero, _p2CrossHairs, _p2Target);
                    break;
                case HeroMovement.Controller.PS4:
                    PS4CorssHairs(_waterHero, _p2CrossHairs);
                    break;
                case HeroMovement.Controller.XBOX:
                    XBOXCorssHairs(_waterHero, _p2CrossHairs);
                    break;
                case HeroMovement.Controller.Gamepad:
                    PS4CorssHairs(_waterHero,_p2CrossHairs);
                    break;
                default:
                    break;
            }
        }
        if (_airHero.TryGetComponent<HeroMovement>(out HeroMovement airMovement))
        {
            switch (airMovement.ControllerInput)
            {
                case HeroMovement.Controller.None:
                    break;
                case HeroMovement.Controller.Keyboard:
                    KeyboardCorssHairs(_airHero, _p3CrossHairs, _p3Target);
                    break;
                case HeroMovement.Controller.PS4:
                    PS4CorssHairs(_airHero,_p3CrossHairs);
                    break;
                case HeroMovement.Controller.XBOX:
                    XBOXCorssHairs(_airHero,_p3CrossHairs);
                    break;
                case HeroMovement.Controller.Gamepad:
                    PS4CorssHairs(_airHero, _p3CrossHairs);
                    break;
                default:
                    break;
            }
        }

        switch (_earthHero.HeroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                KeyboardCorssHairs(_earthHero,_p4CrossHairs,_p4Target);
                break;
            case HeroMovement.Controller.PS4:
                PS4CorssHairs(_earthHero,_p4CrossHairs);
                break;
            case HeroMovement.Controller.XBOX:
                XBOXCorssHairs(_earthHero,_p4CrossHairs);
                break;
            case HeroMovement.Controller.Gamepad:
                PS4CorssHairs(_earthHero, _p4CrossHairs);
                break;
            default:
                break;
        }
    }

    void KeyboardCorssHairs(HeroActions hero, GameObject crossHairs, Vector3 target)
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(hero.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
        crossHairs.transform.position = new Vector3(target.x, target.y);
    }

    void PS4CorssHairs(HeroActions hero, GameObject crossHairs)
    {
        crossHairs.transform.SetParent(hero.transform);
        if (hero.PlayerInput.PS4.Aim.ReadValue<Vector2>().x.Equals(0f) &&
            hero.PlayerInput.PS4.Aim.ReadValue<Vector2>().y.Equals(0f))
        {
            crossHairs.SetActive(false);
        }
        else
        {
            crossHairs.SetActive(true);
        }
        crossHairs.transform.position = new Vector3(
                          hero.transform.position.x + (hero.PlayerInput.PS4.Aim.ReadValue<Vector2>().x * 5.5f),
                          hero.transform.position.y + hero.PlayerInput.PS4.Aim.ReadValue<Vector2>().y * 5.5f);
    }

    void XBOXCorssHairs(HeroActions hero, GameObject crossHairs)
    {
        crossHairs.transform.SetParent(hero.transform);
        //if (hero.PlayerInput.XBOX.Aim.ReadValue<Vector2>().x.Equals(0f) &&
        //    hero.PlayerInput.XBOX.Aim.ReadValue<Vector2>().y.Equals(0f))
        //{
        //    crossHairs.SetActive(false);
        //}
        //else
        //{
        //    crossHairs.SetActive(true);
        //}
        crossHairs.transform.position = new Vector3(
                          hero.transform.position.x + (_playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().x * 5.5f),
                          hero.transform.position.y + _playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().y * 5.5f);
    }
}
