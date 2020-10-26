using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrapManager : MonoBehaviour
{
    public List<Transform> iceSpikesLocation = new List<Transform>();
    public GameObject iceSpike;
    [SerializeField]
    float damage = 50.0f;
    public float Damage { get { return damage; } }

    private void Awake()
    {
        for (int i = 0; i < iceSpikesLocation.Count; ++i)
        {
            Instantiate(iceSpike, iceSpikesLocation[i].position, Quaternion.identity);
        }
    }
}
