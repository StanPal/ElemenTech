using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        var gamepad = Gamepad.current;
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;
        var allGamepads = Gamepad.all;

        foreach (Gamepad item in allGamepads)
        {
            Debug.Log(item.displayName);
        }        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
