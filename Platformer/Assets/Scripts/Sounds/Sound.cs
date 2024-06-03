using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum Type
    {
        Music,Effects
    }
    public string name;
    public Type type;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource audioSource;
}
