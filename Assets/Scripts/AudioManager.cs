using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager audioManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        sounds[1].source.Stop();
        audioManagerInstance.Stop("Swing");
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.25f);
    }
    private void Awake()
    {
        audioManagerInstance = this;
        InitializeSounds();
    }
    void InitializeSounds()
    {
        foreach (Sound s in sounds)
        {
            if (!s.source)
            {
                s.source = gameObject.AddComponent<AudioSource>();
            }
            s.source.clip = s.clip;
            s.source.playOnAwake = s.playOnAwake;
            if (s.playOnAwake)
            {
                s.source.Play();
            }
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) 
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sounds[0] != null && sounds[1] != null)
            {
                sounds[1].source.Play();
                sounds[0].source.Stop();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sounds[0] != null && sounds[1] != null)
            {
                sounds[0].source.Play();
                sounds[1].source.Stop();
            }
        }
    }
}
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0,1)]
    public float volume = 0.5f;
    [Range(0, 3)]
    public float pitch = 1f;
    public bool loop;
    public bool playOnAwake = false;
    public AudioSource source;
    public Sound()
    {
        volume = 0.5f;
        pitch = 1f;
        loop = false;
    }
}
