
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawtoothTrap : MonoBehaviour
{

    [SerializeField]
    private GameObject Saw;
    [SerializeField]
    private GameObject[] waypoints;
    private int current = 0;
    private float WPreadius = 1;
    [SerializeField]
    private float moveSpeed = 0;
    [SerializeField]
    private bool workWay = false;
    private bool isActive = false;
    int mTriggerId = 0;
    int count = 0;
    [SerializeField] private GameObject _spark;
    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = ServiceLocator.Get<SoundManager>();
    }

    public void Move(int TriggerId)
    {
        if (!isActive)
        {
            isActive = true;
            mTriggerId = TriggerId;

        }
    }

    void Update()
    {
        if (!workWay)
        {
            if (Vector3.Distance(waypoints[current].transform.position, Saw.transform.position) < WPreadius)
            {
                current++;
                if (current >= waypoints.Length)
                {
                    current = 0;
                }
            }
            Saw.transform.position = Vector3.MoveTowards(Saw.transform.position, waypoints[current].transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            if (isActive)
            {
                if (Saw.transform.position == waypoints[0].transform.position && count == 1)
                {
                    isActive = false;
                    _spark.GetComponentInChildren<ParticleSystem>().Stop();                    
                    count = 0;
                }

                if(Saw.transform.position == waypoints[mTriggerId].transform.position)
                {
                    count = 1;
                }

                if(count == 1)
                {
                    Saw.transform.position = Vector3.MoveTowards(Saw.transform.position, waypoints[0].transform.position, Time.deltaTime * moveSpeed);
                    _spark.transform.position = Vector3.MoveTowards(_spark.transform.position, 
                        new Vector3(Saw.transform.position.x, Saw.transform.position.y + 2.0f, -1f)
                        , Time.deltaTime * moveSpeed);
                    _spark.GetComponentInChildren<ParticleSystem>().Play();
                    AudioSource.PlayClipAtPoint(_soundManager.CombatSounds[1], this.transform.position, _soundManager.AudioVolume - 10.0f);
                }
                else
                {
                    Saw.transform.position = Vector3.MoveTowards(Saw.transform.position, waypoints[mTriggerId].transform.position, Time.deltaTime * moveSpeed);
                    _spark.transform.position = Vector3.MoveTowards(_spark.transform.position,
                        new Vector3(Saw.transform.position.x, Saw.transform.position.y + 2.0f, -1f)
                        , Time.deltaTime * moveSpeed);                    
                    AudioSource.PlayClipAtPoint(_soundManager.CombatSounds[1], this.transform.position, _soundManager.AudioVolume - 10.0f);
                    _spark.GetComponentInChildren<ParticleSystem>().Play();

                }
            }
        }

    }
}
