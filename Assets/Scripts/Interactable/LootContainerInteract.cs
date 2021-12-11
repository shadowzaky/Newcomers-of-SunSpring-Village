using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    public Sprite openSprite;
    public Sprite closedSprite;
    public bool opened;
    public AudioClip onOpenAudio;
    public AudioClip onCloseAudio;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        opened = !opened;
        spriteRenderer.sprite = opened ? openSprite : closedSprite;
        AudioManager.instance.Play(opened ? onOpenAudio : onCloseAudio);

    }
}
