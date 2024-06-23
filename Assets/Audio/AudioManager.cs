using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private SoundsRefsSO soundsRefsSO;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in soundsRefsSO.sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            sound.source.outputAudioMixerGroup = sound.mixerGroup;
        }
    }

    private Sound FindSound(string name, Sound[] sounds)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return null;
        }
        return sound;
    }

    private Sound FindSound(string name)
    {
        Sound sound = Array.Find(soundsRefsSO.sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return null;
        }
        return sound;
    }

    public void PlaySound(string name)
    {
        FindSound(name).source.Play();
    }

    public void StopSound(string name)
    {
        FindSound(name).source.Stop();
    }

    public void StopSounds()
    {
        foreach (Sound sound in soundsRefsSO.sounds)
        {
            sound.source.Stop();
        }
    }
    public void StopSounds(string[] exeptions)
    {
        foreach (Sound sound in soundsRefsSO.sounds)
        {
            foreach (string name in exeptions)
            {
                if (sound.name != name)
                {
                    sound.source.Stop();
                }
            }
        }
    }

    public void ChangeVolume(string mixerGroupName, float value)
    {
        value = value == 0 ? Mathf.Epsilon : value;

        audioMixer.SetFloat(mixerGroupName, Mathf.Log10(value) * 20);
    }

    public float ClipLength(string name)
    {
        return FindSound(name).clip.length;
    }
}
