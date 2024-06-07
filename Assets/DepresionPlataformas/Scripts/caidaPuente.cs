using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caidaPuente : MonoBehaviour
{
    public GameObject[] partesPuente;
    public GameObject partePuentePrimera;
    public GameObject triggerHelpDialogue;

    private void OnTriggerEnter(Collider other)
    { 

        if (other.CompareTag("Player"))
        {
            AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_WoodCrash");
            Debug.Log("colisiona con trigger caida puente");
            //Activa el trigger del pulsador y el dialogo
            triggerHelpDialogue.SetActive(true);

            //Desactiva el collider de la primera parte del puente
            partePuentePrimera.GetComponent<MeshCollider>().enabled = false;

            //Hace caer todas las partes del medio del puente
            foreach (GameObject p in partesPuente)
            {
                p.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }


    private void triggerHelpDialogue_OnHelpComplete(object sender, bool e)
    {
        //Activa el collider de la primera parte del puente
        partePuentePrimera.GetComponent<MeshCollider>().enabled = true;
    }

    public void OnEnable()
    {
        triggerHelpDialogue.GetComponent<HelpDialogue>().OnHelpComplete += triggerHelpDialogue_OnHelpComplete;
    }

    public void OnDisable()
    {
        triggerHelpDialogue.GetComponent<HelpDialogue>().OnHelpComplete -= triggerHelpDialogue_OnHelpComplete;
    }


}
