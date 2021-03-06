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

        if (_earthHero.TryGetComponent<HeroMovement>(out HeroMovement earthMovement))
        {
            switch (earthMovement.ControllerInput)
            {
                case HeroMovement.Controller.None:
                    break;
                case HeroMovement.Controller.Keyboard:
                    KeyboardCorssHairs(_earthHero, _p4CrossHairs, _p4Target);
                    break;
                case HeroMovement.Controller.PS4:
                    PS4CorssHairs(_earthHero, _p4CrossHairs);
                    break;
                case HeroMovement.Controller.XBOX:
                    XBOXCorssHairs(_earthHero, _p4CrossHairs);
                    break;
                case HeroMovement.Controller.Gamepad:
                    PS4CorssHairs(_earthHero, _p4CrossHairs);
                    break;
                default:
                    break;
            }
        }
    }

    void KeyboardCorssHairs(HeroActions hero, GameObject crossHairs, Vector3 target)
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(hero.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
        crossHairs.transform.position = new Vector3(target.x, target.y);
    }

    void PS4CorssHairs(HeroActions hero, GameObject crossHairs)
    {
        // Read it in once and cache instead of making multiple property access calls.
        var aimDirection = hero.PlayerInput.PS4.Aim.ReadValue<Vector2>();
        Debug.Log($"[INPUT] PS4 RAWINPUT X:{aimDirection.x} Y:{aimDirection.y}");

        // Why is this here? The crosshairs should be a child already, and SetParent is an _extremely_ expensive operation.
        // DO NOT DO THIS.
        crossHairs.transform.SetParent(hero.transform);

        // From what you have told me you want to keep the crosshairs active all the time. 
        if (aimDirection.x.Equals(0f) && aimDirection.y.Equals(0f))
        {
            //crossHairs.SetActive(false);
            
            // If the player isn't actively aiming we'll just exit and not update the position of the crosshairs.
            return;
        }
        //else
        //{
        //    crossHairs.SetActive(true);
        //}

        // Normalize your direction input to get the unit vector in the direction the player is aiming.
        aimDirection.Normalize();
        var aimDirVec3 = new Vector3(aimDirection.x, aimDirection.y, 0.0f);
        Debug.Log($"[INPUT] PS4 NORMALIZED X:{aimDirVec3.x} Y:{aimDirVec3.y}");

        // Now set the crosshairs position to be a scalar value away from the hero position in the direction the player is aiming.
        // This 5.5f value should be either set as a const define, or made as a [SerializeField] private float _crossHairDist; so that it can be tuned in the inspector.
        var crossHairPos = hero.transform.position + (aimDirVec3 * 5.5f);
        crossHairs.transform.position = new Vector3(crossHairPos.x, crossHairPos.y, crossHairPos.z);
        Debug.Log($"[INPUT] PS4 FINAL X: {crossHairs.transform.position.x} Y: {crossHairs.transform.position.y}");
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
