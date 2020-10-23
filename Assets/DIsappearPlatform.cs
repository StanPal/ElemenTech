using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIsappearPlatform : MonoBehaviour
{
    public float timeToTogglePlatform = 2;
    public float currentTime = 0;
    public bool enabled = true;
    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToTogglePlatform)
        {
            currentTime = 0;
            TogglePlatform();
        }

        void TogglePlatform()
        {
            enabled = !enabled;
           
            gameObject.SetActive(enabled);

            foreach (Transform main in gameObject.transform)
            {
                main.gameObject.SetActive(enabled);
            }

            //foreach(Transform child in gameObject.transform)
            //{
            //    child.gameObject.SetActive(enabled);
            //}
        }
    }
}
