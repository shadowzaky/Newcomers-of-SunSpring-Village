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
}
