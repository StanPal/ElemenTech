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
        Cursor.visible = false;
    }

    void Update()
    {
        //mTarget = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.PS4.Aim.ReadValue<Vector2>());
        if (playerManager.mPlayersList[0].GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            _P1Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            _P1CrossHairs.transform.position = new Vector3(_P1Target.x, _P1Target.y);
        }
        if (playerManager.mPlayersList[1].GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            _P2Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[1].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            _P2CrossHairs.transform.position = new Vector3(_P2Target.x, _P2Target.y);
        }
        if (playerManager.mPlayersList[2].GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            _P3Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[2].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            _P3CrossHairs.transform.position = new Vector3(_P3Target.x, _P3Target.y);
        }
        if (playerManager.mPlayersList[3].GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            _P4Target = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[3].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            _P4CrossHairs.transform.position = new Vector3(_P4Target.x, _P4Target.y);
        }
    }
}
