using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPruebaTeclaE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationComplete(AnimationEvent animationEvent)
    {
        // Aqu� puedes ejecutar el c�digo que te interesa
        Debug.Log("Ha terminado la primera animaci�n del animator");
    }
}
