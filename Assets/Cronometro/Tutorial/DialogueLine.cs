using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private TMP_Text textHolder;
        [SerializeField]private string input;

        [Header("Time parameters")]
        [SerializeField] private float delay;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();
            textHolder.text = "";

            StartCoroutine(WriteText(input, textHolder, delay));
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }
    }
}
