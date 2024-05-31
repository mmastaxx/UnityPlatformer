using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundArray;
    private void Awake()
    {
        foreach (var sound in soundArray) 
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip=  sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;    
        }   
    }
    public void Play(string soundName) 
    {
        Sound s =  Array.Find(soundArray, sound => sound.name == soundName);
        s.audioSource.Play();
    }
    public void StopAll()
    {
        foreach (var sound in soundArray)
        {
            sound.audioSource.Stop();   
        }
    }

}
