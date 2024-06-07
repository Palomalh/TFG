using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaSeleccion : MonoBehaviour
{
    [Header("-------------Pantallas-------------")]
    [SerializeField] private Animator pantallaSeleccion_animator;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject exitScreen;

    [Header("-------------Botones-------------")]
    [SerializeField] private Sprite settingsIcon;
    [SerializeField] private Sprite creditsIcon;
    [SerializeField] private Sprite arrowBackIcon;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject creditsButton;


    [Header ("-------------TutoScreen-------------")]
    [SerializeField] private GameObject tutoScreen;
    [SerializeField] private GameObject tutoIconoContenedor;
    [SerializeField] private TMP_Text tutoTituloContenedor;
    [SerializeField] private TMP_Text tutoDescripcionContenedor;
    [SerializeField] private Sprite[] iconosJuegos;
    [SerializeField] private string[] titulosJuegos;
    [SerializeField] private string[] descripcionesJuegos;
    [SerializeField] private GameObject botonesDificultades;
    [SerializeField] private GameObject botonComenzar;
    private string nombreJuego;

    private void Start()
    {
        //musicSlider.value = PlayerPrefs.GetFloat("volumeMusic");
        //sfxSlider.value = PlayerPrefs.GetFloat("volumeSfx");
    }


    public void ElegirJuego(string nombre)
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        tutoScreen.SetActive(true);

        if (nombre.Equals("Cronometro"))
        {
            nombreJuego = "Cronometro";
            tutoIconoContenedor.GetComponent<Image>().sprite = iconosJuegos[0];
            tutoTituloContenedor.SetText(titulosJuegos[0]);
            //Debug.Log(descripcionesJuegos[0]);

            tutoDescripcionContenedor.SetText(descripcionesJuegos[0]);
            botonesDificultades.SetActive(true);
            botonComenzar.SetActive(false);


        }
        else
        if (nombre.Equals("DepresionPlataformas"))
        {
            nombreJuego = "DepresionPlataformas";
            tutoIconoContenedor.GetComponent<Image>().sprite = iconosJuegos[1];
            tutoTituloContenedor.SetText(titulosJuegos[1]);
            //Debug.Log(descripcionesJuegos[1]);

            tutoDescripcionContenedor.SetText(descripcionesJuegos[1]);

            botonesDificultades.SetActive(false);
            botonComenzar.SetActive(true);

        }
        else
            if (nombre.Equals("PuzleEscape"))
            {

            nombreJuego = "PuzleEscape";
                tutoIconoContenedor.GetComponent<Image>().sprite = iconosJuegos[2];
                tutoTituloContenedor.SetText(titulosJuegos[2]);
            //Debug.Log(descripcionesJuegos[2]);

            tutoDescripcionContenedor.SetText(descripcionesJuegos[2]);
            botonesDificultades.SetActive(true);
            botonComenzar.SetActive(false);
        }

    }

    public void ElegirNivel(int nivel)
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        PlayerPrefs.SetInt("nivelJuego", nivel);
        //if (nombreJuego.Equals("PuzleEscape")) AudioManager.audioManagerInstance.PlaySFX("Click");
        SceneManager.LoadScene(nombreJuego);
            

    }
    public void IniciarEscena()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        SceneManager.LoadScene(nombreJuego);

    }

    public void Settings()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        pantallaSeleccion_animator.SetBool("fadeOut_ClickCredits", false);

        if (pantallaSeleccion_animator.GetBool("fadeOutSelectorNiveles") == false && pantallaSeleccion_animator.GetBool("fadeOut_ClickCredits") == false)
        {

        pantallaSeleccion_animator.SetBool("fadeOutSelectorNiveles", true);
        GameObject.Find("SettingsIcon").GetComponent<Image>().sprite = arrowBackIcon;

        settingsScreen.SetActive(true);
        creditsButton.GetComponent<Button>().enabled = false;

        }
        else
        {
            pantallaSeleccion_animator.SetBool("fadeOutSelectorNiveles", false);
            GameObject.Find("SettingsIcon").GetComponent<Image>().sprite = settingsIcon;
            settingsScreen.SetActive(false);
            creditsButton.GetComponent<Button>().enabled = true;

        }

    }

    public void Credits()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        if (pantallaSeleccion_animator.GetBool("fadeOut_ClickCredits") == false && pantallaSeleccion_animator.GetBool("fadeOutSelectorNiveles") == false)
        {

            pantallaSeleccion_animator.SetBool("fadeOut_ClickCredits", true);
            creditsScreen.SetActive(true);
            settingsButton.GetComponent<Button>().enabled = false;
            GameObject.Find("CreditsIcon").GetComponent<Image>().sprite = arrowBackIcon;

            
        }
        else
        {
            pantallaSeleccion_animator.SetBool("fadeOut_ClickCredits", false);
            GameObject.Find("CreditsIcon").GetComponent<Image>().sprite = creditsIcon;
            creditsScreen.SetActive(false);
            settingsButton.GetComponent<Button>().enabled = true;

        }

    }

    public void ExitScreen()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        exitScreen.SetActive(true);
    }

    public void CloseExitScreen()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        exitScreen.SetActive(false);
    }

    public void CloseGame()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        Application.Quit();
    }

    public void CrossClose()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        botonesDificultades.SetActive(false);
        botonComenzar.SetActive(false);
        tutoScreen.SetActive(false);
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
        Debug.Log("volumen musica " + PlayerPrefs.GetFloat("volumeMusic"));
        AudioManager.audioManagerInstance.musicSource.volume = musicSlider.value;
    }


    public void ChangeVolumeSFX()
    {
        PlayerPrefs.SetFloat("volumeSfx", sfxSlider.value);
        Debug.Log("volumen musica " + PlayerPrefs.GetFloat("volumeSfx"));
        AudioManager.audioManagerInstance.sfxSource.volume = sfxSlider.value;
    }

}
