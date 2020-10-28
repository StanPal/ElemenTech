using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class IceTrapManager : MonoBehaviour
{
    public List<Transform> iceSpikesLocation = new List<Transform>();
    List<Transform> spawnLocation = new List<Transform>();
    public List<Transform> SpawnLocation { get { return spawnLocation; } }

    public GameObject iceSpike;

    [SerializeField]
    float delayTime = 5.0f;
    float currentDelayTime = 0.0f;
    [SerializeField]
    int maxSpawnlocation = 3;
    [SerializeField]
    float damage = 50.0f;
    public float Damage { get { return damage; } }
    int iceSpikeCounter =0;

    public int IceSpikeCounter { set { iceSpikeCounter = value; } get { return iceSpikeCounter; } }
    private void Awake()
    {
        spawnLocation = iceSpikesLocation;
        
        for (int i = 0; i < maxSpawnlocation; ++i)
        {
            SpawnSpike();
        }
    }

    private void Update()
    {
        if (iceSpikeCounter < maxSpawnlocation && currentDelayTime < Time.time)
        {
            currentDelayTime = Time.time + delayTime;
            SpawnSpike();
        }
    }

    public void SpawnSpike()
    {
        Transform location = spawnLocation[Random.Range(0, spawnLocation.Count)];
        Instantiate(iceSpike, location.position, Quaternion.identity);
        spawnLocation.Remove(location);
        iceSpikeCounter++;
    }
}
