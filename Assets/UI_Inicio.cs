using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Inicio : MonoBehaviour
{
    [SerializeField] private InputActionReference clickAction;
    [SerializeField] private GameObject etiquetaTitulo;
    [SerializeField] private GameObject pantallaSeleccion;

    private bool tituloIniciado = false;

    // Start is called before the first frame update
    void Start()
    {
        etiquetaTitulo.GetComponent<RectTransform>().LeanMoveY(0, 2f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(TituloIniciado);
        clickAction.action.performed += clicked;
        
    }

    private void clicked(InputAction.CallbackContext obj)
    {

        if (tituloIniciado)
        {

            Debug.Log("Se ha hecho click");
            etiquetaTitulo.GetComponent<RectTransform>().LeanMoveY(-855, 1f).setEase(LeanTweenType.easeInElastic);
            pantallaSeleccion.SetActive(true);
            tituloIniciado = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TituloIniciado()
    {
       tituloIniciado = true;
    }

    public void OnEnable()
    {
        clickAction.action.Enable();
    }

    public void OnDisable()
    {
        clickAction.action.Disable();

    }
}
