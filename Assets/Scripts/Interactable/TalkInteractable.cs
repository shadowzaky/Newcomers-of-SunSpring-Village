using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteractable : Interactable
{
    public DialogueContainer dialogue;
    public override void Interact()
    {
        GameManager.instance.dialogueSystem.Initialize(dialogue);
    }
}
