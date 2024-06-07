using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField] float deltaX, deltaY;
    [SerializeField] static public bool move = true;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private int width, height;
    public float posX, posY;
    public int ancho, alto;

    public Vector2 XMovement;
    public Vector2 YMovement;

    // Indica si se hizo clic en el objeto.
    bool Clicked = false;

    void Awake()
    {
        // Obtener el componente Rigidbody2D del objeto.
        rb = GetComponent<Rigidbody2D>();

        //// Hacer que el objeto no reaccione a fuerzas físicas externas.
        rb.isKinematic = true;
    }

    void Update()
    {
        // Verificar si se hizo clic con el botón izquierdo del ratón.
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Se hizo clic");
            // Convertir la posición del clic del ratón al mundo del juego.
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Verificar si el clic fue en el objeto.
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(clickPos))
            {
                Debug.Log("Se hizo clic en el objeto");
                Clicked = true;

                // Permitir que el objeto responda a fuerzas físicas.
                rb.isKinematic = false;

                // Calcular el desplazamiento del clic con respecto al objeto.
                deltaX = clickPos.x - transform.position.x;
                deltaY = clickPos.y - transform.position.y;
            }
        }

        // Verificar si se está moviendo el objeto y si se mantiene pulsado el botón izquierdo del ratón.
        if (Clicked && move && Input.GetMouseButton(0))
        {
            // Convertir la posición del ratón al mundo del juego.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Mover el objeto según el desplazamiento del clic.
            rb.MovePosition(new Vector2(mousePos.x - deltaX, mousePos.y - deltaY));
        }

        // Verificar si se ha soltado el botón izquierdo del ratón.
        if (Input.GetMouseButtonUp(0))
        {

            int integerPartX;
            float decimalPartX;

            int integerPartY;
            float decimalPartY;

            float Xpart = .0f;
            float Ypart = .0f;
            // Indicar que el objeto ya no está siendo clicado.
            Clicked = false;

            if (!rb.isKinematic)
            {

                integerPartX = (int)transform.localPosition.x / 1;
                decimalPartX = transform.localPosition.x % 1;

                integerPartY = (int)transform.localPosition.y / 1;
                decimalPartY = transform.localPosition.y % 1;



                if (integerPartX >= 0)
                {
                    if (decimalPartX - .5f >= .25f)
                    {
                        Xpart = integerPartX + 1;
                    }
                    else if (decimalPartX - .5f <= .25f)
                    {
                        float value = decimalPartX - .5f;
                        if (value >= .0f)
                        {
                            Xpart = integerPartX + .5f;
                        }
                        else
                        {
                            float absoluteValue = Mathf.Abs(value);
                            if (absoluteValue <= .25f)
                            {
                                Xpart = integerPartX + .5f;
                            }
                            else
                            {
                                Xpart = integerPartX;
                            }
                        }
                    }
                }
                else
                {
                    if (decimalPartX + .5f >= -.25f)
                    {
                        Xpart = integerPartX - 1;
                    }
                    else if (decimalPartX + .5f <= -.25f)
                    {
                        float value = decimalPartX + .5f;
                        if (value <= .0f)
                        {
                            print("El valor decimal es menor que .25f");
                            Xpart = integerPartX - .5f;
                        }
                        else
                        {
                            if (value <= .25f)
                            {
                                Xpart = integerPartX - .5f;
                            }
                            else
                            {
                                Xpart = integerPartX;
                            }
                        }
                    }
                }

                if (integerPartY >= 0)
                {
                    if (decimalPartY - .5f >= .25f)
                    {
                        Ypart = integerPartY + 1;
                    }
                    else if (decimalPartY - .5f <= .25f)
                    {
                        float value = decimalPartY - .5f;
                        if (value >= .0f)
                        {
                            Ypart = integerPartY + .5f;
                        }
                        else
                        {
                            float absoluteValue = Mathf.Abs(value);
                            if (absoluteValue <= .25f)
                            {
                                Ypart = integerPartY + .5f;
                            }
                            else
                            {
                                Ypart = integerPartY;
                            }
                        }
                    }
                }
                else
                {
                    if (decimalPartY + .5f >= -.25f)
                    {
                        Ypart = integerPartY - 1;
                    }
                    else if (decimalPartY + .5f <= -.25f)
                    {
                        float value = decimalPartY + .5f;
                        if (value <= .0f)
                        {
                            Ypart = integerPartY - .5f;
                        }
                        else
                        {
                            if (value <= .25f)
                            {
                                Ypart = integerPartY - .5f;
                            }
                            else
                            {
                                Ypart = integerPartY;
                            }
                        }
                    }
                }
                Xpart = Mathf.Clamp(Xpart, XMovement.x, XMovement.y);
                Ypart = Mathf.Clamp(Ypart, YMovement.x, YMovement.y);
                // Hacer que el objeto no responda a fuerzas físicas externas.
                rb.isKinematic = true;
                transform.localPosition = new Vector3(Xpart, Ypart);
            }



        }
    }
}


