using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemConvertorData
{
    public ItemSlot itemSlot;
    public float timer;

    public ItemConvertorData()
    {
        itemSlot = new ItemSlot();
    }
}

public class ItemConvertorInteract : Interactable, IPersistant
{
    public SpriteRenderer spriteRenderer;
    public Sprite itemProcessingActiveSprite;
    public Sprite itemProcessingIdleSprite;
    public GameItem convertableItem;
    public GameItem producedItem;
    public int producedItemCount = 1;

    public float timeToProcess = 5f;

    ItemConvertorData data;
    Animator animator;

    void Start()
    {
        if (data == null)
        {
            data = new ItemConvertorData();
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (data.itemSlot == null) { return; }

        if (data.timer > 0f)
        {
            data.timer -= Time.deltaTime;
            if (data.timer <= 0f)
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
        animator.SetBool("Working", false);
    }

    public override void Interact()
    {
        if (data.itemSlot.item == null)
        {
            if (GameManager.instance.dragAndDropController.Check(convertableItem))
            {
                StartItemProcessing();
            }
        }

        if (data.itemSlot.item != null && data.timer < 0f)
        {
            GameManager.instance.inventoryContainer.Add(data.itemSlot.item, data.itemSlot.count);
            data.itemSlot.Clear();
        }
    }

    void StartItemProcessing()
    {
        data.itemSlot.Copy(GameManager.instance.dragAndDropController.itemSlot);
        data.itemSlot.count = 1;
        GameManager.instance.dragAndDropController.RemoveItem();

        data.timer = timeToProcess;
        spriteRenderer.sprite = itemProcessingActiveSprite;
        animator.SetBool("Working", true);
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
