﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
    private void OnMouseDown()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
