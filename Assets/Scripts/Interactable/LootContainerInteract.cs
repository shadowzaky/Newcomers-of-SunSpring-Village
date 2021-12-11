using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    public Sprite openSprite;
    public Sprite closedSprite;
    public bool opened = false;
    public AudioClip onOpenAudio;
    public AudioClip onCloseAudio;
    public GameItemContainer itemContainer;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        opened = !opened;
        
        if (opened)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {
        opened = true;
        spriteRenderer.sprite = openSprite;
        AudioManager.instance.Play(onOpenAudio);
        ItemContainerInteractController containerInteractController = GameManager.instance.player.GetComponent<ItemContainerInteractController>();
        containerInteractController.Open(itemContainer, transform);
    }

    public void Close()
    {
        opened = false;
        spriteRenderer.sprite = closedSprite;
        AudioManager.instance.Play(onCloseAudio);
        ItemContainerInteractController containerInteractController = GameManager.instance.player.GetComponent<ItemContainerInteractController>();
        containerInteractController.Close();
    }
}
