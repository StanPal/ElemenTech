using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class IceTrapManager : MonoBehaviour
{
    public List<Transform> iceSpikesLocation = new List<Transform>();
    private List<Transform> spawnLocation = new List<Transform>();
    public List<Transform> SpawnLocation { get { return spawnLocation; } }
    public GameObject iceSpike;

    [SerializeField]
    private float _DelayTime = 5.0f;
    private float _CurrentDelayTime = 0.0f;
    [SerializeField]
    private int _MaxSpawnlocation = 3;
    [SerializeField]
    private float _Damage = 50.0f;
    public float Damage { get { return _Damage; } }
    private int _IceSpikeCounter =0;

    public int IceSpikeCounter { set { _IceSpikeCounter = value; } get { return _IceSpikeCounter; } }
    private void Awake()
    {
        for (int i = 0; i < iceSpikesLocation.Count; i++)
        {
            spawnLocation.Add(iceSpikesLocation[i]);
        }
        for (int i = 0; i < _MaxSpawnlocation; ++i)
        {
            SpawnSpike();
        }
    }

    private void Update()
    {
        Debug.Log(spawnLocation.Count);
        if (_IceSpikeCounter < _MaxSpawnlocation && _CurrentDelayTime < Time.time)
        {
            _CurrentDelayTime = Time.time + _DelayTime;
            SpawnSpike();
        }
    }

    public void SpawnSpike()
    {
        Transform location = spawnLocation[Random.Range(0, spawnLocation.Count)];
        Instantiate(iceSpike, location.position, Quaternion.identity);
        spawnLocation.Remove(location);
        _IceSpikeCounter++;
    }

    public void addNewLocation(Transform newLocation)
    {
        for (int i = 0; i < iceSpikesLocation.Count; i++)
        {
            if (iceSpikesLocation[i].transform.position == newLocation.position)
            {
                spawnLocation.Add(iceSpikesLocation[i]);
            }
        }
    }

}
