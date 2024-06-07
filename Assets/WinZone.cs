using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    //public Rigidbody2D[] piezas;
    //public GameObject ball;
    public GameObject winText;
    public bool finJuego;
    public GameObject pantallaFinJuego;
    private int dificultad;
    [SerializeField] private TMP_Text textoMovimientos;
    [SerializeField] private TMP_Text textoExplicativo;

    // Start is called before the first frame update
    void Start()
    {
        dificultad = PlayerPrefs.GetInt("nivelJuego");
        finJuego = false;
        winText.gameObject.SetActive(false);
        if (dificultad == 0)
        {
            textoExplicativo.text = "¿Sabes que este puzle puede ser resuleto en solo 12 movimientos?";
        }else
        {
            textoExplicativo.text = "¿Sabes que este puzle puede ser resuleto en solo 14 movimientos?";
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ball")
        {
            textoMovimientos.text = (1+Pieza.movimientos).ToString();
            finJuego = true;
            AudioManager.audioManagerInstance.PlaySFX("SFX_Anx_PuntuationFinal");
            pantallaFinJuego.SetActive(true);
            winText.gameObject.SetActive(true);
            Pieza.movimientos = 0;
            //ball = GameObject.Find("Ball");
            //ball.transform.localPosition= new Vector3(-1, -2, 0);


            //foreach (Rigidbody2D rb in piezas)
            //{
            //    if (!col.tag.Equals("B  all"))
            //    {
            //        rb.simulated = false;

            //    }

            //}

        }
    }
}
