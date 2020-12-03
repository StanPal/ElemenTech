using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class DIsappearPlatform : MonoBehaviour
{
    public float disappearDelay = 2.0f;
    public float reappearDelay = 4.0f;
    public bool enabled = true;

    void Start()
    {
        enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HeroStats>() != null)
        {
            gameObject.SetActive(enabled);
            Invoke("Disappear", disappearDelay);
        }
    }
   

    private void Disappear()
    {
        
        
        gameObject.SetActive(false);

        Invoke("Reappear", reappearDelay);
    }

    private void Reappear()
    {
       
       
            gameObject.SetActive(true);
    }
}
    


   

