using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue parameters")]
    private Queue<string> sentences;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("PauseMenuControls")]
    public PauseMenu _pauseMenu;

    [Header("Character Image")]
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private Image imageHolder;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private bool sentenceIsBeingTyped = false;
    private string currentSentence;
    public int contador;
    private bool dialogueStarted;
    private bool canclick;

    public event System.EventHandler<int> OnEndDialogue;
    public event System.EventHandler<int> OnStartDialogue;


    void Awake()
    {
        dialogueStarted = false;
        sentences = new Queue<string>();
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;
    }

    private void Start()
    {
        nameText.text = "Balloony";
        dialogueStarted = false;
        sentences = new Queue<string>();
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_pauseMenu.pausedDialogue) return;
            if (!canclick) return;

            if (!sentenceIsBeingTyped)
            {

                DisplayNextSentence();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = currentSentence;
                sentenceIsBeingTyped = false;
            }
        }
    }

    public void StartDialogue (Dialogue dialogue, TMP_Text cuadroTexto, Animator cuadroAnimator)
    {
        canclick = true;
        animator = cuadroAnimator;
        animator.SetBool("isOpen", true);
        dialogueText = cuadroTexto;
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        dialogueStarted = true;
        OnStartDialogue?.Invoke(this, contador);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        currentSentence = sentences.Dequeue();
        if (sentences.Count >= 0)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentSentence));
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        sentenceIsBeingTyped = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        sentenceIsBeingTyped = false;
    }

    public void EndDialogue()
    {
        canclick = false;
        animator.SetBool("isOpen", false);

        if (dialogueStarted == true)
        {
            contador++;
            dialogueStarted = false;
        }
        OnEndDialogue?.Invoke(this, contador);
    }

}
