using Assets.HeroEditor4D.Common.CharacterScripts;
using UnityEngine;

public class CharacterInteractionController : MonoBehaviour
{
    public float offsetDistance = 1f;
    public float sizeOfInteractableArea = 1.2f;

    Character4D character;
    Rigidbody2D body2d;

    void Awake()
    {
        character = GetComponent<Character4D>();
        body2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Vector2 position = body2d.position + character.Direction * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }

    
}
