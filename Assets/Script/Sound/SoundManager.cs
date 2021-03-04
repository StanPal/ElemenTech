using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public Sound[] _sounds;
    public string[] _playMusic;
    public string[] _playMusicLoopBetweenATime;
    public static SoundManager _instance;
    public float _speedOfChangeFocus = 0.0f;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in _sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.Loop;
        }
    }

    private void Start()
    {
        foreach (var name in _playMusic)
        {
            Play(name);
        }
        foreach (var name in _playMusicLoopBetweenATime)
        {
            StartCoroutine(PlayLoop(name));
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.Name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    IEnumerator PlayLoop(string name)
    {
        while (true)
        {
            Sound s = Array.Find(_sounds, sound => sound.Name == name);
            if (s == null)
            {
                break;
            }

            yield return new WaitUntil(() => s.source.isPlaying == false);
            yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));
            s.source.Play();
        }
    }
}

