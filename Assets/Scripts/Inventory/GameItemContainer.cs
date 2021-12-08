using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public GameItem item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Set(GameItem item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/GameItemContainer")]
public class GameItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    public void Add(GameItem item, int quantity = 1)
    {
        if (item.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += quantity;
            }
            else
            {
                ItemSlot slot = slots.Find(x => x.item == null);
                if (slot != null)
                {
                    slot.item = item;
                    slot.count = quantity;
                }
            }
        }
        else
        {
            ItemSlot slot = slots.Find(x => x.item == null);
            if (slot != null)
            {
                slot.item = item;
            }
        }
    }

    public void Remove(GameItem itemToRemove, int quantity = 1)
    {
        if (itemToRemove.stackable)
        {
            ItemSlot slot = slots.Find(x => x.item == itemToRemove);
            if (slot == null)
            {
                return;
            }
            slot.count -= quantity;
            if (slot.count <= 0)
            {
                slot.Clear();
            }
        }
        else
        {
            while (quantity > 0)
            {
                quantity--;

                ItemSlot slot = slots.Find(x => x.item == itemToRemove);
                if (slot == null)
                {
                    break;
                }
                slot.Clear();
            }
        }
    }

    internal bool CheckFreeSpace()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }
        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem.item);
        if (itemSlot == null)
        {
            return false;
        }

        if (checkingItem.item.stackable) { return itemSlot.count > checkingItem.count; }
        return true;
    }
}
