using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIsappearPlatform : MonoBehaviour
{
    public GameObject Player;
    public float timeToTogglePlatform = 2;
    public float currentTime = 0;
    public bool enabled = true;
    void Start()
    {
        enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {

        currentTime += Time.deltaTime;
        if (currentTime >= timeToTogglePlatform)
        {
            currentTime = 0;
            TogglePlatform();
        }
        enabled = !enabled;

        gameObject.SetActive(enabled);

        foreach (Transform main in gameObject.transform)
        {
            main.gameObject.SetActive(enabled);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        enabled = true;
    }
    void TogglePlatform()
    {
        enabled = !enabled;

        //gameObject.SetActive(enabled);

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(enabled);

        }
    }
}

    //    void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    if (currentTime >= timeToTogglePlatform)
    //    {
    //        currentTime = 0;
    //        TogglePlatform();
    //    }

       
            //foreach (Transform child in gameObject.transform)
            //{
            //    child.gameObject.SetActive(enabled);
            //}
    //    }
    //}


