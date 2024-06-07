using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinController : MonoBehaviour
{
    Animator animator;
    Renderer[] characterMaterials;

    public Texture2D[] albedoList;
    [ColorUsage(true, true)]
    public Color[] eyeColors;
    public enum EyePosition { normal, happy, angry, dead }
    public EyePosition eyeState;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
        }
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ChangeMaterialSettings(int index)
    {
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
            {
                // Ensure emission is enabled to see the color effect
                characterMaterials[i].material.EnableKeyword("_EMISSION");
                characterMaterials[i].material.SetColor("_EmissionColor", eyeColors[index]);
            }
            else
                characterMaterials[i].material.SetTexture("_BaseMap", albedoList[index]);  // URP uses _BaseMap instead of _MainTex
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
