using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedLootItemData
{
    public int itemId;
    public int count;

    public SavedLootItemData(int id, int count)
    {
        itemId = id;
        this.count = count;
    }
}

[Serializable]
public class LootItemDataToSave
{
    public List<SavedLootItemData> itemDatas;

    public LootItemDataToSave()
    {
        itemDatas = new List<SavedLootItemData>();
    }
}

public class LootContainerInteract : Interactable, IPersistant
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

    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (itemContainer == null)
        {
            itemContainer = (GameItemContainer)ScriptableObject.CreateInstance(typeof(GameItemContainer));
            itemContainer.Init();
        }
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

    public string Read()
    {
        LootItemDataToSave toSave = new LootItemDataToSave();

        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                toSave.itemDatas.Add(new SavedLootItemData(-1, 0));
            }
            else
            {
                var slot = itemContainer.slots[i];
                toSave.itemDatas.Add(new SavedLootItemData(slot.item.id, slot.count));
            }
        }

        return JsonUtility.ToJson(toSave);
    }

    public void Load(string jsonString)
    {
        if (jsonString == "" || jsonString == "{}") { return; }
        Init();
        
        LootItemDataToSave toLoad = JsonUtility.FromJson<LootItemDataToSave>(jsonString);
        for (int i = 0; i < toLoad.itemDatas.Count; i++)
        {
            if (toLoad.itemDatas[i].itemId == -1)
            {
                itemContainer.slots[i].Clear();
            }
            else
            {
                itemContainer.slots[i].item = GameManager.instance.itemDB.items[toLoad.itemDatas[i].itemId];
                itemContainer.slots[i].count = toLoad.itemDatas[i].count;
            }
        }
    }
}
