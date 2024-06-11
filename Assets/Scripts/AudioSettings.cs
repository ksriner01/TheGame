using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;

    public void SetVolume()
    {
        float volume = audioSlider.value;
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }
}
