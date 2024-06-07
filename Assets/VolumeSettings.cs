using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("volumeMusic");
        sfxSlider.value = PlayerPrefs.GetFloat("volumeSfx");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MuteSounds()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        AudioManager.audioManagerInstance.sfxSource.mute = !AudioManager.audioManagerInstance.sfxSource.mute;
        AudioManager.audioManagerInstance.musicSource.mute = !AudioManager.audioManagerInstance.musicSource.mute;
    }

    public void ChangeVolumeMusic()
    {

        PlayerPrefs.SetFloat("volumeMusic", musicSlider.value);
        AudioManager.audioManagerInstance.musicSource.volume = musicSlider.value;
    }


    public void ChangeVolumeSFX()
    {

        PlayerPrefs.SetFloat("volumeSfx", sfxSlider.value);
        AudioManager.audioManagerInstance.sfxSource.volume = sfxSlider.value;
    }
}
