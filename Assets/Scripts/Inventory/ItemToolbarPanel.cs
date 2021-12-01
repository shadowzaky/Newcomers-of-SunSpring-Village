using System.Collections;
using System.Collections.Generic;
using HeroEditor4D.Common.Enums;
using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    public ToolbarController toolbarController;
    int currentSelectedTool = 0;

    void Start()
    {
        Init();
        toolbarController.onChange += Hightlight;
        Hightlight(currentSelectedTool);
    }

    public override void OnClick(int id)
    {
        toolbarController.Set(id);
        Hightlight(id);
    }
    public void Hightlight(int id)
    {
        buttons[currentSelectedTool].Hightlight(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].Hightlight(true);
    }
}
