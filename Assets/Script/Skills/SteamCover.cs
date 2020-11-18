using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SteamCover : MonoBehaviour
{
    private ParticleSystem mSteamParticle;
    [SerializeField]
    private float mSteamDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        mSteamParticle = GetComponent<ParticleSystem>();
        StartCoroutine(SteamTimer());
    }
    
    IEnumerator SteamTimer()
    {
        mSteamParticle.Play();
        yield return new WaitForSeconds(mSteamDuration);
        mSteamParticle.Stop();
    }
}
