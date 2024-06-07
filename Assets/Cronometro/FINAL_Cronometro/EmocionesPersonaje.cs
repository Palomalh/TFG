using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmocionesPersonaje : MonoBehaviour
{
    Animator animator;
    public Renderer[] characterMaterials;

    public Texture2D[] albedoList;
    [ColorUsage(true, true)]
    public Color[] eyeColors;
    public enum EyePosition { normal, happy, angry, dead }
    public EyePosition eyeState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        //Preocupar();
    }


    public void Preocupar()
    {

            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
    }

    public void Calmar()
    {
        ChangeEyeOffset(EyePosition.normal);
        ChangeAnimatorIdle("normal");
    }

   public void Alegrar()
    {
        ChangeEyeOffset(EyePosition.happy);
        ChangeAnimatorIdle("happy");
    }


    void ChangeAnimatorIdle(string trigger)
    {
        if (animator != null)
        {
            animator.SetTrigger(trigger);
        }
        else
        {
            Debug.LogWarning("Animator component is not assigned or missing.");
        }
    }


    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_BaseMap", offset);  // Adjusting texture offsets for eyes
        }
    }
}
