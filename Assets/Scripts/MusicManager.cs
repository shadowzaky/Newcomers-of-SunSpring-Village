using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private void Awake()
    {
        instance = this;
    }
    public AudioSource audioSource;

    public AudioClip playOnStart;

    public float timeToSwitch = 0.5f;

    AudioClip switchTo;
    float volume;

    void Start()
    {
        Play(playOnStart, true);
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if (musicToPlay == null) { return; }
        if (interrupt)
        {
            audioSource.volume = 1f;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }
        else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }
    }
    
    IEnumerator SmoothSwitchMusic()
    {
        volume = 1f;
        while(volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if (volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }
}
