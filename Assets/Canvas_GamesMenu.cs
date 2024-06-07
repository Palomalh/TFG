using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_GamesMenu : MonoBehaviour
{
    [SerializeField]
        GameObject pantalla_seleccion;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        pantalla_seleccion.SetActive(true);
        AudioManager.audioManagerInstance.PlayMusic("Theme");

        musicSlider.value = PlayerPrefs.GetFloat("volumeMusic");
        sfxSlider.value = PlayerPrefs.GetFloat("volumeSfx");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
