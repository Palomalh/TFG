using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Rendering;
using System;

public class Countdown : MonoBehaviour
{
    public enum EstadoJuego { Tutorial, Inspirar, Mantener, Espirar, Ganar }
    private int dificultad;
    public EstadoJuego estadoJuego;
    public  GameObject minijuego;

   
    private int repeticion;

    bool inspirarTut;
    bool mantenerTut;
    bool espirarTut;

    bool timerActive;

    private float inspirarTime = 4f;
    private float inspirarMaximo = 4.6f;
    private float mantenerTime = 4f;
    private float espirarTime = 4f;
    private float espirarMaximo = 4.6f;
    private float maxScale = 100f;
    private float minScale = 0.1f;

    private int puntuacion;
    private int puntuacionInspirar;
    private int puntuacionMantener;
    private int puntuacionEspirar;
    private float timeStart;
    private float tiempoTranscurrido;
    private float escalaInicial;
    private float escalaActual;

    [SerializeField] private float minEsp;
    [SerializeField] private float maxEsp;
    [SerializeField] private float minInsp;
    [SerializeField] private float maxInsp;
    [SerializeField] private float minMant;
    [SerializeField] private float maxMant;

    public InputActionReference inspirarAction;
    public InputActionReference espirarAction;

    [SerializeField] private SpriteRenderer aro;
    [SerializeField] private GameObject circuloCierreEfecto;
    [SerializeField] private GameObject circuloAperturaEfecto;

    [SerializeField] private GameObject clickIcon;
    [SerializeField] private Sprite leftIcon;
    [SerializeField] private Sprite rightIcon;

    [Header("------------- Niebla -------------")]
    [SerializeField] private ParticleSystem niebla;
    [SerializeField] private float duracionTransicion = 2f;
    [SerializeField] private Color colorInicio;
    [SerializeField] private float decrementoAlphaPorFrame = 2f;


    [Header("------------- Mensajes -------------")]
    [SerializeField] private string[] mensajesPositivos;
    [SerializeField] private string[] mensajesNegativos;
    [SerializeField] private string[] instrucciones;
    [SerializeField] private TMP_Text guia;
    [SerializeField] private TMP_Text textCuenta;
    [SerializeField] private TMP_Text textPuntuacion;

    [SerializeField] private GameObject floatingTextPrefab;
    private GameObject floatingTextInstance;

    [Header("------------- Dialogos Tutorial -------------")]
    [SerializeField] private GameObject[] dialogos;
    [SerializeField] private int currentDialogue;
    [SerializeField] private DialogueManager dialogueManager;
    private bool repreVisual;
    [SerializeField] GameObject botonesTuto;

    public GameObject tactilInsEsp1;
    public GameObject tactilInsEsp2;

    [Header("-------------Fin de Juego -------------")]
    public GameObject finDeJuego;
    bool finPartida;
    public TMP_Text puntos;


    // Start is called before the first frame update

    private void Awake()
    {


        dificultad = PlayerPrefs.GetInt("nivelJuego");
        puntuacionInspirar = 50;
        puntuacionMantener = 50;
        puntuacionEspirar = 50;

        puntuacion = 0;

        repeticion = 0;

        escalaInicial = 10f;

        timeStart = 0;
        timerActive = false;

        colorInicio = niebla.startColor;

        inspirarTut = true;
        mantenerTut = false;
        espirarTut = false;

        repreVisual = false;
    }
    void Start()
    {

        if (dificultad == 0)
        {
            Iniciacion();
        }else
        {
            Avanzado();
        }

        finPartida = false;

        textPuntuacion.text = "Puntuación: " + puntuacion.ToString();

        guia.text = ("Inspira " + inspirarTime + " segundos");

        escalaActual = escalaInicial;

        textCuenta.text = timeStart.ToString("F0");

        if (!Application.isMobilePlatform)
        {
            inspirarAction.action.performed += inspirar;
            inspirarAction.action.canceled += soltarInspirar;



            espirarAction.action.performed += espirar;
            espirarAction.action.canceled += soltarEspirar;
        }
        
    }



