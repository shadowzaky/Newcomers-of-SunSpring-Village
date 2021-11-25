using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public GameItem item;
    public int count;
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
