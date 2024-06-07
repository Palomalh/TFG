using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDeJuego : MonoBehaviour
{
	public void RestartGame(string nombreJuego)
	{
		AudioManager.audioManagerInstance.PlaySFX("Click");
		SceneManager.LoadScene(nombreJuego);
	}

	public void VolverAlMenu()
    {
		AudioManager.audioManagerInstance.PlaySFX("Click");

		SceneManager.LoadScene("GamesMenu");
    }

}