    // Update is called once per frame
    void Update()
    {

        switch (estadoJuego)
        {
            case EstadoJuego.Tutorial:

                //Diálogo explica síntomas de estrés por examenes e introduce el ejercicio de respiración con 3 pasos
                //Solo se ejecuta si currentDialogue == 0
                if (currentDialogue == 0)
                {
                    dialogos[currentDialogue].SetActive(true);

                }
                //Aro aumenta su tamaño
                if (inspirarTut && repreVisual)
                {
                    if (!Application.isMobilePlatform)
                    {

                        clickIcon.SetActive(true);
                    }
                    else
                    {
                        tactilInsEsp1.GetComponent<Image>().enabled = true;
                    }
                    dialogos[currentDialogue-1].SetActive(false);

                    if (tiempoTranscurrido <= maxInsp)
                    {
                        tiempoTranscurrido += Time.deltaTime;
                        textCuenta.text = (tiempoTranscurrido).ToString("F0");
                        escalaActual = Mathf.Lerp(escalaInicial, maxScale, tiempoTranscurrido / inspirarMaximo);
                        aro.transform.localScale = new Vector3(escalaActual, escalaActual, 1f);
                        if (tiempoTranscurrido >= (minInsp + 0.3f) && tiempoTranscurrido <= maxInsp)
                        {
                            inspirarTut = false;
                            mantenerTut = true;
                            tiempoTranscurrido = 0f;

                            if (!Application.isMobilePlatform)
                            {
                                clickIcon.SetActive(false);
                            }
                            else
                            {
                                tactilInsEsp1.GetComponent<Image>().enabled = false;

                            }
                            repreVisual = false;
                            dialogos[currentDialogue].SetActive(true);
                        }
                    }
                }

                
                //Interrupción con diálgo que explica que terminada la inspiración comienza la fase de mantener el aire X segundos
                //Aro se mantiene
                if (mantenerTut && repreVisual)
                {
                    
                    guia.text = ("Mantén la respiración " + mantenerTime.ToString("F0") + " segundos");
                    dialogos[currentDialogue-1].SetActive(false);

                    //tiempoTranscurrido = timeStart;
                    textCuenta.text = tiempoTranscurrido.ToString("F0");
                    //Pasa el tiempo de mantener la respiracion mientras no se toque ninguna tecla
                    tiempoTranscurrido += Time.deltaTime;
                    //Sumo 0.3 para que redondee antes y podamos ver el número a tiempo, antes de que cambie a 0 para la fase de espirar.
                    textCuenta.text = (0.3 + tiempoTranscurrido).ToString("F0");

                    //Se modifica el tamaño del aro levemente para que alcance la posición que tiene cuando empieza a espirar
                    float scaleTut = Mathf.Lerp(escalaActual, 85f, tiempoTranscurrido / mantenerTime);
                    aro.transform.localScale = new Vector3(scaleTut, scaleTut, 1f);
                    if (tiempoTranscurrido >= minMant && tiempoTranscurrido <= maxMant)
                    {
                        mantenerTut = false;
                        espirarTut = true;
                        tiempoTranscurrido = 0f;

                        repreVisual = false;
                        dialogos[currentDialogue].SetActive(true);
                    }
                }

                //Interrupción con diálgo que explica que terminada la fase de mantener comienza la fase de soltar el aire X segundos
                //Aro disminuye su tamaño
                if (espirarTut && repreVisual)
                {
                    if (!Application.isMobilePlatform)
                    {
                        clickIcon.transform.Find("mouseClick").GetComponent<Image>().sprite = rightIcon;
                        clickIcon.SetActive(true);

                    }
                    else
                    {
                                tactilInsEsp2.GetComponent<Image>().enabled = true;
                    }

                    guia.text = ("Espira " + espirarTime + " segundos");
                    dialogos[currentDialogue - 1].SetActive(false);
                    if (tiempoTranscurrido <= maxEsp)
                    {
                        //aumenta el contador
                        tiempoTranscurrido += Time.deltaTime;
                        textCuenta.text = tiempoTranscurrido.ToString("F0");

                        //disminuye el aro
                        float scale2 = Mathf.Lerp(escalaActual, minScale, tiempoTranscurrido / espirarMaximo);
                        aro.transform.localScale = new Vector3(scale2, scale2, 1f);

                        if (tiempoTranscurrido >= (minEsp + 0.3f) && tiempoTranscurrido <= maxEsp)
                        {
                            espirarTut = false;
                            tiempoTranscurrido = 0f;
                            if (!Application.isMobilePlatform)
                            {
                                clickIcon.SetActive(false);
                                clickIcon.transform.Find("mouseClick").GetComponent<Image>().sprite = leftIcon;
                            }
                            else
                            {
                                tactilInsEsp2.GetComponent<Image>().enabled = false;

                            }

                            repreVisual = false;
                            dialogos[currentDialogue].SetActive(true);
                        }
                    }
                }

                /*Interrupción con dialogo:
                     ¿Se ha entendido? --> Díálogo ("Genial, ahora es tu turno" + recordar los segundos que debe inspirar, mantener, espirar)
                     ¿No se ha entendido? --> Repetir tutorial.
                */


                break;

            case EstadoJuego.Inspirar:

                if (Application.isMobilePlatform) {
                    tactilInsEsp1.SetActive(true);
                    tactilInsEsp2.SetActive(true);
                }

                if (!Application.isMobilePlatform)
                {
                    clickIcon.SetActive(true);
                }
                else
                {
                    tactilInsEsp1.GetComponent<Image>().enabled = true;

                }

                if (timerActive)
                {
                    //aumenta el contador
                    tiempoTranscurrido += Time.deltaTime;
                    textCuenta.text = tiempoTranscurrido.ToString("F0");

                    //aumenta el aro
                    escalaActual = Mathf.Lerp(escalaInicial, maxScale, tiempoTranscurrido / inspirarMaximo);
                    aro.transform.localScale = new Vector3(escalaActual, escalaActual, 1f);
                }

                break;

            case EstadoJuego.Mantener:

                //Pasa el tiempo de mantener la respiracion mientras no se toque ninguna tecla
                tiempoTranscurrido += Time.deltaTime;
                //Sumo 0.3 para que redondee antes y podamos ver el número a tiempo, antes de que cambie a 0 para la fase de espirar.
                textCuenta.text = (0.3 + tiempoTranscurrido).ToString("F0");

                //Se modifica el tamaño del aro levemente para que alcance la posición que tiene cuando empieza a espirar
                float scale = Mathf.Lerp(escalaActual, 85f, tiempoTranscurrido / mantenerTime);
                aro.transform.localScale = new Vector3(scale, scale, 1f);

                //Comprobar que la fase se ha realizado correctamente
                if (Input.anyKey || Input.touchCount>0)
                {
                    AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Error");

                    //Si se ha pulsado, esta fase da 0 puntos y se indica que se ha realizado mal.
                    puntuacionMantener = 0;
                    ShowFloatingText(mensajesNegativos);

                    //El juego se reinicia
                    //El contador de tiempo vuelve a 0
                    tiempoTranscurrido = timeStart;
                    textCuenta.text = tiempoTranscurrido.ToString("F0");

                    //El aro y el juego vuelven a su primera fase (inspirar)
                    aro.transform.localScale = new Vector3(escalaInicial, escalaInicial, 1f);
                    estadoJuego = EstadoJuego.Inspirar;
                }
                else
                {
                    //Si no se ha pulsado ninguna tecla, comprobar si se mantiene el tiempo debido
                    if (tiempoTranscurrido >= minMant && tiempoTranscurrido <= maxMant)
                    {
                        //A la puntuación se le suma la que corresponda para esta fase de mantener y se felicita al jugador
                        AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Success");

                        puntuacion += puntuacionMantener;
                        textPuntuacion.text = "Puntuación: " + puntuacion.ToString();
                        ShowFloatingText(mensajesPositivos);

                        //El contador de tiempo vuelve a 0 y se establece como escala actual del aro la que ha obtenido mientras cambiaba levemenete su tamaño
                        tiempoTranscurrido = timeStart;
                        textCuenta.text = tiempoTranscurrido.ToString("F0");
                        escalaActual = scale;

                        //Se cambia el mensaje para indicar que empieza la última fase del juego.
                        guia.text = ("Espira " + espirarTime + " segundos");
                        estadoJuego = EstadoJuego.Espirar;

                    }
                }

                break;

            case EstadoJuego.Espirar:
                if (!Application.isMobilePlatform)
                {
                    clickIcon.transform.Find("mouseClick").GetComponent<Image>().sprite = rightIcon;
                    clickIcon.SetActive(true);
                }
                else
                {
                    tactilInsEsp2.GetComponent<Image>().enabled = true;

                }

                if (timerActive)
                {

                    //aumenta el contador
                    tiempoTranscurrido += Time.deltaTime;
                    textCuenta.text = tiempoTranscurrido.ToString("F0");

                    //disminuye el aro
                    float scale2 = Mathf.Lerp(escalaActual, minScale, tiempoTranscurrido / espirarMaximo);
                    aro.transform.localScale = new Vector3(scale2, scale2, 1f);
                }


                break;

            case EstadoJuego.Ganar:

                finPartida = true;
                puntos.text= puntuacion.ToString();
                finDeJuego.SetActive(true);
                finDeJuego.GetComponent<RectTransform>().LeanMoveY(0, -989f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);



                break;



        }
    }

