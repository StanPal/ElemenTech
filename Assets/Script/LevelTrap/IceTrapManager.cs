using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class IceTrapManager : MonoBehaviour
{
    public List<Transform> iceSpikesLocation = new List<Transform>();
    public List<Transform> spawnLocation = new List<Transform>();
    public List<Transform> SpawnLocation { get { return spawnLocation; } }
    public List<Transform> spawnLocationNotUse = new List<Transform>();
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
        spawnLocationNotUse = iceSpikesLocation;
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
        Transform location = spawnLocationNotUse[Random.Range(0, spawnLocationNotUse.Count)];
        Instantiate(iceSpike, location.position, Quaternion.identity);
        spawnLocation.Add(location);
        spawnLocationNotUse.Remove(location);
        RecalculatePosition();
        iceSpikeCounter++;
    }

    public void RecalculatePosition()
    {
        spawnLocationNotUse = iceSpikesLocation;
        for (int i = 0; i < spawnLocationNotUse.Count; i++)
        {
            for (int j = 0; j < spawnLocation.Count; j++)
            {
                if (spawnLocationNotUse[i].position == spawnLocation[j].position)
                {
                    spawnLocationNotUse.Remove(spawnLocationNotUse[i]);
                }
            }
        }
    }
}
