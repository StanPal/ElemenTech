using UnityEngine;

public class Crosshair : MonoBehaviour
{
    //public GameObject _crossHairs;

    private Vector3 _target;

    private HeroActions _heroAction = null;
    private HeroMovement _heroMovement = null;

    private void Awake()
    {
        _heroAction = GetComponentInParent<HeroActions>();
        _heroMovement = GetComponentInParent<HeroMovement>();
        Cursor.visible = false;
    }

    void Update()
    {
        switch (_heroMovement.ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                KeyboardCrossHairs();
                break;
            case HeroMovement.Controller.PS4:
                PS4CrossHairs();
                break;
            case HeroMovement.Controller.XBOX:
                XBOXCrossHairs();
                break;
            case HeroMovement.Controller.Gamepad:
                PS4CrossHairs();
                break;
            default:
                break;
        }
    }

    void KeyboardCrossHairs()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay((Vector3)_heroAction.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 50, Color.yellow);
        //float midPoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
        _target = new Vector3(mouseRay.origin.x + mouseRay.direction.x * 55, mouseRay.origin.y + mouseRay.direction.y * 55, 0);
        transform.position = _target;
        //transform.position.Normalize();
       // _target = Camera.main.ScreenToWorldPoint((Vector3)_heroAction.PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
        //transform.position = new Vector3(_target.x, _target.y, 0);
        //_crossHairs.transform.position = new Vector3(_target.x, _target.y) * Mathf.Rad2Deg;
    }

    void PS4CrossHairs()
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
        transform.position = new Vector3(crossHairPos.x, crossHairPos.y, crossHairPos.z);
        //Debug.Log($"[INPUT] PS4 FINAL X: {_crossHairs.transform.position.x} Y: {_crossHairs.transform.position.y}");
    }

    void XBOXCrossHairs()
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
        transform.position = new Vector3(crossHairPos.x, crossHairPos.y, crossHairPos.z);
        //Debug.Log($"[INPUT] PS4 FINAL X: {_crossHairs.transform.position.x} Y: {_crossHairs.transform.position.y}");
    }

}
