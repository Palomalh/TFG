using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulsadorScript : MonoBehaviour
{
    //public GameObject personaje;
    private GameObject enemigo;
    [SerializeField] private GameObject helpDialogue;
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private GameObject explosionLiberar;
    [SerializeField] private GameObject indicaciones;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Contadores contadores;
    private float fillSpeed = 0.1f; // Velocidad de llenado cuando se presiona el botón
    private float decayRate = 0.15f; // Tasa de decaimiento constante de la barra
    public bool complete = false;
    public bool completeHelpDialogue = false;
    private Vector3 controlPoint2;
    private Vector3 midCharacterPosition;
    private Vector3 controlPoint1;
    private Vector3 endCharacterPosition;

    //[SerializeField] private GameObject nivel2;
    //[SerializeField] private GameObject[] enemigoslvl2;

    //[SerializeField] private GameObject nivel3;
    //[SerializeField] private GameObject[] enemigoslvl3;

    private void Start()
    {
        controlPoint2 = new Vector3(-154.1f, -0.11f, 20.13f);
        midCharacterPosition = new Vector3(-154.1f, -2.57f, 20.13f);
        
        complete = false;
    }
    private void Update()
    {
        // Siempre disminuye el progreso
        progressBar.value -= decayRate * Time.deltaTime;
        progressBar.value = Mathf.Max(progressBar.value, 0); // Evita que el progreso sea negativo

        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseProgress();
        }

        if (enemigo != null && enemigo.activeSelf)
        {
            CheckCompletion();
        }
    }

    void IncreaseProgress()
    {
        // Incrementa el progreso cuando el jugador pulsa el botón
        progressBar.value += fillSpeed;
        progressBar.value = Mathf.Min(progressBar.value, progressBar.maxValue); // Asegura que el progreso no exceda el máximo
    }

    public void CheckCompletion()
    {
        if (!complete && progressBar.value >= progressBar.maxValue)
        {
            AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_BotHelp");

            // Desactiva el Slider si el progreso es completo
            progressBar.gameObject.SetActive(false);
            indicaciones.SetActive(false);
            explosionLiberar.SetActive(true);
            _thirdPersonController.ChangeScaredState();
            _thirdPersonController.GetComponentInParent<CharacterController>().enabled = true;
            complete = true;
            enemigo.GetComponent<FollowScript>().GetOut(new Vector3(-2, 2, -1));
        }
    }
    public void CheckCompletionHelpDialogue()
    {
        if (!completeHelpDialogue && progressBar.value >= progressBar.maxValue)
        {
            AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_BotHelp");
            completeHelpDialogue = true;
            progressBar.value = 0;
            helpDialogue.GetComponent<HelpDialogue>().ayudaCompletada();
            TransportarJugador();
        }

    }
    private void TransportarJugador()
    {

        if (contadores.getEstrellasActuales() >= 2)
        {
            endCharacterPosition = new Vector3(-152.5f, 1.27f, 20.13f);
        }else
        {
            endCharacterPosition = new Vector3(-157.5f, 1.27f, 20.13f);
        }

            _thirdPersonController.GetComponentInParent<CharacterController>().enabled = false;
            controlPoint1 = _thirdPersonController.GetComponentInParent<Transform>().position;
            _thirdPersonController.GetComponentInParent<Transform>().LeanMove(new Vector3[] { controlPoint1, midCharacterPosition, controlPoint2, endCharacterPosition }, 4f).setOnComplete(() =>
            {
                _thirdPersonController.GetComponentInParent<CharacterController>().enabled = true;
                helpDialogue.GetComponent<BoxCollider>().enabled = true;
                completeHelpDialogue = false;
            });
    }

    public void SetEnemigo(GameObject _enemigo)
    {
        enemigo = _enemigo;
    }
}
