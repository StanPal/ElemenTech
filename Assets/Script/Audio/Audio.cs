using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private SoundManager _soundManager;
    [SerializeField] private int _TrackIndex;
    private AudioSource _audioSource;

    private void Awake()
    {
        _soundManager = ServiceLocator.Get<SoundManager>();
        _audioSource.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeAudioSource.StartFade(_audioSource, 1f, 0.5f));
    }

}
