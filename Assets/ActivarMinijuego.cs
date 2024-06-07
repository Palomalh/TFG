using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarMinijuego : MonoBehaviour
{
    public EmocionesPersonaje _emocionesPersonaje;
    [SerializeField] private GameObject niebla;
    public GameObject DialogoIntroduccion;
    [SerializeField] private DialogueManager dialogueManager;
    private bool primerDialogo;
    public GameObject minijuego;
    private float densidadFogActual;
    private float densidadFogFinal;
    private bool oscurecido;



    // Start is called before the first frame update
    void Start()
    {
        _emocionesPersonaje.Preocupar();
        AudioManager.audioManagerInstance.PlayMusic("BGM_Anx_1");
        oscurecido = false;
        densidadFogFinal = 0.065F;
        primerDialogo = true;
        densidadFogActual = 0f;
        RenderSettings.fogDensity = 0;
        niebla.SetActive(true);
        DialogoIntroduccion.SetActive(true);

    }


    private void DialogueManager_OnEndDialogue(object sender, int contador)
    {
        if (primerDialogo)
        {
            minijuego.SetActive(true);
            primerDialogo = false;
            RenderSettings.fogDensity = densidadFogFinal;
        }

    }

    private void Update()
    {

        if (!oscurecido && primerDialogo && densidadFogActual < densidadFogFinal)
        {
            RenderSettings.fogDensity += 0.0001F;
            densidadFogActual = RenderSettings.fogDensity;

        } else if (densidadFogActual >= densidadFogFinal)
        {
            oscurecido = true;
        }
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
