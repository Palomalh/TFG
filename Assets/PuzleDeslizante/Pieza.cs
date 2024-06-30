using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pieza : MonoBehaviour
{
    [SerializeField] float deltaX, deltaY;
    static public bool move = true;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private SpriteRenderer rend;
    private int sortingOrder = 5;
    private int sortingOrderStart = 4;
    [SerializeField] private float posIniX, posIniY, posX, posY;
    static public int movimientos;
    public TMP_Text textoMovimientos;
    [SerializeField] WinZone _winZone;

    // Indica si se hizo clic en el objeto.
    bool Clicked = false;

    void Awake()
    {
        // Obtener el componente Rigidbody2D del objeto.
        rb = GetComponent<Rigidbody2D>();
        //// Hacer que el objeto no reaccione a fuerzas físicas externas.
        rb.isKinematic = true;
        movimientos = 0;
        textoMovimientos.SetText("Movimientos " + movimientos.ToString("F0"));
    }

    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            // Verificar si se hizo clic con el botón izquierdo del ratón.
            if (Input.GetMouseButtonDown(0))
            {
                if (_winZone.finJuego) return;
                // Convertir la posición del clic del ratón al mundo del juego.
                Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Verificar si el clic fue en el objeto.
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(clickPos))
                {
                    Clicked = true;

                    posIniX = transform.localPosition.x;
                    posIniY = transform.localPosition.y;

                    // Permitir que el objeto responda a fuerzas físicas.
                    rb.isKinematic = false;

                    // Calcular el desplazamiento del clic con respecto al objeto
                    deltaX = clickPos.x - transform.position.x;
                    deltaY = clickPos.y - transform.position.y;
                }
            }

            // Verificar si se está moviendo el objeto y si se mantiene pulsado el botón izquierdo del ratón.
            if (Clicked && move && Input.GetMouseButton(0))
            {
                rend = GetComponent<SpriteRenderer>();
                rend.sortingOrder = sortingOrder;
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
                if (!rb.isKinematic)
                {
                    //Redondear la posición para encasillar la pieza en el tablero.
                    posY = Mathf.Round(transform.localPosition.y);
                    posX = Mathf.Round(transform.localPosition.x);
                    if (Mathf.Round(posIniX) != posX || Mathf.Round(posIniY) != posY)
                    {
                        movimientos++;
                        posIniX = posX;
                        posIniY = posY;
                        textoMovimientos.SetText("Movimientos " + movimientos.ToString("F0"));
                    }

                    rb.isKinematic = true;
                    rend.sortingOrder = sortingOrderStart;
                    transform.localPosition = new Vector3(posX, posY);
                }
            }
        }
        else
        {
            Touch touch;
            // Mismo proceso con el contacto con los dedos con la pantalla
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (_winZone.finJuego) return;
                
                Vector3 clickPos = Camera.main.ScreenToWorldPoint(touch.position);

                
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(clickPos))
                {
                    Clicked = true;

                    posIniX = transform.localPosition.x;
                    posIniY = transform.localPosition.y;

                    // Permitir que el objeto responda a fuerzas físicas.
                    rb.isKinematic = false;

                    // Calcular el desplazamiento del clic con respecto al objeto
                    deltaX = clickPos.x - transform.position.x;
                    deltaY = clickPos.y - transform.position.y;
                }
            }

            if (Clicked && move && Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                rend = GetComponent<SpriteRenderer>();
                rend.sortingOrder = sortingOrder;
                
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                
                rb.MovePosition(new Vector2(mousePos.x - deltaX, mousePos.y - deltaY));
            }

            if (Input.touchCount == 0)
            {
                
                Clicked = false;
                if (!rb.isKinematic)
                {
                    
                    posY = Mathf.Round(transform.localPosition.y);
                    posX = Mathf.Round(transform.localPosition.x);
                    if (Mathf.Round(posIniX) != posX || Mathf.Round(posIniY) != posY)
                    {
                        movimientos++;
                        posIniX = posX;
                        posIniY = posY;
                        textoMovimientos.SetText("Movimientos " + movimientos.ToString("F0"));
                    }

                    rb.isKinematic = true;
                    rend.sortingOrder = sortingOrderStart;
                    transform.localPosition = new Vector3(posX, posY);
                }
            }
        }
    }
}