using System.Collections;
using UnityEngine;

public class RotationFloor : MonoBehaviour
{
    [SerializeField] private GameObject _floor = null;
    [SerializeField] private GameObject _floorRS = null;
    [SerializeField] private GameObject _floorLS = null;
    [SerializeField] private float _delayTime = 5.0f;
    [SerializeField] private float _rotationSpeed = 0;
    [SerializeField] private float _finalZRotation = 0;
    [SerializeField] private float _startZRotation = 0;


    private Quaternion startRotation = new Quaternion(0f, 0f, 0f, 1f);
    private Quaternion endRotation = new Quaternion(0f, 0f, 90f, 1f);

    public enum RotationState
    {
        Uknown,
        Ready,
        Clockwise,
        Waiting,
        CounterClockwise,
        Finished,
    }
    private RotationState _state = RotationState.Uknown;

    private void Awake()
    {
        _state = RotationState.Ready;
        float zRotation = transform.rotation.eulerAngles.z;
        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }
    }

    public void StartRotating()
    {
        if (_state == RotationState.Ready)
        {
            Debug.Log("Starting Floor Rotation");
            StartCoroutine(RotationRoutine());
        }
    }

    private IEnumerator RotationRoutine()
    {
        _state = RotationState.Clockwise;
        while (_state != RotationState.Finished)
        {
            RotateFloor();
            UpdateState();
            if (_state == RotationState.Waiting)
            {
                yield return new WaitForSeconds(_delayTime);
                _state = RotationState.CounterClockwise;
            }
            yield return null;
        }
        Debug.Log("Finished Rotating Floor");
        _state = RotationState.Ready;
    }

    private void RotateFloor()
    {
        var refVector = _state == RotationState.Clockwise ? -Vector3.forward : Vector3.forward;
        transform.RotateAround(transform.position, refVector, _rotationSpeed * Time.deltaTime);
    }

    private void UpdateState()
    {
        float zRotation = transform.rotation.eulerAngles.z;
        switch (_state)
        {
            case RotationState.Clockwise:
                zRotation = Mathf.Abs(zRotation - 360f);
                if (zRotation > _finalZRotation)
                {
                    _state = RotationState.Waiting;
                    _floor.layer = 10;
                    _floorLS.layer = 8;
                    _floorRS.layer = 8;
                }
                break;
            case RotationState.CounterClockwise:
                if(transform.rotation.z < 0)
                {
                    zRotation -= 360f;
                }
                if (zRotation > _startZRotation)
                {
                    _state = RotationState.Finished;
                    _floor.layer = 8;
                    _floorLS.layer = 10;
                    _floorRS.layer = 10;
                }
                break;
        }
        Debug.Log($"Rotating Floor State: {_state}");
    }

}