    public void inspirar(InputAction.CallbackContext obj)
    {
        if (finPartida) return;
        if (estadoJuego != EstadoJuego.Tutorial && estadoJuego == EstadoJuego.Inspirar)
        {
                if (obj.interaction is HoldInteraction)
                {
                    timerActive = true;
                }
                else
                    timerActive = false;
        }
    }

    public void inspirar()
    {
        if (estadoJuego != EstadoJuego.Tutorial && estadoJuego == EstadoJuego.Inspirar)
        {
            
                timerActive = true;

        }
    }

    public void espirar()
    {
        if (estadoJuego != EstadoJuego.Tutorial && estadoJuego == EstadoJuego.Espirar)
        {
            
                timerActive = true;

        }
    }

    public void espirar(InputAction.CallbackContext obj)
    {
        if (finPartida) return;
        if ( estadoJuego != EstadoJuego.Tutorial && estadoJuego == EstadoJuego.Espirar)
        {
            if (obj.interaction is HoldInteraction)
            {
                timerActive = true;

            }
            else
                timerActive = false;

        }
    }
    private void soltarInspirar(InputAction.CallbackContext obj)
    {
        if (finPartida) return;
        if (estadoJuego != EstadoJuego.Tutorial) {
        if (estadoJuego == EstadoJuego.Espirar || estadoJuego == EstadoJuego.Mantener)
        {
            estadoJuego = EstadoJuego.Inspirar;
        }
        //Desactiva el contador
        timerActive = false;

        //Al desactivar el contador, deja de sumar el tiempo transcurrido
        //y se comprueba si está dentro del intervalo que se debe inspirar.
        if (tiempoTranscurrido >= minInsp && tiempoTranscurrido <= maxInsp)
        {
            //Si se inspira lo que se debe se suma la puntuación y se reproduce el sonido de éxito
            AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Success");
            puntuacion += puntuacionInspirar;
            textPuntuacion.text = "Puntuación: " + puntuacion.ToString();

            //Se reproduce un efecto satisfactorio con el aro
            ReproduccionParticulas(circuloAperturaEfecto);
            ShowFloatingText(mensajesPositivos);

            //Se reinicia el contador y se da pie a la nueva fase
            tiempoTranscurrido = timeStart;
            textCuenta.text = tiempoTranscurrido.ToString("F0");

            guia.text = ("Mantén la respiración "+ mantenerTime.ToString("F0") + " segundos");
                if (!Application.isMobilePlatform)
                {
                    clickIcon.SetActive(false);
                }
                else
                {
                    tactilInsEsp1.GetComponent<Image>().enabled = false;
                }
                estadoJuego = EstadoJuego.Mantener;
        }
        else
        {
            //Si no se inspira el tiempo que se debe, se reinicia la fase y
            //los puntos que se otorgan pasan a ser 0 y se reproduce el sonido de error
            AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Error");
            puntuacionInspirar = 0;
            ShowFloatingText(mensajesNegativos);
            guia.text = "¡Intentémoslo de nuevo, inspira!";
            tiempoTranscurrido = timeStart;
            textCuenta.text = tiempoTranscurrido.ToString("F0");
            aro.transform.localScale = new Vector3(escalaInicial, escalaInicial, 1f);
        }
        }
    }


