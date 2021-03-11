using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject _crossHairs;

    private Vector3 _target;

    private HeroActions _heroAction = null;
    private HeroMovement _heroMovement = null;

    private void Awake()
    {
        _heroAction = GetComponent<HeroActions>();
        _heroMovement = GetComponent<HeroMovement>();
    }

    void Update()
    {
        if(_heroAction == null)
        {
            return;
        }

        switch (_heroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                KeyboardCorssHairs();
                break;
            case HeroMovement.Controller.PS4:
                PS4CorssHairs();
                break;
            case HeroMovement.Controller.XBOX:
                XBOXCorssHairs();
                break;
            case HeroMovement.Controller.Gamepad:
                PS4CorssHairs();
                break;
            default:
                break;
        }
    }

    void KeyboardCorssHairs()
    {
        _target = FindObjectOfType<Camera>().ScreenToWorldPoint(_heroAction.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
        _crossHairs.transform.position = new Vector3(_target.x, _target.y);
    }

    void PS4CorssHairs()
    {
        // Read it in once and cache instead of making multiple property access calls.
        var aimDirection = _heroAction.PlayerInput.PS4.Aim.ReadValue<Vector2>();
        //Debug.Log($"[INPUT] PS4 RAWINPUT X:{aimDirection.x} Y:{aimDirection.y}");

        // From what you have told me you want to keep the crosshairs active all the time. 
        if (aimDirection.x.Equals(0f) && aimDirection.y.Equals(0f))
        {
            // If the player isn't actively aiming we'll just exit and not update the position of the crosshairs.
            return;
        }

        // Normalize your direction input to get the unit vector in the direction the player is aiming.
        aimDirection.Normalize();
        var aimDirVec3 = new Vector3(aimDirection.x, aimDirection.y, 0.0f);
        //Debug.Log($"[INPUT] PS4 NORMALIZED X:{aimDirVec3.x} Y:{aimDirVec3.y}");

        // Now set the crosshairs position to be a scalar value away from the hero position in the direction the player is aiming.
        // This 5.5f value should be either set as a const define, or made as a [SerializeField] private float _crossHairDist; so that it can be tuned in the inspector.
        var crossHairPos = _heroAction.transform.position + (aimDirVec3 * 5.5f);
        _crossHairs.transform.position = new Vector3(crossHairPos.x, crossHairPos.y, crossHairPos.z);
        //Debug.Log($"[INPUT] PS4 FINAL X: {_crossHairs.transform.position.x} Y: {_crossHairs.transform.position.y}");
    }

    void XBOXCorssHairs()
    {
        var aimDirection = _heroAction.PlayerInput.XBOX.Aim.ReadValue<Vector2>();
        //Debug.Log($"[INPUT] PS4 RAWINPUT X:{aimDirection.x} Y:{aimDirection.y}");

        if (aimDirection.x.Equals(0f) && aimDirection.y.Equals(0f))
        {
            // If the player isn't actively aiming we'll just exit and not update the position of the crosshairs.
            return;
        }

        aimDirection.Normalize();
        var aimDirVec3 = new Vector3(aimDirection.x, aimDirection.y, 0.0f);
        //Debug.Log($"[INPUT] PS4 NORMALIZED X:{aimDirVec3.x} Y:{aimDirVec3.y}");

        var crossHairPos = _heroAction.transform.position + (aimDirVec3 * 5.5f);
        _crossHairs.transform.position = new Vector3(crossHairPos.x, crossHairPos.y, crossHairPos.z);
        //Debug.Log($"[INPUT] PS4 FINAL X: {_crossHairs.transform.position.x} Y: {_crossHairs.transform.position.y}");
    }
}
