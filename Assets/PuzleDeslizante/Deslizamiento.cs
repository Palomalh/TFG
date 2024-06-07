using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deslizamiento : MonoBehaviour
{
    [SerializeField] float deltaX, deltaY;
    [SerializeField] static public bool move = true;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private Vector3 offset;

    // Indica si se hizo clic en el objeto.
    bool Clicked = false;

    void Awake()
    {
        //Obtener el componente Rigidbody2D del objeto.
        rb = GetComponent<Rigidbody2D>();

        //Hacer que el objeto no reaccione a fuerzas físicas externas.
         //rb.isKinematic = true;
    }

    void Update()
    {
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Verificar si se hizo clic con el botón izquierdo del ratón.
        if (Input.GetMouseButtonDown(0))
        {


            offset = this.transform.position - clickPos;  
        }

        //Verificar si se está moviendo el objeto y si se mantiene pulsado el botón izquierdo del ratón.
        if (Clicked)
        {
            this.transform.position = clickPos + offset;

        }

        //Verificar si se ha soltado el botón izquierdo del ratón.
        if (Input.GetMouseButtonUp(0))
        {
            //Indicar que el objeto ya no está siendo clicado.
           Clicked = false;

            //Hacer que el objeto no responda a fuerzas físicas externas.
            //rb.isKinematic = true;
        }
    }

    private void OnMouseDown()
    {
        Clicked = true;
    }
}