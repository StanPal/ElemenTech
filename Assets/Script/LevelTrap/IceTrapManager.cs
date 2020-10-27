using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrapManager : MonoBehaviour
{
    public List<Transform> iceSpikesLocation = new List<Transform>();
    List<Transform> randomNum = new List<Transform>();
    public List<Transform> RandomNum { get { return randomNum; } }

    public GameObject iceSpike;

    [SerializeField]
    float delayTime = 5.0f;
    float currentDelayTime = 0.0f;
    System.Random random = new System.Random();
    [SerializeField]
    int maxSpawnlocation = 3;
    [SerializeField]
    float damage = 50.0f;
    public float Damage { get { return damage; } }
    int iceSpikeCounter =0;

    public int IceSpikeCounter { set { iceSpikeCounter = value; } get { return iceSpikeCounter; } }
    private void Awake()
    {
        randomNum = iceSpikesLocation;
        
        for (int i = 0; i < maxSpawnlocation; ++i)
        {
            spawnSpike();
        }
    }

    private void Update()
    {
        if (iceSpikeCounter < maxSpawnlocation && currentDelayTime < Time.time)
        {
            currentDelayTime = Time.time + delayTime;
            spawnSpike();
        }
    }

    void spawnSpike()
    {
        Transform location = randomNum[random.Next(1, randomNum.Count)];
        Instantiate(iceSpike, location.position, Quaternion.identity);
        randomNum.Remove(location);
        iceSpikeCounter++;
    }
}
