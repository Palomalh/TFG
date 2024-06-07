using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerAnxiety : MonoBehaviour
{
    [SerializeField] private GameObject[] mouseClick;
    [SerializeField] private GameObject cronometroCanvas;

    [SerializeField] private GameObject[] dialogos;
    [SerializeField] private DialogueManager dialogueManager;
    private int currentDialogue;
    // Start is called before the first frame update    
    void Start()
    {
        for (int i = 1; i < dialogos.Length; i++)
        {
            dialogos[i].SetActive(false);
        }
        currentDialogue = 0;
        dialogos[currentDialogue].SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DialogueManager_OnEndDialogue(object sender, int contador)
    {

        currentDialogue = contador;
        for (int i = 0; i < mouseClick.Length; i++)
        {
            mouseClick[i].SetActive(false);
        }

        if (contador == 1)
        {
            cronometroCanvas.SetActive(true);
            //dialogos[currentDialogue].SetActive(true);
        }

    }

    private void OnEnable()
    {
        dialogueManager.OnEndDialogue += DialogueManager_OnEndDialogue;
        //dialogueManager.OnStartDialogue += DialogueManager_OnStartDialogue
    }
    private void OnDisable()
    {
        dialogueManager.OnEndDialogue -= DialogueManager_OnEndDialogue;
       // dialogueManager.OnStartDialogue -= DialogueManager_OnStartDialogue
    }
}
