using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.Rendering.HighDefinition


public class Contadores : MonoBehaviour
{
    public static Contadores instancia;

    public TMP_Text textoMonedas;
    public TMP_Text textoEstrellas;

    public int monedasActuales;
    public int estrellasActuales;

    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    public event System.EventHandler<int> OnStarsComplete;

    private void Awake()
    {
        instancia = this;
    }
    void Start()
    {
        textoMonedas.text = monedasActuales.ToString();
        textoEstrellas.text = estrellasActuales.ToString() + " / 3 ";

        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            Debug.Log("Color Adjustments encontrado.");
        }
        else
        {
            Debug.LogError("No se encontró el componente Color Adjustments en el perfil del Volume.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AumentarMonedas()
    {
        AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_CoinCollect");
        monedasActuales ++;
        textoMonedas.text = monedasActuales.ToString();
    }

    public void AumentarEstrellas()
    {
        AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_StarCollect");
        estrellasActuales++;
        textoEstrellas.text = estrellasActuales.ToString()+ " / 3 ";
        ReducirSaturacion();
        
        if (estrellasActuales == 3)
        {
            OnStarsComplete?.Invoke(this, estrellasActuales);
            textoEstrellas.text = estrellasActuales.ToString() + " / 3 ";
        }
        
    }

    public void ResetearNumeroEstrellas()
    {
        estrellasActuales = 0;
        textoEstrellas.text = estrellasActuales.ToString() + " / 3 ";

    }


    public void ReducirSaturacion()
    {
        if (colorAdjustments != null)
        {
            // Reducir la saturación en 10 unidades
            colorAdjustments.saturation.value += 10f;

            // Clamping del valor para que no salga del rango permitido
            colorAdjustments.saturation.value = Mathf.Clamp(colorAdjustments.saturation.value, -80f, 0f);
        }
    }
       
    
    public void setEstrellasActuales(int _estrellasActuales)
    {
        estrellasActuales = _estrellasActuales;
    }

    public int getEstrellasActuales()
    {
        return estrellasActuales;
    }
}
