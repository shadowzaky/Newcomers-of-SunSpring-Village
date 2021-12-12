using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConvertorInteract : Interactable
{
    public SpriteRenderer spriteRenderer;
    public Sprite itemProcessingActiveSprite;
    public Sprite itemProcessingIdleSprite;
    public GameItem convertableItem;
    public GameItem producedItem;
    public int producedItemCount = 1;

    ItemSlot itemSlot;
    Animator animator;

    public float timeToProcess = 5f;
    float timer;

    void Start()
    {
        itemSlot = new ItemSlot();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (itemSlot == null) { return; }

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                CompleteItemConversion();
            }
        }
    }

    void CompleteItemConversion()
    {
        itemSlot.Clear();
        itemSlot.Set(producedItem, producedItemCount);
        spriteRenderer.sprite = itemProcessingIdleSprite;
        animator.SetBool("Working", false);
    }

    public override void Interact()
    {
        if (itemSlot.item == null)
        {
            if (GameManager.instance.dragAndDropController.Check(convertableItem))
            {
                StartItemProcessing();
            }
        }

        if (itemSlot.item != null && timer < 0f)
        {
            GameManager.instance.inventoryContainer.Add(itemSlot.item, itemSlot.count);
            itemSlot.Clear();
        }
    }

    void StartItemProcessing()
    {
        itemSlot.Copy(GameManager.instance.dragAndDropController.itemSlot);
        GameManager.instance.dragAndDropController.RemoveItem();

        timer = timeToProcess;
        spriteRenderer.sprite = itemProcessingActiveSprite;
        animator.SetBool("Working", true);
    }


}
