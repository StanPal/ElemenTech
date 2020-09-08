using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConnectedControllers : MonoBehaviour
{

    public string[] controllers;

    void Update()
    {
        controllers = Input.GetJoystickNames();
        Debug.Log(controllers.ToString());
    }
}