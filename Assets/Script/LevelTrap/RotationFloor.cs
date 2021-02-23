using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFloor : MonoBehaviour
{
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _floorRS;
    [SerializeField] private GameObject _floorLS;

    [SerializeField] private float _delayTime = 5.0f;
    private float _currentdelayTime = 0.0f;

    [SerializeField] private float _rotaSpeed = 0;
    [SerializeField] private float _rotationAngle = 0;
    [SerializeField] private float _starAngle= 0;

    private bool _isRota = false;
    private bool _isRotaBack = false;


    private void Update()
    {
        if (_currentdelayTime < Time.deltaTime && transform.rotation.eulerAngles.z <= _starAngle)
        {
            _isRota = true;
            _isRotaBack = false;
        }
        else if(_currentdelayTime < Time.deltaTime && transform.rotation.eulerAngles.z >= _rotationAngle)
        {
            _isRota = true;
            _isRotaBack = true;
        }

        if (_isRota)
        {
            Rotafloor(_isRotaBack);
        }

        if (_isRota && transform.rotation.z <= _starAngle || _isRota && transform.rotation.eulerAngles.z >= _rotationAngle)
        {
            _isRota = false;
            _currentdelayTime = Time.deltaTime + _delayTime;
        }
    }

    private void Rotafloor(bool isRotaBack)
    {
        if (isRotaBack)
        {
            transform.RotateAround(transform.position, -Vector3.forward, _rotaSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(transform.position, Vector3.forward, _rotaSpeed * Time.deltaTime);

        }
    }
}
