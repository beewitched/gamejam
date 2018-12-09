using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractWithPlayer
{
    bool IsInteractable { get; }
    void EnableInteraction();
    void DisableInteraction();
    void Interact();
}
