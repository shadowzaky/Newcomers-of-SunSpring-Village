using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public int toolbarSize = 12;

    int selectedTool = 0;

    public Action<int> onChange;
    public ItemHighlight itemHighlight;

    public GameItem GetItem
    {
        get
        {
            return GetItemAtIndex(selectedTool);
        }
    }

    public ItemSlot GetItemSlot
    {
        get
        {
            return GameManager.instance.inventoryContainer.slots[selectedTool];
        }
    }

    void Start()
    {
        onChange += UpdateHighlightItem;
        UpdateHighlightItem(selectedTool);
    }

    private GameItem GetItemAtIndex(int index)
    {
        return GameManager.instance.inventoryContainer.slots[index].item;
    }

    void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            var previousSelectedTool = selectedTool;
            if (delta > 0)
            {
                selectedTool++;
                selectedTool = selectedTool >= toolbarSize ? 0 : selectedTool;
            }
            else
            {
                selectedTool--;
                selectedTool = selectedTool >= 0 ? selectedTool : toolbarSize - 1;
            }
            onChange?.Invoke(selectedTool);
            GetItem?.onAction?.OnToolbarSelectedChanged(GetItem);
            var previousSelectedGameItem = GetItemAtIndex(previousSelectedTool);
            if (previousSelectedGameItem != null)
            {
                previousSelectedGameItem.onAction?.OnToolbarSelectedChanged(previousSelectedGameItem, false);
            }
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }

    public void UpdateHighlightItem(int id = 0)
    {
        GameItem item = GetItem;
        if (item == null) {
            itemHighlight.Show = false;
            return;
        }
        itemHighlight.Show = item.itemHighlight;
        if (item.itemHighlight)
        {
            itemHighlight.Set(item.icon);
        }
    }
}
