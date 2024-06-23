using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [Header("SETTINGS")]
    public string name;
    [Range(.001f, 1f)] public float volume = 0.5f;
    [Range(.1f, 3f)] public float pitch = 1f;
    public bool loop;
    [Header("REFERENCES")]
    public AudioMixerGroup mixerGroup;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
}