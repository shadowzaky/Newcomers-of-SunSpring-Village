using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemConvertorData
{
    public ItemSlot itemSlot;
    public int timer;

    public ItemConvertorData()
    {
        itemSlot = new ItemSlot();
    }
}

[RequireComponent(typeof(TimeAgent))]
public class ItemConvertorInteract : Interactable, IPersistant
{
    public SpriteRenderer spriteRenderer;
    public Sprite itemProcessingActiveSprite;
    public Sprite itemProcessingIdleSprite;
    public GameItem convertableItem;
    public GameItem producedItem;
    public int producedItemCount = 1;

    public int timeToProcess = 5;

    ItemConvertorData data;
    Animator animator;

    void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += ItemConvertProcess;

        if (data == null)
        {
            data = new ItemConvertorData();
        }
        animator = GetComponent<Animator>();
        Animate();
    }

    private void ItemConvertProcess()
    {
        if (data.itemSlot == null) { return; }

        if (data.timer > 0)
        {
            data.timer -= 1;
            if (data.timer <= 0)
            {
                CompleteItemConversion();
            }
        }
    }

    void CompleteItemConversion()
    {
        data.itemSlot.Clear();
        data.itemSlot.Set(producedItem, producedItemCount);
        spriteRenderer.sprite = itemProcessingIdleSprite;
        Animate();
    }

    public override void Interact()
    {
        if (data.itemSlot.item == null)
        {
            if (GameManager.instance.dragAndDropController.Check(convertableItem))
            {
                StartItemProcessing(GameManager.instance.dragAndDropController.itemSlot);
                return;
            }

            ToolbarController toolbarController = GameManager.instance.character.GetComponent<ToolbarController>();
            if (toolbarController == null) { return; }
            ItemSlot itemSlot = toolbarController.GetItemSlot;
            if (itemSlot.item == convertableItem)
            {
                StartItemProcessing(itemSlot);
                return;
            }
        }

        if (data.itemSlot.item != null && data.timer <= 0f)
        {
            GameManager.instance.inventoryContainer.Add(data.itemSlot.item, data.itemSlot.count);
            data.itemSlot.Clear();
        }
    }

    void StartItemProcessing(ItemSlot toProcess)
    {
        data.itemSlot.Copy(GameManager.instance.dragAndDropController.itemSlot);
        data.itemSlot.count = 1;
        if (toProcess.item.stackable)
        {
            toProcess.count -= 1;
            if (toProcess.count < 0)
            {
                toProcess.Clear();
            }
        }
        else
        {
            GameManager.instance.dragAndDropController.RemoveItem();
        }

        data.timer = timeToProcess;
        Animate();
    }

    void Animate()
    {
        bool isWorking = data.timer > 0f;
        animator.SetBool("Working", isWorking);
        spriteRenderer.sprite = isWorking ? itemProcessingActiveSprite : itemProcessingIdleSprite;
    }

    public string Read()
    {
        return JsonUtility.ToJson(data);
    }

    public void Load(string jsonString)
    {
        data = JsonUtility.FromJson<ItemConvertorData>(jsonString);
    }
}
