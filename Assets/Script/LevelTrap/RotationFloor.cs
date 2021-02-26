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
    private bool _starRota = false;

    private void Update()
    {
        float eulerAngles = transform.rotation.eulerAngles.z;
        if (eulerAngles > 180f)
        {
            eulerAngles -= 360f;
        }

        // set rota clock or unclock
        if (_isRota && !_starRota)
        {
            if (!_isRotaBack)
            {
                _starRota = true;
                _floor.layer = 10;
                _floorLS.layer = 8;
                _floorRS.layer = 8;
            }
            else if(_currentdelayTime < Time.time && _isRotaBack)
            {
                _starRota = true;
                _floor.layer = 8;
                _floorLS.layer = 10;
                _floorRS.layer = 10;
            }
        }

        if (_starRota)
        {
            Rotafloor(_isRotaBack);
        }

        if (_starRota && eulerAngles < _starAngle )
        {
            _starRota = false;
            _isRota = false;
            _isRotaBack = false;
            transform.rotation.eulerAngles.Set(0, 0, _starAngle) ;
        }
        else if ( _starRota && _rotationAngle < eulerAngles)
        {
            _currentdelayTime = Time.time + _delayTime;
            _starRota = false;
            _isRotaBack = true;
            transform.rotation.eulerAngles.Set(0, 0, _rotationAngle);
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

    public void StartRota()
    {
        _isRota = true;
    }
}
