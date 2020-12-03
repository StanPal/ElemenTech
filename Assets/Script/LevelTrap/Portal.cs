using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private Transform destination;
    public bool isOrange;

    private void Start()
    {
        if (isOrange == false)
        {
            destination = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("BulePortal").GetComponent<Transform>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > 0.3f)
        {
            collision.transform.position = new Vector3(destination.position.x, destination.position.y, destination.position.z);
            Debug.Log("move");
        }
    }
}
