using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject mCrossHairs;
    [SerializeField]
    private Vector3 mTarget;
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
        if (playerManager.mPlayersList[0].gameObject != null)
        {
            mTarget = transform.GetComponent<Camera>().ScreenToWorldPoint(playerManager.mPlayersList[0].GetComponent<HeroActions>().PlayerInput.KeyboardMouse.Aim.ReadValue<Vector2>());
            mCrossHairs.transform.position = new Vector3(mTarget.x, mTarget.y);
        }
    }
}
