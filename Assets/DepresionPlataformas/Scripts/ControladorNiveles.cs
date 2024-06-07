using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorNiveles : MonoBehaviour
{
    public Camera mainCamera;
    public Camera[] keyCameras;
    public float delay = 2f;

    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject[] dialogos;
    [SerializeField] private GameObject dialogoPensamiento;
    private bool primerDialogo;
    private bool pensamientoDialogo;

    [SerializeField] Contadores contadores;
    [SerializeField] GameObject[] llaves;


    [SerializeField] private GameObject player;
    [SerializeField] ThirdPersonController _thirdPersonController;
    private bool tocaSuelo;

    [SerializeField] private GameObject[] mapasNiveles;
    private int nivelActual;

    
    [SerializeField] private GameObject[] enemigos;

    [SerializeField] private Vector3 posActualSpawn;
    private Vector3 posNuevoSpawn;

    [SerializeField] private BoxCollider[] colliders;

    [SerializeField] private GameObject FinJuegoScreen;
    [SerializeField] private GameObject FinJuegoPanel;
    [SerializeField] private TMP_Text monedasTexto;




    //[SerializeField]private GameObject
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.audioManagerInstance.PlayMusic("BGM_Depression");
        posActualSpawn = new Vector3(0f, 16.7f, 0f);

        //_thirdPersonController = GetComponent<ThirdPersonController>();
        pensamientoDialogo = false;
        nivelActual = 0;
        primerDialogo = true;
        tocaSuelo = false;
        player.GetComponent<ThirdPersonController>().MoveSpeed = 0;
        colliders[0] = mapasNiveles[nivelActual].transform.Find("Respawn").GetComponent<BoxCollider>();
        colliders[1] = mapasNiveles[nivelActual].transform.Find("Flag1").GetComponent<BoxCollider>();
        colliders[2] = mapasNiveles[nivelActual].transform.Find("Flag2").GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (primerDialogo && !tocaSuelo)
        {
            dialogos[0].SetActive(true);
            if (player.GetComponent<ThirdPersonController>().Grounded)
            {
                tocaSuelo = true;
                player.GetComponent<ThirdPersonController>().ChangeScaredState();
            }
        }
        
    }
    private void DialogueManager_OnEndDialogue(object sender, int contador)
    {
        if (primerDialogo)
        {
            primerDialogo = false;
            player.GetComponent<ThirdPersonController>().ChangeScaredState();
            player.GetComponent<ThirdPersonController>().MoveSpeed = 2;

        }

        if (dialogoPensamiento)
        {
            player.GetComponent<CharacterController>().enabled = true;
            pensamientoDialogo = false;

        }
    }
    
    private void contadores_OnStarsComplete(object sender, int e)
    {
        AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_KeyShowUp");

        if (nivelActual == 1)
        {
            llaves[nivelActual].SetActive(true);
            GameObject plataformaDeslizante = mapasNiveles[nivelActual].transform.Find("plataformaDeslizante").gameObject;
            plataformaDeslizante.SetActive(true);

        }
        else
        {
            llaves[nivelActual].SetActive(true);
        }
        SwitchToKeyCamera();
    }

    public void IniciarSiguienteNivel()
    {
        //_thirdPersonController = GetComponent<ThirdPersonController>();
        _thirdPersonController.ChangeWinState(); //SustituirScaredState por VictoryState
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<ThirdPersonController>().FinPrimerNivel();
        GetComponent<Animator>().enabled = true;
        

           
    }

    public void SetearNivel ()
    {
        player.GetComponent<ThirdPersonController>().ChangeWinState();
        contadores.ResetearNumeroEstrellas();
        posActualSpawn = new Vector3(0f, 16.7f, 0f);
        player.transform.localPosition = posActualSpawn;
        mapasNiveles[nivelActual].SetActive(false);
        nivelActual++;
        mapasNiveles[nivelActual].SetActive(true);
        colliders[0] = mapasNiveles[nivelActual].transform.Find("Respawn").GetComponent<BoxCollider>();
        colliders[1] = mapasNiveles[nivelActual].transform.Find("Flag1").GetComponent<BoxCollider>();
        colliders[2] = mapasNiveles[nivelActual].transform.Find("Flag2").GetComponent<BoxCollider>();
        player.GetComponent<CharacterController>().enabled = true;


        if (nivelActual == 1)
        {
            Debug.Log("Entra en if nivel actual == 1");
            pensamientoDialogo = true;
            dialogoPensamiento.SetActive(true);
            //enemigos[0].SetActive(true);
            if (pensamientoDialogo)
            {
                Debug.Log("Entra en if pensamientoDialogo");

                SwitchToKeyCamera();
            }
        }
        else if (nivelActual == 2)
        {
        }

    }

    public void DesactivarAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void SetSpawn(Vector3 nuevaPos)
    {
        if (nuevaPos.x < posActualSpawn.x)
        {
            posActualSpawn = nuevaPos;
        }
    }

    public void DefinirColision(Collider other)
    {
        Debug.Log("Entra en el metodo");
        if (other == colliders[0])
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.localPosition = posActualSpawn;
            player.GetComponent<CharacterController>().enabled = true;

        }
        if (other == colliders[1])
        {
            if (nivelActual == 0)
            {
                posNuevoSpawn = new Vector3(-10.86f, 5.50f, -1.66f);

            }else if (nivelActual == 1)
            {
                posNuevoSpawn = new Vector3(-10.29f, 15.28f, 10.31f);

            }
            else if (nivelActual == 2)
            {
                posNuevoSpawn = new Vector3(-4.05f, 9.91f, -3.92f);

            }
            SetSpawn(posNuevoSpawn);

        }

        if (other == colliders[2])
        {
            if (nivelActual == 0)
            {
                posNuevoSpawn = new Vector3(-13.39597f, 10.53f, -1.825865f);

            }
            else if (nivelActual == 1)
            {
                posNuevoSpawn = new Vector3(-26.98f, 16.65f, 5.59f);

            }
            else if (nivelActual == 2)
            {
                posNuevoSpawn = new Vector3(-20.3f, 12.76f, -16.7f);

            }
            
            SetSpawn(posNuevoSpawn);

        }
    }
    public void SwitchToKeyCamera()
    {
        StartCoroutine(SwitchCameraCoroutine());

    }
    private IEnumerator SwitchCameraCoroutine()
    {
        player.GetComponent<CharacterController>().enabled = false;
        if (mainCamera.enabled)
        {
            mainCamera.enabled = false;
        }
        if (pensamientoDialogo)
        {
            Debug.Log("Entra en if pensamientoDialogo de SwitchCamera");

            keyCameras[3].enabled = true;
            yield return new WaitUntil(()=>pensamientoDialogo == false);
            keyCameras[3].enabled = false;
            mainCamera.enabled = true;

        }
        else
        {
            keyCameras[nivelActual].enabled = true;
            yield return new WaitForSeconds(delay);
            keyCameras[nivelActual].enabled = false;
            mainCamera.enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
    public void OnEnable()
    {
        dialogueManager.OnEndDialogue += DialogueManager_OnEndDialogue;
        contadores.OnStarsComplete += contadores_OnStarsComplete;
    }
    public void OnDisable()
    {
        dialogueManager.OnEndDialogue -= DialogueManager_OnEndDialogue;
        contadores.OnStarsComplete -= contadores_OnStarsComplete;
    }
    public void FinJuego()
    {
        AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_PuntuationFinal");
        FinJuegoPanel.SetActive(true);
        monedasTexto.text = contadores.monedasActuales.ToString();
        FinJuegoScreen.SetActive(true);
        Llave.numLlaves = 0;
    }
}
    