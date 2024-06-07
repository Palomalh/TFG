using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public TMP_Text text;
    public Animator animator;

    private void OnEnable()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, text, animator);
    }
}
