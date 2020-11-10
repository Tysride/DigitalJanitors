using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer MainVolume;
    public Slider slider;
    private DataManager dataManager;

    private void Start()
    {
        dataManager = DataManager.Instance;
        SetVolumeFromDataManager();

    }

    public void SetVolumeFromDataManager()
    {
        float volume = dataManager.gameVolume;
        MainVolume.SetFloat("Volume", volume);
        slider.value = volume;
    }

    public void SetVolumeFromSlider()
    {
        float volume = slider.value;
        dataManager.gameVolume = volume;
        MainVolume.SetFloat("Volume", volume);
    }
}
