using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slide2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public static bool move = true; // Controla si las piezas pueden moverse

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); // Obtener el RectTransform para mover la pieza
        canvasGroup = GetComponent<CanvasGroup>(); // Obtener el CanvasGroup para controlar la interactividad

        if (canvasGroup == null) // Asegurarse de que hay un CanvasGroup y añadir uno si no lo hay
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalPosition = rectTransform.anchoredPosition; // Guardar la posición original por si necesitas resetearla
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!move) return; // Si no se pueden mover las piezas, no hacer nada

        // Opcional: Añadir lógica para cuando se presiona una pieza, como cambiar su apariencia
        canvasGroup.blocksRaycasts = false; // Esto permite que el evento de arrastre "vea" a través de la pieza para que puedas soltarla correctamente
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!move) return; // Si no se pueden mover las piezas, no hacer nada

        Vector2 newPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out newPosition);
        rectTransform.anchoredPosition = newPosition; // Mover la pieza según la posición del ratón
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Asegurarse de que la pieza puede volver a ser seleccionada por eventos de raycast

        // Opcional: Añadir lógica para cuando se suelta una pieza, como verificar si está en la posición correcta
    }
}