    public void soltarInspirar()
    {
        if (estadoJuego != EstadoJuego.Tutorial)
        {
            if (estadoJuego == EstadoJuego.Espirar || estadoJuego == EstadoJuego.Mantener)
            {
                estadoJuego = EstadoJuego.Inspirar;
            }
            //Desactiva el contador
            timerActive = false;

            //Al desactivar el contador, deja de sumar el tiempo transcurrido y se comprueba si está dentro del intervalo que se debe inspirar.
            if (tiempoTranscurrido >= minInsp && tiempoTranscurrido <= maxInsp)
            {
                //Si se inspira lo que se debe se suma la puntuación y se reproduce el sonido de éxito
                AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Success");
                puntuacion += puntuacionInspirar;
                textPuntuacion.text = "Puntuación: " + puntuacion.ToString();

                //Se reproduce un efecto satisfactorio con el aro
                ReproduccionParticulas(circuloAperturaEfecto);
                ShowFloatingText(mensajesPositivos);

                //Se reinicia el contador y se da pie a la nueva fase
                tiempoTranscurrido = timeStart;
                textCuenta.text = tiempoTranscurrido.ToString("F0");

                guia.text = ("Mantén la respiración " + mantenerTime.ToString("F0") + " segundos");
                if (!Application.isMobilePlatform)
                {
                    clickIcon.SetActive(false);
                }
                else
                {
                    tactilInsEsp1.GetComponent<Image>().enabled = false;

                }
                estadoJuego = EstadoJuego.Mantener;

            }
            else
            {
                //Si no se inspira el tiempo que se debe, se reinicia la fase y los puntos que se otorgan pasan a ser 0 y se reproduce el sonido de error
                AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Error");

                puntuacionInspirar = 0;
                ShowFloatingText(mensajesNegativos);
                guia.text = "¡Intentémoslo de nuevo, inspira!";


                tiempoTranscurrido = timeStart;
                textCuenta.text = tiempoTranscurrido.ToString("F0");
                aro.transform.localScale = new Vector3(escalaInicial, escalaInicial, 1f);

            }

        }
    }

