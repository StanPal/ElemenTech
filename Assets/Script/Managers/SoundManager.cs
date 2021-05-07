using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musicFiles;
    [SerializeField] private AudioClip[] _combatSounds;
    [SerializeField] private float _volume = 0.5f;
    private AudioSource _audioSource;
    public AudioClip[] CombatSounds { get => _combatSounds; }
    public float AudioVolume { get => _volume; set => _volume = value; }

    private void Awake()
    {
        ServiceLocator.Register<SoundManager>(this);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (_audioSource != null)
        {
      
        }
    }    

    public AudioSource PlayMusic(int trackIndex)
    {
        _audioSource.clip = _musicFiles[trackIndex];
        return _audioSource;

    }

    public AudioSource PlayCombatSounds(int soundIndex)
    {
        _audioSource.clip = _combatSounds[soundIndex];
        return _audioSource;
    }
}
