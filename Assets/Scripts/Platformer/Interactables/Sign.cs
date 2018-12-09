using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractWithPlayer
{
    [SerializeField]
    private Dialogue dialogue;

    [SerializeField]
    private bool isInteractable = true;
    public bool IsInteractable
    {
        get
        {
            return isInteractable;
        }
    }

    public void DisableInteraction()
    {
        isInteractable = false;
    }

    public void EnableInteraction()
    {
        isInteractable = true;
    }

    public void Interact()
    {
        if (isInteractable)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }
}
