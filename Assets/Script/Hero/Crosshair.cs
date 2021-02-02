using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject P1CrossHairs;
    public GameObject P2CrossHairs;
    public GameObject P3CrossHairs;
    public GameObject P4CrossHairs;

    [SerializeField] private Vector3 _P1Target;
    [SerializeField] private Vector3 _P2Target;
    [SerializeField] private Vector3 _P3Target;
    [SerializeField] private Vector3 _P4Target;

    private PlayerManager _PlayerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _PlayerManager = FindObjectOfType<PlayerManager>();
        Cursor.visible = false;
    }

    void Update()
    {
        //mTarget = transform.GetComponent<Camera>().ScreenToWorldPoint(_PlayerManager.PlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>());
        if (_PlayerManager.PlayersList[0].GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            _P1Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_PlayerManager.PlayersList[0].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            P1CrossHairs.transform.position = new Vector3(_P1Target.x, _P1Target.y);
        }
        if (_PlayerManager.PlayersList[1].GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            _P2Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_PlayerManager.PlayersList[1].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            P2CrossHairs.transform.position = new Vector3(_P2Target.x, _P2Target.y);
        }
        if (_PlayerManager.PlayersList[2].GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            _P3Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_PlayerManager.PlayersList[2].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            P3CrossHairs.transform.position = new Vector3(_P3Target.x, _P3Target.y);
        }
        if (_PlayerManager.PlayersList[3].GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            _P4Target = transform.GetComponent<Camera>().ScreenToWorldPoint(_PlayerManager.PlayersList[3].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            P4CrossHairs.transform.position = new Vector3(_P4Target.x, _P4Target.y);
        }
    }
}
