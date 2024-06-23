using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private AudioSlider[] audioSliders;

    private void Start()
    {
        foreach (AudioSlider audioSlider in audioSliders)
        {
            audioSlider.audioMixerGroup.audioMixer.GetFloat(audioSlider.audioMixerGroup.name, out float value);

            audioSlider.slider.value = Mathf.Pow(10, (value / 20));
            audioSlider.slider.onValueChanged.AddListener(delegate { AudioManager.Instance.ChangeVolume(audioSlider.audioMixerGroup.name, audioSlider.slider.value); });
        }
    }
}

[System.Serializable]
public class AudioSlider
{
    public Slider slider;
    public AudioMixerGroup audioMixerGroup;
}