    private void soltarEspirar(InputAction.CallbackContext obj)
    {
        if (finPartida) return;
        if (estadoJuego != EstadoJuego.Tutorial)
        {
            timerActive = false;

            if (tiempoTranscurrido >= minEsp && tiempoTranscurrido <= maxEsp)
            {
                //Se pasa a la siguiente repetición de las tres que completan el juego
                repeticion++;
                //ReducirNiebla(niebla);
                StartCoroutine(ReducirNiebla());
                StartCoroutine(ReducirDensidadFog());

                //Si se ha superado la fase de espirar (La última del ciclo) pero todavía no ha habidio tres repeticiones del ciclo, se vuelve a la primera fase.
                if (repeticion < 3)
                {
                    //Se suma la puntuación de espirar obtenida en esta fase y se reproduce el sonido de exito de fin de ciclo
                    AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_CycleFinal");

                    puntuacion += puntuacionEspirar;
                    textPuntuacion.text = "Puntuación: " + puntuacion.ToString();

                    //Se reproduce un efecto satisfactorio para el jugador
                    ReproduccionParticulas(circuloCierreEfecto);
                    ShowFloatingText(mensajesPositivos);
                    guia.text = "¡Muy bien!\nInspira";

                    //Se reinicia el ciclo y las puntuaciones que se obtienen de cada fase.
                    tiempoTranscurrido = timeStart;
                    puntuacionInspirar = 50;
                    puntuacionEspirar = 50;
                    puntuacionMantener = 50;

                    if (!Application.isMobilePlatform)
                    {
                        clickIcon.SetActive(false);
                        clickIcon.transform.Find("mouseClick").GetComponent<Image>().sprite = leftIcon;
                    }
                    else
                    {
                        tactilInsEsp2.GetComponent<Image>().enabled = false;
                        tactilInsEsp1.GetComponent<Image>().enabled = true;


                    }


                    estadoJuego = EstadoJuego.Inspirar;
                }
                else //Si ya ha habido tres repeticiones, el juego termina.
                {
                    AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_PuntuationFinal");

                    puntuacion += puntuacionEspirar;
                    textPuntuacion.text = "Puntuación: " + puntuacion.ToString();

                    if (!Application.isMobilePlatform)
                    {
                        clickIcon.SetActive(false);
                        clickIcon.transform.Find("mouseClick").GetComponent<Image>().sprite = leftIcon;
                    }
                    else
                    {
                        tactilInsEsp1.GetComponent<Image>().enabled = false;
                        tactilInsEsp2.GetComponent<Image>().enabled = false;

                    }

                    estadoJuego = EstadoJuego.Ganar;
                }


            }
            else //Si no se ha espira el tiempo suficiente
            {
                //Esta fase da 0 puntos y se reproduce el sonido de error
                AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Error");
                puntuacionEspirar = 0;
                ShowFloatingText(mensajesNegativos);

                //Se vuelve a la primera fase del ciclo
                tiempoTranscurrido = timeStart;
                textCuenta.text = tiempoTranscurrido.ToString("F0");
                aro.transform.localScale = new Vector3(escalaInicial, escalaInicial, 1f);
                estadoJuego = EstadoJuego.Inspirar;

            }

        }
    }

