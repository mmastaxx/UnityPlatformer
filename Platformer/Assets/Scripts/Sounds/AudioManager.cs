
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
            sound.audioSource.clip =  sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;    
        }   
    }

    public void Play(string soundName) 
    {
        Sound s =  Array.Find(soundArray, sound => sound.name == soundName);
        if (s != null) 
            s.audioSource.Play();
        
    }
    public void StopAll()
    {
        foreach (var sound in soundArray)
        {
            sound.audioSource.Stop();   
        }
    }
    public void SetMusicVolume(float newVolume)
    {
        foreach (var sound in soundArray)
        {
            if (sound.type == Sound.Type.Music)
            {
                sound.audioSource.volume = newVolume;
            }
        }
    }
    public void SetEffectsVolume(float newVolume)
    {
        foreach (var sound in soundArray)
        {
            if (sound.type == Sound.Type.Effects)
            {
                sound.audioSource.volume = newVolume;
            }
        }
    }

}
