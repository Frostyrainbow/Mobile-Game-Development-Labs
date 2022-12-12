using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> channels;
    private List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Awake()
    {
        channels = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        InitalizeSoundFX();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitalizeSoundFX()
    {
        audioClips.Add(Resources.Load<AudioClip>("Audio/Jump"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/TookDamage"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Death"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/LevelMusic"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Gameover"));
    }

    public void PlaySoundFX(SoundEffects sound, Channel channel)
    {
        channels[(int)channel].clip = audioClips[(int)sound];
        channels[(int)channel].Play();
    }

    public void PlayMusic(SoundEffects sound)
    {
        channels[(int)Channel.MUSIC].clip = audioClips[(int)sound];
        channels[(int)Channel.MUSIC].volume = 0.25f;
        channels[(int)Channel.MUSIC].loop = true;
        channels[(int)Channel.MUSIC].Play();
    }
}