    public void soltarEspirar( )
    {
        if (estadoJuego != EstadoJuego.Tutorial)
        {
            timerActive = false;

            if (tiempoTranscurrido >= minEsp && tiempoTranscurrido <= maxEsp)
            {
                //Se pasa a la siguiente repetición de las tres que completan el juego
                repeticion++;
                //ReducirNiebla(niebla);
                StartCoroutine(ReducirNiebla());
                StartCoroutine(ReducirDensidadFog());

                //Si se ha superado la fase de espirar (La última del ciclo) pero todavía no ha habidio tres repeticiones del ciclo, se vuelve a la primera fase.
                if (repeticion < 3)
                {
                    //Se suma la puntuación de espirar obtenida en esta fase
                    AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_CycleFinal");

                    puntuacion += puntuacionEspirar;
                    textPuntuacion.text = "Puntuación: " + puntuacion.ToString();

                    //Se reproduce un efecto satisfactorio para el jugador
                    ReproduccionParticulas(circuloCierreEfecto);
                    ShowFloatingText(mensajesPositivos);
                    guia.text = "¡Muy bien!\nInspira";

                    //Se reinicia el ciclo y las puntuaciones que se obtienen de cada fase.
                    tiempoTranscurrido = timeStart;
                    puntuacionInspirar = 50;
                    puntuacionEspirar = 50;
                    puntuacionMantener = 50;

                    if (!Application.isMobilePlatform)
                    {
                        clickIcon.SetActive(false);
                        clickIcon.transform.Find("mouseClick").GetComponent<Image>().sprite = leftIcon;
                    }
                    else
                    {
                        tactilInsEsp1.GetComponent<Image>().enabled = false;

                    }

                    estadoJuego = EstadoJuego.Inspirar;
                }
                else //Si ya ha habido tres repeticiones, el juego termina.
                {
                    AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_PuntuationFinal");

                    if (!Application.isMobilePlatform)
                    {
                        clickIcon.SetActive(false);
                        clickIcon.transform.Find("mouseClick").GetComponent<Image>().sprite = leftIcon;
                    }
                    else
                    {
                        tactilInsEsp2.GetComponent<Image>().enabled = false;

                    }
                    estadoJuego = EstadoJuego.Ganar;
                }


            }
            else //Si no se ha espira el tiempo suficiente
            {
                //Esta fase da 0 puntos
                AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_Error");

                puntuacionEspirar = 0;
                ShowFloatingText(mensajesNegativos);

                //Se vuelve a la primera fase del ciclo
                tiempoTranscurrido = timeStart;
                textCuenta.text = tiempoTranscurrido.ToString("F0");
                aro.transform.localScale = new Vector3(escalaInicial, escalaInicial, 1f);
                estadoJuego = EstadoJuego.Inspirar;

            }

        }
    }

    private void ReproduccionParticulas(GameObject efectoAro)
    {
        ParticleSystem sistemaPrincipal = efectoAro.GetComponent<ParticleSystem>();
        // Establece la velocidad de reproducción del sistema principal
        sistemaPrincipal.playbackSpeed = 0.5f;

        // Itera a través de todos los sub-sistemas
        foreach (Transform hijo in sistemaPrincipal.transform)
        {
            // Verifica si el hijo tiene un componente ParticleSystem
            ParticleSystem subSistema = hijo.GetComponent<ParticleSystem>();

            if (subSistema != null)
            {
                // Establece la velocidad de reproducción para cada sub-sistema
                subSistema.playbackSpeed = 0.5f;
            }
        }
        // Reproduce el sistema principal y todos sus sub-sistemas
        sistemaPrincipal.Play();
    }

    private void ShowFloatingText(string[] mensaje)
    {
        //Verificar si ya hay una instancia y destruirla si es necesario
        if (floatingTextInstance != null)
        {
            Destroy(floatingTextInstance);
        }

        //Crear una nueva instancia
        floatingTextInstance = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);


