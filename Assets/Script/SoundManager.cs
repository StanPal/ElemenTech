using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip GameLoopSound,ElementSound,JumpSound,AtttackSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        GameLoopSound = Resources.Load<AudioClip>("Cronos");
        ElementSound = Resources.Load<AudioClip>("element");
        //JumpSound = Resources.Load<AudioClip>("");
        AtttackSound = Resources.Load<AudioClip>("attack");
        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            //use sound resources name in ""
            case "Cronos":
                audioSrc.PlayOneShot(GameLoopSound);
                break;
            case "element":
                audioSrc.PlayOneShot(ElementSound);
                break;
            case "attack":
                audioSrc.PlayOneShot(AtttackSound);
                break;
            //case "e":
            //    audioSrc.PlayOneShot(JumpSound);
            //    break;

        }
    }
}
