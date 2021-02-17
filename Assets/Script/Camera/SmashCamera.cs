using System.Collections.Generic;
using UnityEngine;

public class SmashCamera : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Camera _Camera;

    [SerializeField] private List<GameObject> Players;
    public FocusLevel _focusLevel;
    [SerializeField] private float _depthUpdateSpeed = 5f;
    [SerializeField] private float _angleUpdateSpeed = 7f;
    [SerializeField] private float _positionUpdateSpeed = 5f;
    [SerializeField] private float _depthMax = -10f;
    [SerializeField] private float _depthMin = -22f;
    [SerializeField] private float _angleMax = -11f;
    [SerializeField] private float _angleMin = 3f;
    private float CameraEulerX;
    private Vector3 _cameraPosition;
    private Vector3 _desiredPos;
    private Vector3 _targetLocalPos;

    private void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        for (int i = 0; i < _playerManager.mPlayersList.Count; i++)
        {
            if (_playerManager.mPlayersList[i].activeSelf)
            {
                Players.Add(_playerManager.mPlayersList[i]);
            }
        }
        _Camera = GetComponent<Camera>();
    }

    private void Start()
    {
        Players.Add(_focusLevel.gameObject);
    }

    private void LateUpdate()
    {
        if(Players.Count <= 0)
        {
            return;
        }
        CalculateCameraLocations();
        MoveCamera();
        //Move();
        //Zoom();
    }

    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if(position != _cameraPosition)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, _cameraPosition.x, _positionUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, _cameraPosition.y, _positionUpdateSpeed * Time.deltaTime);
            targetPosition.z = Mathf.MoveTowards(position.z, _cameraPosition.z, _depthUpdateSpeed * Time.deltaTime); //How quickly it moves left and right top or bottom
            gameObject.transform.position = targetPosition;
        }

        Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
        if(localEulerAngles.x != CameraEulerX)
        {
            Vector3 targetEulerAngles = new Vector3(CameraEulerX, localEulerAngles.y, localEulerAngles.z);
            gameObject.transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, _angleUpdateSpeed * Time.deltaTime);
        }
    }

    private void CalculateCameraLocations()
    {
        Vector3 averageCenter = Vector3.zero;
        Vector3 totalPositions = Vector3.zero;
        Bounds playerBounds = new Bounds();

        for(int i = 0; i < Players.Count; i++)
        {
            Vector3 playerPosition = Players[i].transform.position;
          
            if(!_focusLevel.FocusBounds.Contains(playerPosition))
            {
                float playerX = Mathf.Clamp(playerPosition.x, _focusLevel.FocusBounds.min.x, _focusLevel.FocusBounds.max.x);
                float playerY = Mathf.Clamp(playerPosition.y, _focusLevel.FocusBounds.min.y, _focusLevel.FocusBounds.max.y);
                float playerZ = Mathf.Clamp(playerPosition.z, _focusLevel.FocusBounds.min.z, _focusLevel.FocusBounds.max.z);
                playerPosition = new Vector3(playerX, playerY, playerZ);
            }
            totalPositions += playerPosition;
            playerBounds.Encapsulate(playerPosition);
        }

        averageCenter = (totalPositions / Players.Count);

        float extents = playerBounds.extents.x + playerBounds.extents.y;
        float lerpPercent = Mathf.InverseLerp(0, (_focusLevel.HalfXBounds + _focusLevel.HalfYBounds) / 2, extents);
        float depth = Mathf.Lerp(_depthMax, _depthMin, lerpPercent);
        float angle = Mathf.Lerp(_angleMax, _angleMin, lerpPercent);

        CameraEulerX = angle;
        _cameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
        
    }
}
