using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteractable : Interactable
{
    public override void Interact()
    {
        Debug.Log("You talked with me");
    }
}
