using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour
{
    private int dificultad;
    private bool pulsaRestart;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] private GameObject dialogoInicio;
    [SerializeField] private GameObject iniciacion;
    [SerializeField] private GameObject avanzado;
    [SerializeField] private GameObject escenario;
    // Start is called before the first frame update
 

    private void Awake()
    {
        dificultad = PlayerPrefs.GetInt("nivelJuego");
        pulsaRestart = PlayerPrefs.GetInt("pulsaRestart") == 1;
    }
    void Start()
    {
        if (pulsaRestart)
        {
            PlayerPrefs.SetInt("pulsaRestart", 0);
            iniciacion.GetComponent<Animator>().enabled = false;
            avanzado.GetComponent<Animator>().enabled = false;
            escenario.GetComponent<Animator>().enabled = false;
            ElegirNivel();
        }
        else
        {
            AudioManager.audioManagerInstance.PlayMusic("BGM_TDAH");
            iniciacion.GetComponent<Animator>().enabled = true;
            avanzado.GetComponent<Animator>().enabled = true;
            escenario.GetComponent<Animator>().enabled = true;
            Invoke("IniciarDialogo", 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("pulsaRestart", 1);
        SceneManager.LoadScene("PuzleEscape");
    }

    private void IniciarDialogo()
    {
            dialogoInicio.SetActive(true);
    }

    private void ElegirNivel()
    {
        escenario.SetActive(true);
        if (dificultad == 0)
        {
            iniciacion.SetActive(true);
        }
        else
            avanzado.SetActive(true);
    }

    private void DialogueManager_OnEndDialogue(object sender, int contador)
    {
        ElegirNivel();
    }

    public void OnEnable()
    {
        dialogueManager.OnEndDialogue += DialogueManager_OnEndDialogue;
    }

    public void OnDisable()
    {
        dialogueManager.OnEndDialogue -= DialogueManager_OnEndDialogue;
    }


}