        //Configurar el texto en la nueva instancia
        int indiceRandom = (int)UnityEngine.Random.Range(0, mensaje.Length - 1);
        floatingTextInstance.GetComponent<TextMeshPro>().text = mensaje[indiceRandom];


    }



    IEnumerator ReducirNiebla()
    {
        float tiempoPasado = 0f;
        Color colorActual = colorInicio;

        while (tiempoPasado < duracionTransicion)
        {
            // Decrementar el canal alpha
            colorActual.a -= decrementoAlphaPorFrame / 255.0f;

            // Establecer el color de inicio de las partículas
            //var mainModule = niebla.main;
            niebla.startColor = colorActual;

            // Incrementar el tiempo transcurrido
            tiempoPasado += Time.deltaTime;

            // Esperar al siguiente frame
            yield return null;
        }

        // Asegurarse de que el color final sea exacto al final de la transición
        colorInicio = new Color(colorInicio.r, colorInicio.g, colorInicio.b, colorActual.a);

    }

    IEnumerator ReducirDensidadFog()
    {
        float tiempoPasado = 0f;
        float densidadActual = RenderSettings.fogDensity;

        while (tiempoPasado < duracionTransicion)
        {
            densidadActual -= (densidadActual / 3.5f) * Time.deltaTime;
            RenderSettings.fogDensity = densidadActual;

            tiempoPasado += Time.deltaTime;
            yield return null;
        }

    }

    private void DialogueManager_OnEndDialogue(object sender, int contador)
    {
        //Cuando el diálogo se cierra, el contador interno de la clase Diálogo aumenta en uno, así que aumentamos en uno el currentDialogue.
        currentDialogue++;

        //Si ya hemos explicado el juego, comenzamos con la representación visual.
        if (currentDialogue != 0)
        {
            repreVisual = true;
        }

        if (currentDialogue == 4)
        {
            botonesTuto.SetActive(true);
            dialogos[currentDialogue - 1].SetActive(false);
        }

    }

    public void RepetirTutorial()
    {
        currentDialogue = 0;
        botonesTuto.SetActive(false);
        inspirarTut = true;
        repreVisual = false;
    }

    public void EmpezarJuego()
    {
        AudioManager.audioManagerInstance.PlayMusic("BGM_Anx_2");

        currentDialogue = 0;
        botonesTuto.SetActive(false);

        escalaActual = escalaInicial;

        tiempoTranscurrido = timeStart;
        textCuenta.text = tiempoTranscurrido.ToString("F0");
        guia.text = ("Inspira " + mantenerTime.ToString("F0") + " segundos");
        estadoJuego = EstadoJuego.Inspirar;


    }
    public void Iniciacion()
    {

        //Valores para comprobar si está dentro del rango permitido en los IFs
        minEsp = 3.5f;
        maxEsp = 4.5f;
        minInsp = 3.5f;
        maxInsp = 4.5f;
        minMant = 3.5f;
        maxMant = 4.5f;

        //Valores para modificar el tamaño del aro
        inspirarTime = 4f;
        inspirarMaximo = 4.6f;
        mantenerTime = 4f;
        espirarTime = 4f;
        espirarMaximo = 4.6f;

        estadoJuego = EstadoJuego.Tutorial;
    }

    public void Avanzado()
    {
        //Valores para comprobar si está dentro del rango permitido en los IFs
        minEsp = 7.5f;
        maxEsp = 8.5f;
        minInsp = 3.5f;
        maxInsp = 4.5f;
        minMant = 6.5f;
        maxMant = 7.5f;

        //Valores para modificar el tamaño del aro
        inspirarTime = 4f;
        inspirarMaximo = 4.6f;
        mantenerTime = 7.2f;
        espirarTime = 8f;
        espirarMaximo = 8.6f;

        estadoJuego = EstadoJuego.Tutorial;
    }

    public void Error(string puntuacion)
    {

        if (puntuacion.Equals("puntuacionInspirar"))
        {
            puntuacionInspirar = 0;
        }
        else
            puntuacionEspirar = 0;

        ShowFloatingText(mensajesNegativos);

        //Se vuelve a la primera fase del ciclo
        tiempoTranscurrido = timeStart;
        textCuenta.text = tiempoTranscurrido.ToString("F0");
        aro.transform.localScale = new Vector3(escalaInicial, escalaInicial, 1f);
        estadoJuego = EstadoJuego.Inspirar;
    }


    public void OnEnable()
    {
        inspirarAction.action.Enable();
        espirarAction.action.Enable();
        dialogueManager.OnEndDialogue += DialogueManager_OnEndDialogue;
    }

    public void OnDisable()
    {
        inspirarAction.action.Disable();
        espirarAction.action.Disable();
        dialogueManager.OnEndDialogue -= DialogueManager_OnEndDialogue;
    }
}
