using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField]ControladorNiveles controladorNiveles;
    static public int numLlaves = 0;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            numLlaves++;
            AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_KeyCollect");

            if (numLlaves < 3)
            {
                controladorNiveles.IniciarSiguienteNivel();
            }else
            {
                controladorNiveles.FinJuego();
            }
            Destroy(gameObject);
        }
    }

}
