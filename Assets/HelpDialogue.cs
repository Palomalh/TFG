using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDialogue : MonoBehaviour
{
    public GameObject dialogo;
    public GameObject pulsador;

    public event System.EventHandler<bool> OnHelpComplete;


    private void Update()
    {
        pulsador.GetComponent<PulsadorScript>().CheckCompletionHelpDialogue();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            dialogo.SetActive(true);
            pulsador.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ayudaCompletada()
    {
        pulsador.SetActive(false);

        dialogo.SetActive(false);
        OnHelpComplete?.Invoke(this, true);
    }

}
