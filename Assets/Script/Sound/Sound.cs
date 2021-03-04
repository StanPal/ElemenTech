using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    public string Name { get { return _name; } }
    [SerializeField] private AudioClip _clip;
    public AudioClip Clip { get { return _clip; } }

    [Range(0.0f, 1.0f)]
    [SerializeField] private float _volume;
    public float Volume { get { return _volume; } }
    [Range(0.1f, 3.0f)]
    [SerializeField] private float _pitch;
    public float Pitch { get { return _pitch; }}
    [SerializeField] bool _loop;
    public bool Loop { get { return _loop; } }

    [HideInInspector]
    public AudioSource source;
}

