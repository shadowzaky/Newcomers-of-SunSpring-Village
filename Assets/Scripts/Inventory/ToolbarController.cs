using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public int toolbarSize = 12;

    int selectedTool = 0;

    public Action<int> onChange;

    public GameItem GetItem
    {
        get
        {
            return GetItemAtIndex(selectedTool);
        }
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
}
