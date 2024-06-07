using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tactilInsEsp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Countdown countdown;
    public enum Respiracion
    {
        Inspirar,
        Espirar
    }

    public Respiracion tipoRespiracion;

    public void OnPointerDown(PointerEventData eventData)
    {
        Clicar(countdown, tipoRespiracion);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Soltar(countdown, tipoRespiracion);
    }

    public void Clicar (Countdown countdown, Respiracion tipo)
    {
        var puntuacion = tipoRespiracion == Respiracion.Inspirar ? "puntuacionInspirar" : "puntuacionEspirar";

        if (tipo == Respiracion.Inspirar && countdown.estadoJuego == Countdown.EstadoJuego.Inspirar)
        {
            countdown.inspirar();
        }
        else if (tipo == Respiracion.Espirar && countdown.estadoJuego == Countdown.EstadoJuego.Espirar)
        {
            countdown.espirar();
        }
        else
            countdown.Error(puntuacion);
    }

    public void Soltar(Countdown countdown, Respiracion tipo)
    {
        if (tipo == Respiracion.Inspirar && countdown.estadoJuego == Countdown.EstadoJuego.Inspirar)
        {
            countdown.soltarInspirar();
        }
        else if (tipo == Respiracion.Espirar && countdown.estadoJuego == Countdown.EstadoJuego.Espirar)
        {
            countdown.soltarEspirar();
        }
    }

    private void Start()
    {
        
    }
}
