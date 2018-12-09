using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractWithPlayer
{
    public Dialogue dialogue;
    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
