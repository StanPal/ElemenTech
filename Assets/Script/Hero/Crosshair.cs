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

    private PlayerManager playerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        if(playerManager.mPlayersList.Count == 1)
        {
            _P1CrossHairs.SetActive(true);
        }        
        if (playerManager.mPlayersList.Count == 2)
        {
            _P1CrossHairs.SetActive(true);
            _P2CrossHairs.SetActive(true);
        }        
        if (playerManager.mPlayersList.Count == 3)
        {
            _P1CrossHairs.SetActive(true);
            _P2CrossHairs.SetActive(true);
            _P3CrossHairs.SetActive(true);
        }        
        if (playerManager.mPlayersList.Count == 4)
        {
            _P1CrossHairs.SetActive(true);
            _P2CrossHairs.SetActive(true);
            _P3CrossHairs.SetActive(true);
            _P4CrossHairs.SetActive(true);
        }        
        Cursor.visible = false;
    }

    void Update()
    {
        //mTarget = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>());
        switch (playerManager.mPlayersList[0].GetComponent<HeroMovement>().ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _P1Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
                _P1CrossHairs.transform.position = new Vector3(_P1Target.x, _P1Target.y);
                break;
            case HeroMovement.Controller.PS4:
                _P1CrossHairs.transform.SetParent(playerManager.mPlayersList[0].transform);
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
                _P1CrossHairs.transform.SetParent(playerManager.mPlayersList[0].transform);
                if (playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P1CrossHairs.SetActive(false);
                }
                else
                {
                    _P1CrossHairs.SetActive(true);
                }
                _P1CrossHairs.transform.position = new Vector3(
                    playerManager.mPlayersList[0].transform.position.x + (playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().x * 5.5f),
                    playerManager.mPlayersList[0].transform.position.y + playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.Gamepad:
                _P1CrossHairs.transform.SetParent(playerManager.mPlayersList[0].transform);
                if (playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P1CrossHairs.SetActive(false);
                }
                else
                {
                    _P1CrossHairs.SetActive(true);
                }
                _P1CrossHairs.transform.position = new Vector3(
                    playerManager.mPlayersList[0].transform.position.x + (playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x * 5.5f),
                    playerManager.mPlayersList[0].transform.position.y + playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y * 5.5f);
                break;

            default:
                break;
        }

        switch (playerManager.mPlayersList[1].GetComponent<HeroMovement>().ControllerInput)
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
                _P2CrossHairs.transform.SetParent(playerManager.mPlayersList[1].transform);
                Debug.Log(playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>());
                if (playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P2CrossHairs.SetActive(false);
                }
                else
                {
                    _P2CrossHairs.SetActive(true);
                }
                _P2CrossHairs.transform.position = new Vector3(
                                  playerManager.mPlayersList[1].transform.position.x + (playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().x * 5.5f),
                                  playerManager.mPlayersList[1].transform.position.y + playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.XBOX.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.Gamepad:
                _P2CrossHairs.transform.SetParent(playerManager.mPlayersList[1].transform);
                Debug.Log(playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>());
                if (playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P2CrossHairs.SetActive(false);
                }
                else
                {
                    _P2CrossHairs.SetActive(true);
                }
                _P2CrossHairs.transform.position = new Vector3(
                                  playerManager.mPlayersList[1].transform.position.x + (playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().x * 5.5f),
                                  playerManager.mPlayersList[1].transform.position.y + playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.Gamepad.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }


        switch (playerManager.mPlayersList[2].GetComponent<HeroMovement>().ControllerInput)
        {
            case HeroMovement.Controller.None:
                break;
            case HeroMovement.Controller.Keyboard:
                _P3Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[2].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
                _P3CrossHairs.transform.position = new Vector3(_P3Target.x, _P3Target.y);
                break;
            case HeroMovement.Controller.PS4:
                _P3CrossHairs.transform.SetParent(playerManager.mPlayersList[2].transform);
                Debug.Log(playerManager.mPlayersList[2].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>());
                if (playerManager.mPlayersList[2].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().x.Equals(0f) &&
                    playerManager.mPlayersList[2].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().y.Equals(0f))
                {
                    _P3CrossHairs.SetActive(false);
                }
                else
                {
                    _P3CrossHairs.SetActive(true);
                }
                _P3CrossHairs.transform.position = new Vector3(
                                  playerManager.mPlayersList[2].transform.position.x + (playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().x * 5.5f),
                                  playerManager.mPlayersList[2].transform.position.y + playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>().y * 5.5f);
                break;
            case HeroMovement.Controller.XBOX:
                break;
            case HeroMovement.Controller.Keyboard2:
                break;
            default:
                break;
        }

        if (playerManager.mPlayersList[3].GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            _P4Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[3].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            _P4CrossHairs.transform.position = new Vector3(_P4Target.x, _P4Target.y);
        }
    }
}
