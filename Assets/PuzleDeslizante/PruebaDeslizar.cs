using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaDeslizar : MonoBehaviour
{
    
    [SerializeField] float deltaX, deltaY;
    [SerializeField] static public bool move = true;
    [SerializeField] Rigidbody2D rb;
    private Vector3 clickPos;

    // Indica si se hizo clic en el objeto.
    bool Clicked = false;

    void Awake()
    {
        // Obtener el componente Rigidbody2D del objeto.
        rb = GetComponent<Rigidbody2D>();

        // Hacer que el objeto no reaccione a fuerzas físicas externas.
        rb.isKinematic = true;
    }

    void Update()
    {
        // Verificar si se hizo clic con el botón izquierdo del ratón.
        if (Input.GetMouseButtonDown(0))
        {
            // Convertir la posición del clic del ratón al mundo del juego.
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Verificar si el clic fue en el objeto.
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(clickPos))
            {
                Clicked = true;

                // Permitir que el objeto responda a fuerzas físicas.
                rb.isKinematic = false;

                // Calcular el desplazamiento del clic con respecto al objeto.
                deltaX = clickPos.x - transform.position.x;
                deltaY = clickPos.y - transform.position.y;
            }
        }

        // Verificar si se está moviendo el objeto y si se mantiene pulsado el botón izquierdo del ratón.
        clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(clickPos) && Clicked && move && Input.GetMouseButton(0))
        {

            // Convertir la posición del ratón al mundo del juego.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Mover el objeto según el desplazamiento del clic.
            rb.MovePosition(new Vector2(mousePos.x - deltaX, mousePos.y - deltaY));
        }

        // Verificar si se ha soltado el botón izquierdo del ratón.
        if (Input.GetMouseButtonUp(0))
        {
            // Indicar que el objeto ya no está siendo clicado.
            Clicked = false;

            // Hacer que el objeto no responda a fuerzas físicas externas.
            rb.isKinematic = true;
        }
    }
}

