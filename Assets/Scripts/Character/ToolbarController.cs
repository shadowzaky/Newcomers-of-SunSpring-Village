using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public int toolbarSize = 12;
    
    int selectedTool = 0;

    public Action<int> onChange;

    void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
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
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }
}