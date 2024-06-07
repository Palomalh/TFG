using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderProgressController : MonoBehaviour
{
   //public GameObject personaje;
    public GameObject indicaciones;
    public Slider progressBar; // Arrastra aquí el componente Slider de tu barra de progreso
    public float fillSpeed = 0.1f; // Velocidad de llenado cuando se presiona el botón
    public float decayRate = 0.01f; // Tasa de decaimiento constante de la barra


    private void Update()
    {
        // Siempre disminuye el progreso
        progressBar.value -= decayRate * Time.deltaTime;
        progressBar.value = Mathf.Max(progressBar.value, 0); // Evita que el progreso sea negativo

        // Verifica si el jugador está pulsando el botón (por ejemplo, el botón izquierdo del ratón)
        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseProgress();
        }

        CheckCompletion();
    }

    void IncreaseProgress()
    {
        // Incrementa el progreso cuando el jugador pulsa el botón
        progressBar.value += fillSpeed;
        progressBar.value = Mathf.Min(progressBar.value, progressBar.maxValue); // Asegura que el progreso no exceda el máximo
    }

    void CheckCompletion()
    {
        if (progressBar.value >= progressBar.maxValue)
        {
            // Desactiva el Slider si el progreso es completo
            progressBar.gameObject.SetActive(false);
            indicaciones.SetActive(false);
            //personaje.transform.localPosition = new Vector3(14.76f, 1.47f, 0f);
        }
    }
}
