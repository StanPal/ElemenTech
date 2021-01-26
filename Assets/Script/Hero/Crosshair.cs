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
        //mTarget = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>());
        switch (playerManager.mPlayersList[0].GetComponent<HeroMovement>().controllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _P1Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
                _P1CrossHairs.transform.position = new Vector3(_P1Target.x, _P1Target.y);
                break;
            case HeroMovement.Controller.PS4:
                _P1CrossHairs.transform.SetParent(playerManager.mPlayersList[0].transform);
                Debug.Log(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>());
                if (playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P1CrossHairs.SetActive(false);
                }
                else
                {
                    _P1CrossHairs.SetActive(true);
                }
                //Debug.Log(Screen.width * playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().x);
                _P1CrossHairs.transform.position = new Vector3(
                    playerManager.mPlayersList[0].transform.position.x + (playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().x * 5.5f),
                    playerManager.mPlayersList[0].transform.position.y + playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.XBOX:
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }

        switch (playerManager.mPlayersList[1].GetComponent<HeroMovement>().controllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _P2Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
                _P2CrossHairs.transform.position = new Vector3(_P2Target.x, _P2Target.y);
                break;
            case HeroMovement.Controller.PS4:
                _P2CrossHairs.transform.SetParent(playerManager.mPlayersList[1].transform);
                Debug.Log(playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>());
                if (playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P2CrossHairs.SetActive(false);
                }
                else
                {
                    _P2CrossHairs.SetActive(true);
                }
                _P2CrossHairs.transform.position = new Vector3(
                                  playerManager.mPlayersList[1].transform.position.x + (playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().x * 5.5f),
                                  playerManager.mPlayersList[1].transform.position.y + playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.XBOX:
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }

        if (playerManager.mPlayersList[2].GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
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
