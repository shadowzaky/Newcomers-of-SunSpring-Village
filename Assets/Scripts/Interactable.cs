using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite interactedSprite;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            
            GetComponent<SpriteRenderer>().sprite = interactedSprite;
        }
    }
}
