using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public bool pausedDialogue;
    [SerializeField] GameObject pauseButton;
   [SerializeField ]GameObject pauseMenuUI;
   [SerializeField ]GameObject pauseMenuBG;
   [SerializeField ]GameObject settingsMenuBG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");

        pauseButton.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pausedDialogue = true;
        gameIsPaused = true;

    }

    public void Resume()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");

        pauseButton.SetActive(true);
        pauseMenuUI.SetActive(false);
        pauseMenuBG.SetActive(true);
        settingsMenuBG.SetActive(false);

        Time.timeScale = 1f;
        pausedDialogue = false;

        gameIsPaused = false;

    }

    public void LoadGamesMenu()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        Time.timeScale = 1f;
        pausedDialogue = false;

        gameIsPaused = false;
        SceneManager.LoadScene("GamesMenu");
    }

    public void SettingsScreen()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        pauseMenuBG.SetActive(false);
        settingsMenuBG.SetActive(true);

    }

    public void VolverButton()
    {
        AudioManager.audioManagerInstance.PlaySFX("Click");
        pauseMenuBG.SetActive(true);
        settingsMenuBG.SetActive(false);
    }

    public void ReanudarButton()
    {
        pauseButton.SetActive(true);
        AudioManager.audioManagerInstance.PlaySFX("Click");

        Time.timeScale = 1f;

        pausedDialogue = false;
        gameIsPaused = false;

        pauseMenuUI.SetActive(false);
    }
}
